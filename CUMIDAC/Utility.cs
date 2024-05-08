﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Data;
using System.Web.Script.Serialization;

namespace CUMIDAC
{
    public partial class WMSDAL
    {

        public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            try
            {
                Dictionary<string, object> childRow;
                foreach (DataRow row in table.Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in table.Columns)
                    {
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                }
            }
            catch (Exception ex)
            {

            }
            return jsSerializer.Serialize(parentRow);
        }
    }
}