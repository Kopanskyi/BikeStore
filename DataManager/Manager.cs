using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace DataManager
{
    public enum Active
    {
        Stores,
        Bikes,
        Models,
        Users,
        Orders
    }

    public class Manager
    {
        // Data Source=DELL;Initial Catalog=Tourfirm;Integrated Security=True
        string ConString = @" Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Store;Integrated Security=True";

        public SqlConnection Connect;

        public Active ActiveTable;
        public Stores Stores;
        public Bikes Bikes;
        public Models Models;
        public Users Users;
        public Orders Orders;
        public Manufactures Manufactures;
        public ProducingCountry ProducingCountry;
        public Colors Colors;
        public Year Year;

        private static Manager instance;
        public static Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Manager();
                }
                return instance;
            }                
        }

        public Manager()
        {
            Connect = new SqlConnection(ConString);
            try
            {
                Connect.Open();
                Stores = new Stores();
                Bikes = new Bikes();
                Models = new Models();
                Users = new Users();
                Orders = new Orders();
                Manufactures = new Manufactures();
                ProducingCountry = new ProducingCountry();
                Colors = new Colors();
                Year = new Year();
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong connection!");
                throw;
            }

        }

    }
}
