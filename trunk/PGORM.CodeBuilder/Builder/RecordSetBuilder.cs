﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PGORM.CodeBuilder.TemplateObjects;
using Antlr.StringTemplate;
using System.IO;

namespace PGORM.CodeBuilder
{
    public class RecordSetBuilder : TemplateBase
    {
        StringTemplate st;
        string p_ObjectNamespace;

        public RecordSetBuilder(ProjectBuilder p_builder,string object_namespace)
            : base(Templates.DataObjectRecordSet_stg, p_builder)
        {
            st = p_stgGroup.GetInstanceOf("recordset");
            p_ObjectNamespace = object_namespace;

        }

        public void Create(TemplateRelation relation, string destFolder)
        {
            string nspace = Helper.GetExplicitNamespace(p_Project, relation);
            string fname = string.Format(@"{0}\{1}_{2}_recordset.cs", destFolder, relation.SchemaName, relation.TemplateRelationName);
            st.Reset();
            st.SetAttribute("table", relation);
            st.SetAttribute("namespace", Helper.GetExplicitNamespace(p_Project, relation));
            st.SetAttribute("libs", p_Project.InternalReferences);
            st.SetAttribute("libs", string.Format("{0}.RecordSet", nspace));
            if (!string.IsNullOrEmpty(p_ObjectNamespace))
                st.SetAttribute("libs", string.Format("{0}.{1}", Helper.GetExplicitNamespace(p_Project, relation), p_ObjectNamespace));
            File.WriteAllText(fname, st.ToString());
        }
    }
}