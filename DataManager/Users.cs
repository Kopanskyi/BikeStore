using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public class Users
    {
        string MySQl;

        public DataTable Table;

        SqlDataAdapter adapter;
        //SqlCommandBuilder builder;
        
        public DataTable GetTable()
        {
            MySQl = "Select * From TUsers Order by Name";
            Table = new DataTable();
            adapter = new SqlDataAdapter(MySQl, Manager.Instance.Connect);

            adapter.Fill(Table);

            return Table;

        }

        public void Add(string name, string phone)
        {
            int maxId = (int)Table.Select().Max(v => v["Id"]);

            MySQl = "Insert into TUsers(Id, Name, Phone) " +
                $"values ({++maxId}, '{name}', '{phone}')";

            SqlCommand sqlCommand = new SqlCommand(MySQl, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }

        public void Edit(int id, string name, string phone)
        {
            MySQl = "Update TUsers " +
                $"set Name = '{name}', " +
                $"Phone = '{phone}' " +
                $"where Id = {id}";

            SqlCommand sqlCommand = new SqlCommand(MySQl, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            MySQl = "delete from TUsers " +
                $"where Id = {id}";

            SqlCommand sqlCommand = new SqlCommand(MySQl, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
