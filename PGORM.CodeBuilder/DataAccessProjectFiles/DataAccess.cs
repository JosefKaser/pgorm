using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Data.Common;
using System.Data;

namespace TemplateNS.Core
{
    #region ObjectRelationMapper
    public delegate T ObjectRelationMapper<T>(IDataReader reader);
    #endregion

    #region DbINParameter
    public class DbINParameter<T> : DbParameter
    {
        private string p_param_name;
        private string p_param_value;
        public DbINParameter(string parameter_name, List<T> value_list)
        {
            p_param_name = parameter_name;
            p_param_value = ExplodeArray(value_list, ",");
        }

        /// <summary>
        /// Flattens an arry of objects to an string using a separator
        /// </summary>
        /// <typeparam name="T">Generic type of array elements</typeparam>
        /// <param name="array">Generic list of T</param>
        /// <param name="separator">item separator</param>
        /// <returns>Flatten string of array.</returns>
        private string ExplodeArray(List<T> array, string separator)
        {
            string result = "";
            if (typeof(T) == typeof(string))
                array.ForEach(i => result += (i != null ? string.Format("E'{0}',", i) : ""));
            else
                array.ForEach(i => result += (i != null ? string.Format("{0},", i) : ""));
            if (result != "")
                result = result.Substring(0, result.Length - 1);
            return result;
        }


