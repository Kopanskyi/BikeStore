using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataManager
{
    public class Models
    {
        string MySQL;

        public DataTable Table;
        int maxId;

        SqlDataAdapter adapter;
        //SqlCommandBuilder builder;
         

        
        public DataTable GetTable(int ManufactureId = 0)
        {
            Table = new DataTable();

            if (ManufactureId > 0)
            {
                MySQL = $"Select * From TModel Where ManufactureId = {ManufactureId} Order by Name ";
            }
            else
            {
                MySQL = "Select * From TModel Order by Name";
            }

            adapter = new SqlDataAdapter(MySQL, Manager.Instance.Connect);

            adapter.Fill(Table);

            return Table;

        }

        private int GetMaxId()
        {
            string MySQl = "Select MAX(Id) from TModel";
            SqlCommand sqlCommand = new SqlCommand(MySQl, Manager.Instance.Connect);
            return maxId = (Int32)sqlCommand.ExecuteScalar();
        }

        public void Add(int manufactureId, string name, string desc, int producingCountryId)
        {
            MySQL = "Insert into TModel(Id, ManufactureId, Name, [Desc], ProducingCountryId) " +
                $"values({GetMaxId() + 1}, {manufactureId}, '{name}', '{desc}', {producingCountryId})";
            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }

        public void Edit(int id, int manufactureId, string name, string desc, int producingCountryId)
        {
            MySQL = "Update TModel " +
                $"set ManufactureId = {manufactureId}, " +
                $"Name = '{name}', " +
                $"[Desc] = '{desc}', " +
                $"ProducingCountryId = {producingCountryId} " +
                $"where Id = {id}";
            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            try
            {
                string sql = $"delete from TModel Where Id = {id}";
                SqlCommand sqlCommand = new SqlCommand(sql, Manager.Instance.Connect);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Delete bike first!!!");
            }
        }
    }
}
