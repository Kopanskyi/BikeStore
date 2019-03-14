using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public class Bikes
    {
        string MySQL;
        public DataTable Table;

        //SqlDataAdapter adapter;
        //SqlCommandBuilder builder;


        public DataTable GetTable(int storeId = 0)
        {
            //MySQL = "Select TBikes.Id, TManufacture.Name as Manufacture, TModel.Name, TModel.[Desc], " +
            //    "TColors.Name as Color, TYear.Year, TBikes.Price, TStores.Name as Store " +
            //    "From TBikes, TModel, TStores, TManufacture, TColors, TYear " +
            //    "WHERE TManufacture.Id = TModel.ManufactureId " +
            //    "and TColors.Id = TBikes.ColorId " +
            //    "and TYear.Id = TBikes.YearId " +
            //    "and TBikes.ModelId = TModel.Id " +
            //    "and TBikes.StoreId = TStores.Id " +
            //    "ORDER BY TBikes.Price";

            Table = new DataTable();
            //adapter = new SqlDataAdapter(MySQL, Manager.Instance.Connect);
            //adapter.Fill(Table);

            MySQL = "SelectBikesFromStore";
            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter sqlParameter = new SqlParameter("@StoreId", storeId);
            sqlCommand.Parameters.Add(sqlParameter);

            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            Table.Load(dataReader);

            return Table;
        }

        public void Delete(int row)
        {
            MySQL = $"delete from TBikes where Id = {row}";
            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }

        private int GetMaxId()
        {
            MySQL = "select MAX(Id) from TBikes";
            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            return (Int32)sqlCommand.ExecuteScalar();
        }

        public void Add(int store, int model, decimal price, string desc, int color, int year)
        {
            //MySQL = "Insert into TBikes(Id, StoreId, ModelId, Price, Description, ColorId, YearId) " +
            //    $"Values({GetMaxId() + 1}, {store}, {model}, {price}, '{desc}', {color}, {year})";

            MySQL = "AddBike";

            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter idParameter = new SqlParameter("@Id", GetMaxId() + 1);
            sqlCommand.Parameters.Add(idParameter);

            SqlParameter storeParameter = new SqlParameter("@StoreId", store);
            sqlCommand.Parameters.Add(storeParameter);

            SqlParameter modelParameter = new SqlParameter("@ModelId", model);
            sqlCommand.Parameters.Add(modelParameter);

            SqlParameter priceParameter = new SqlParameter("@Price", price);
            sqlCommand.Parameters.Add(priceParameter);

            SqlParameter descParameter = new SqlParameter("@Description", desc);
            sqlCommand.Parameters.Add(descParameter);

            SqlParameter colorParameter = new SqlParameter("@ColorId", color);
            sqlCommand.Parameters.Add(colorParameter);

            SqlParameter yearParameter = new SqlParameter("@YearId", year);
            sqlCommand.Parameters.Add(yearParameter);

            sqlCommand.ExecuteNonQuery();
        }

        public void Edit(int id, int store, int model, decimal price, string desc, int color, int year)
        {
            MySQL = "Update TBikes " +
                $"set StoreId = {store}, " +
                $"ModelId = {model}, " +
                $"Price = {price}, " +
                $"Description = '{desc}', " +
                $"ColorId = {color}, " +
                $"YearId = {year} " +
                $"where Id = {id}";

            SqlCommand sqlCommand = new SqlCommand(MySQL, Manager.Instance.Connect);
            sqlCommand.ExecuteNonQuery();
        }

    }
}
