﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PGORMWizard
{
    #region ParameterName
    internal class ParameterName
    {
        public static string project_type = "project_type";
        public static string selected_project = "selected_project";
        public static string db_connection_string = "db_connection_string";
    } 
    #endregion

    #region Helper
    internal class Helper
    {
        public static Image PGLogo
        {
            get
            {
                return global::PGORMWizard.Properties.Resources.elephant;
            }
        }
    } 
    #endregion
}