        public override DbType DbType
        {
            get
            {
                if (typeof(T) == typeof(string))
                    return DbType.String;
                else
                    return DbType.VarNumeric;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override ParameterDirection Direction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool IsNullable
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override string ParameterName
        {
            get
            {
                return p_param_name;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void ResetDbType()
        {
            throw new NotImplementedException();
        }

        public override int Size
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override string SourceColumn
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool SourceColumnNullMapping
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override DataRowVersion SourceVersion
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override object Value
        {
            get
            {
                return p_param_value;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    } 
    #endregion

    #region DataAccess
    public class DataAccess
    {
        #region Props
        public static NpgsqlConnection Connection { get; set; }
        public static bool ThrowMapperException { get; set; }
        public static bool IsDatabaseInitialized { get; set; }
        #endregion

        #region DataAccess
        static DataAccess()
        {
            ThrowMapperException = true;
        } 
        #endregion

        #region CheckDatabase
        protected static void CheckDatabase()
        {
            if (!IsDatabaseInitialized)
            {
                throw new Exception("Database system is not yet initialized");
            }
        }
        #endregion

        #region InitializeDatabase
        public static void InitializeDatabase(string connectionstring, bool throw_exception)
        {
            if (Connection != null)
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();

            Connection = null;
            Connection = new NpgsqlConnection(connectionstring);
            try
            {
                Connection.Open();
                IsDatabaseInitialized = true;
            }
            catch (Exception e)
            {
                IsDatabaseInitialized = false;
                if (throw_exception)
                    throw e;
            }
        }
        public static void InitializeDatabase(string host, string database, string username, string password, string options, bool throw_exception)
        {
            string constr = string.Format("host={0};database={1};user={2};password={3};{4}", host, database, username, password, options);
            InitializeDatabase(constr, throw_exception);
        }
        #endregion

        #region Transaction
        public static NpgsqlTransaction BeginTransaction()
        {
            CheckDatabase();
            return Connection.BeginTransaction();
        }

        public static void CreateSavePoint(string savespoint, NpgsqlTransaction transation)
        {
            NpgsqlCommand command = Connection.CreateCommand();
            command.CommandText = string.Format("SAVEPOINT {0}", savespoint);
            command.ExecuteNonQuery();
        }

        public static void RollbackToSavePoint(string savespoint, NpgsqlTransaction transation)
        {
            NpgsqlCommand command = Connection.CreateCommand();
            command.CommandText = string.Format("ROLLBACK TO SAVEPOINT  {0}", savespoint);
            command.ExecuteNonQuery();
        }

        public static void ReleaseSavePoint(string savespoint, NpgsqlTransaction transation)
        {
            NpgsqlCommand command = Connection.CreateCommand();
            command.CommandText = string.Format("RELEASE SAVEPOINT {0}", savespoint);
            command.ExecuteNonQuery();
        }

        public static void RollbackTransaction(NpgsqlTransaction transaction)
        {
            transaction.Rollback();
        }

        public static void CommitTransaction(NpgsqlTransaction transaction)
        {
            transaction.Commit();
        }
        #endregion

        #region ExecuteSingleObjectQuery
        public static T ExecuteSingleObjectQuery<T>(string sqlStatement, ObjectRelationMapper<T> ormFunction, NpgsqlTransaction trans, params DbParameter[] parameters)
        {
            List<T> result = ExecuteObjectQuery<T>(sqlStatement, ormFunction, trans, parameters);
            if (result != null)
                return result[0];
            else
            {
                object nullobj = null;
                return (T)nullobj;
            }
        } 
        #endregion

        #region ExecuteObjectQuery
        public static R ExecuteObjectQuery<T,R>(string sqlStatement, ObjectRelationMapper<T> ormFunction, NpgsqlTransaction trans, params DbParameter[] parameters) where R : List<T>,new()
        {
            R result = new R();

            NpgsqlCommand command = new NpgsqlCommand(sqlStatement, DataAccess.Connection, trans);
            SetupParameters(command, parameters);

            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(ormFunction(reader));
                }
                reader.Close();
                return result;
            }
            else
            {
                reader.Close();
                return null;
            }
        }

        public static List<T> ExecuteObjectQuery<T>(string sqlStatement, ObjectRelationMapper<T> ormFunction,NpgsqlTransaction trans, params DbParameter[] parameters)
        {
            List<T> result = new List<T>();

            NpgsqlCommand command = new NpgsqlCommand(sqlStatement, DataAccess.Connection,trans);
            SetupParameters(command, parameters);

            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(ormFunction(reader));
                }
                reader.Close();
                return result;
            }
            else
            {
                reader.Close();
                return null;
            }
        }
        #endregion

        #region ExecuteNoneQuery
        public static int ExecuteNoneQuery(string sqlStatement,NpgsqlTransaction trans, params DbParameter[] parameters)
        {
            NpgsqlCommand command = new NpgsqlCommand(sqlStatement, DataAccess.Connection,trans);
            SetupParameters(command, parameters);
            return command.ExecuteNonQuery();
        } 
        #endregion

        #region ExecuteScalarQuery
        public static T ExecuteScalarQuery<T>(string sqlStatement,NpgsqlTransaction trans, params DbParameter[] parameters)
        {
            return (T)ExecuteScalarQuery(sqlStatement,trans, parameters);
        }

        public static object ExecuteScalarQuery(string sqlStatement,NpgsqlTransaction trans, params DbParameter[] parameters)
        {
            NpgsqlCommand command = new NpgsqlCommand(sqlStatement, DataAccess.Connection,trans);
            SetupParameters(command, parameters);
            return command.ExecuteScalar();
        } 
        #endregion

        #region SetupParameters
        private static void SetupParameters(DbCommand command, DbParameter[] parameters)
        {
            string value = "";
            foreach (DbParameter param in parameters)
            {
                value = param.Value.ToString();
                if (param.GetType().Name.Contains("DbINParameter"))
                    command.CommandText = command.CommandText.Replace(param.ParameterName, string.Format("({0})", param.Value));
                else if (value.Contains("array["))
                    command.CommandText = command.CommandText.Replace(param.ParameterName, string.Format("{0}", param.Value));
                else
                    command.Parameters.Add(param);
            }
        } 
        #endregion

        #region Convert
        public static T Convert<T>(object data, object default_if_null)
        {
            if (data == null || data == DBNull.Value)
                return (T)default_if_null;
            else
                return (T)data;
        } 
        #endregion

        #region NewParameter
        public static DbParameter NewParameter(string name, object value)
        {
            return new NpgsqlParameter(name, value);
        } 
        #endregion

        #region DbINParameter
        public static DbINParameter<T> NewINParameter<T>(string name, List<T> values)
        {
            return new DbINParameter<T>(name, values);
        } 
        #endregion
    } 
    #endregion
}