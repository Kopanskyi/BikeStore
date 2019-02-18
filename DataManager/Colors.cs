﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class Colors
    {
        string MySQl = "Select * from TColors";

        public DataTable Table;

        SqlDataAdapter adapter;
        //SqlCommandBuilder builder;


        public DataTable GetTable()
        {
            Table = new DataTable();
            adapter = new SqlDataAdapter(MySQl, Manager.Instance.Connect);

            adapter.Fill(Table);
            return Table;

        }
    }
}
