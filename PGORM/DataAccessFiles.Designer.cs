/*-------------------------------------------------------------------------
 * DataAccessFiles.Designer.cs
 *
 * This file is part of the PGORM project.
 * http://pgorm.googlecode.com/
 *
 * Copyright (c) 2002-2009, TrueSoftware B.V.
 *
 * IDENTIFICATION
 * 
 *  $Id$
 * 	$HeadURL$
 * 	
 *-------------------------------------------------------------------------
 */
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PGORM {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DataAccessFiles {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DataAccessFiles() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PGORM.DataAccessFiles", typeof(DataAccessFiles).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using System;
        ///using System.Collections.Generic;
        ///using System.Linq;
        ///using System.Text;
        ///using Npgsql;
        ///using System.Data.Common;
        ///using System.Data;
        ///
        ///namespace Links.Core.Data
        ///{
        ///    #region ObjectRelationMapper
        ///    public delegate T ObjectRelationMapper&lt;T&gt;(IDataReader reader);
        ///    #endregion
        ///
        ///    #region DataAccess
        ///    public class DataAccess
        ///    {
        ///        #region Props
        ///        public static NpgsqlConnection Connection { get; set; }
        ///        private static bool IsDatabaseInitialized { get; set [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DataAccess {
            get {
                return ResourceManager.GetString("DataAccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using System;
        ///using System.Collections.Generic;
        ///using System.Linq;
        ///using System.Text;
        ///using System.ComponentModel;
        ///using System.Data.Common;
        ///using Links.Core.Data;
        ///
        ///namespace Links.Core.Data
        ///{
        ///    #region CompareOption
        ///    public enum CompareOption
        ///    {
        ///        Equals,
        ///        Like,
        ///        ILike
        ///    }
        ///    #endregion
        ///
        ///    #region OperationParameter
        ///    public class OperationParameter
        ///    {
        ///        #region Props
        ///        public string ColumnName { get; set; }
        ///        public string Par [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DatabaseOperation {
            get {
                return ResourceManager.GetString("DatabaseOperation", resourceCulture);
            }
        }
    }
}