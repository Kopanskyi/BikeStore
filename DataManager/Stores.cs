using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public class Stores
    {
        string MySQL;

        public DataTable Table;

        SqlDataAdapter adapter;
        //SqlCommandBuilder builder;
        
        public DataTable GetTable()
        {
            MySQL = "Select TStores.Id, TStores.Name, TStores.Address, TStores.Phone, " +
                "(Select COUNT(StoreId) from TBikes where TBikes.StoreId = TStores.Id) as Bikes " +
                "From TStores left join TBikes on TBikes.StoreId = TStores.Id " +
                "Group by TStores.Id, TStores.Name, TStores.Address, TStores.Phone";

            Table = new DataTable();
            adapter = new SqlDataAdapter(MySQL, Manager.Instance.Connect);

            adapter.Fill(Table);

            return Table;
        }

        public void Add(string name, string address, string phone)
        {
            int maxId = (int)Table.Select().Max(r => r["Id"]);

            MySQL = "Insert into TStores " +
                $"values ({++maxId}, '{name}', '{address}', '{phone}')";

            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }

        public void Edit(int id, string name, string address, string phone)
        {
            MySQL = "Update TStores " +
                $"set Name = '{name}', " +
                $"Address = '{address}', " +
                $"Phone = '{phone}' " +
                $"where Id = {id}";

            
            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
