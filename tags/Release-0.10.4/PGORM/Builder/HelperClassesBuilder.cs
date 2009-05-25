/*-------------------------------------------------------------------------
 * HelperClassesBuilder.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Antlr.StringTemplate;

namespace PGORM
{
    public class HelperClassesBuilder :TemplateBase
    {
        protected DatabaseSchema schema;
        public HelperClassesBuilder(ProjectFile p_project, VS2008Project p_vsproject, DatabaseSchema p_schema, Builder p_builder)
            : base(CodeTemplates.HelperClasses_stg, p_vsproject, p_project,p_builder)
        {
            schema = p_schema;
        }

        public override void Build()
        {
            SendMessage("Creating helper classes...",BuilderMessageType.Major);

            StringTemplate st;

            string out_dir = project.ProjectOutputFolder + "\\Helper";
            if (!Directory.Exists(out_dir))
                Directory.CreateDirectory(out_dir);

            SendMessage("Creating Helper.cs",BuilderMessageType.Minor);
            st = GetTemplate("helper");
            st.SetAttribute("namespace", project.RootNamespace);
            vsproject.AddCompileItem(@"Helper\Helper.cs", st.ToString());

            SendMessage("Creating DbObjectBase.cs",BuilderMessageType.Minor);
            st = GetTemplate("object_base");
            st.SetAttribute("namespace", project.RootNamespace);
            vsproject.AddCompileItem(@"Helper\DbObjectBase.cs", st.ToString());

            SendMessage("Creating DbRecordSetBase.cs", BuilderMessageType.Minor);
            st = GetTemplate("recordset_base");
            st.SetAttribute("namespace", project.RootNamespace);
            vsproject.AddCompileItem(@"Helper\DbRecordSetBase.cs", st.ToString());

            SendMessage("Creating DbObjectValue.cs", BuilderMessageType.Minor);
            st = GetTemplate("object_value");
            st.SetAttribute("namespace", project.RootNamespace);
            vsproject.AddCompileItem(@"Helper\DbObjectValue.cs", st.ToString());

            SendMessage("Creating ColumnMetaInfo.cs", BuilderMessageType.Minor);
            st = GetTemplate("column_meta_info");
            st.SetAttribute("namespace", project.RootNamespace);
            vsproject.AddCompileItem(@"Helper\ColumnMetaInfo.cs", st.ToString());

            SendMessage("Creating DMLOperation.cs", BuilderMessageType.Minor);
            st = GetTemplate("dml_opr_param");
            st.SetAttribute("namespace", project.RootNamespace);
            st.SetAttribute("libs", project.UsingLibs);
            vsproject.AddCompileItem(@"Helper\DMLOperation.cs", st.ToString());

            SendMessage("Creating FullLoader.cs", BuilderMessageType.Minor);
            st = GetTemplate("full_loader");
            st.SetAttribute("namespace", project.RootNamespace);
            st.SetAttribute("libs", project.UsingLibs);
            foreach (Table tbl in schema.Tables)
                if (!tbl.IsCompositeType)
                    st.SetAttribute("tables", tbl);
            vsproject.AddCompileItem(@"Helper\FullLoader.cs", st.ToString());
        }
    }
}