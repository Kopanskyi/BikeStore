using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    enum OrderStatus
    {
        New,
        Complete
    }

    public class Orders
    {
        string MySQL;
        
        public DataTable OrdersTable;
        public DataTable ItemsTable;
        int maxItemsId;

        DataTable TemporaryItemsTable;

        SqlDataAdapter OrdersAdapter;
        SqlDataAdapter ItemsAdapter;
        //SqlCommandBuilder builder;

        string LocalDateFormat = "yyyy-MM-dd";

        public DataTable GetOrdersTable()
        {
            MySQL = "Select * From TOrder Order by Date";

            OrdersTable = new DataTable();
            OrdersAdapter = new SqlDataAdapter(MySQL, Manager.Instance.Connect);
            OrdersAdapter.Fill(OrdersTable);

            return OrdersTable;
        }

        public int GetNewOrderId()
        {
            return 1 + (OrdersTable.Select().Any() ? (int)OrdersTable.Select().Max(p => p["Id"]) : 0);
        }

        public int GetNewItemsId()
        {
            MySQL = "select MAX(Id) from TOrderList";
            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            return maxItemsId = (int)sqlCommand.ExecuteScalar();

            //return maxItemsId = (ItemsTable.Select().Any() ? (int)ItemsTable.Select().Max(p => p["Id"]) : 0);
        }

        public DataTable GetItemsTable(int orderId)
        {
            MySQL = "Select TOrderList.Id, TOrderList.OrderId, TOrderList.GoodsId, TManufacture.Name as Manufacture, " +
                "TModel.Name, TColors.Name as Color, TYear.Year, TBikes.Price, TStores.Name as Store " +
                "From TOrderList, TBikes, TModel, TStores, TManufacture, TColors, TYear " +
                $"WHERE TOrderList.OrderId = {orderId} " +
                "and TStores.Id = TBikes.StoreId " +
                "and TOrderList.GoodsId = TBikes.Id " +
                "and TModel.Id = TBikes.ModelId " +
                "and TManufacture.Id = TModel.ManufactureId " +
                "and TColors.Id = TBikes.ColorId " +
                "and TYear.Id = TBikes.YearId";

            //string sql = "SELECT * from TOrderList " +
            //    $"Where OrderId = {orderId}";

            ItemsTable = new DataTable();
            ItemsAdapter = new SqlDataAdapter(MySQL, Manager.Instance.Connect);
            ItemsAdapter.Fill(ItemsTable);

            return ItemsTable;
        }

        public void CreateNewOrder(int id, int user, DateTime date, int storeId)
        {
            MySQL = "INSERT INTO TOrder(Id, UserId, Date, Status, StoreId) " +
              $"VALUES ({id}, {user}, '{date.ToString(LocalDateFormat)}', {(int)OrderStatus.New}, {storeId})";

            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }

        public void InitializeTemporaryItemsTable()
        {
            TemporaryItemsTable = ItemsTable.Copy();
        }

        public DataTable GetTemporaryItemsTable()
        {
            return TemporaryItemsTable;
        }



        public void AddOrderItemsToDb()
        {
            foreach (DataRow row in TemporaryItemsTable.Rows)
            {
                if (!ItemsTable.Select().Any(v => Convert.ToInt16(v["Id"]) == Convert.ToInt16(row["Id"])))
                {
                    MySQL = "Insert into TOrderList(Id, OrderId, GoodsId) " +
                                $"Values ({row["Id"]}, {row["OrderId"]}, {row["GoodsId"]})";

                    SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
                    sqlCommand.ExecuteNonQuery();
                }
            }

            ClearItems();
        }

        public void AddTemporaryOrderItem(int orderId, int goodsId)
        {
            DataRow row = TemporaryItemsTable.NewRow();
            row["Id"] = ++maxItemsId;
            row["OrderId"] = orderId;
            row["GoodsId"] = goodsId;
            TemporaryItemsTable.Rows.Add(InitializeRow(row));
        }

        public DataRow InitializeRow(DataRow row)
        {
            row["Manufacture"] = (from r in Manager.Instance.Bikes.GetTable().Select()
                          where r.Field<int>("Id") == Convert.ToInt32(row["GoodsId"])
                          select r.Field<string>("Manufacture")).FirstOrDefault();

            row["Name"] = (from r in Manager.Instance.Bikes.GetTable().Select()
                           where r.Field<int>("Id") == Convert.ToInt32(row["GoodsId"])
                           select r.Field<string>("Name")).FirstOrDefault();

            row["Color"] = (from r in Manager.Instance.Bikes.GetTable().Select()
                            where r.Field<int>("Id") == Convert.ToInt32(row["GoodsId"])
                            select r.Field<string>("Color")).FirstOrDefault();

            row["Year"] = (from r in Manager.Instance.Bikes.GetTable().Select()
                           where r.Field<int>("Id") == Convert.ToInt32(row["GoodsId"])
                           select r.Field<int>("Year")).FirstOrDefault();

            row["Price"] = (from r in Manager.Instance.Bikes.GetTable().Select()
                            where r.Field<int>("Id") == Convert.ToInt32(row["GoodsId"])
                            select r.Field<decimal>("Price")).FirstOrDefault();

            row["Store"] = (from r in Manager.Instance.Bikes.GetTable().Select()
                            where r.Field<int>("Id") == Convert.ToInt32(row["GoodsId"])
                            select r.Field<string>("Store")).FirstOrDefault();

            return row;
        }

        public void ClearItems()
        {
            ItemsTable.Clear();
            TemporaryItemsTable.Clear();
        }
    }
}
