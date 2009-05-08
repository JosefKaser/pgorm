﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TemplateNS.Core
{
	#region Helper
	public class Helper
	{
		public static readonly string SQL_SELECT = "SELECT * FROM {0} {1}";
		public static readonly string SQL_SELECT_COUNT = "SELECT COUNT(*) FROM {0}";
		public static readonly string SQL_SELECT_WHERE = "SELECT {0} FROM {1} WHERE {2} {3}";
		public static readonly string SQL_INSERT_INTO = "INSERT INTO {0} ({1}) VALUES ({2}) RETURNING *";
		public static readonly string SQL_UPDATE = "UPDATE {0} SET {1} WHERE {2} RETURNING *";
		public static readonly string SQL_DELETE_WHERE = "DELETE FROM {0} WHERE {1};";
        public static readonly string SQL_DELETE_TABLE = "DELETE FROM {0}";
        public static readonly string SQL_COPY_IN = "COPY {0} FROM STDIN";

        public static object MakeCopyIOSafe(object data)
        {
            if (data == null || data == DBNull.Value || string.IsNullOrEmpty(data.ToString()))
                return "\\N";
            else
                return data;
        }
	}
	#endregion
}