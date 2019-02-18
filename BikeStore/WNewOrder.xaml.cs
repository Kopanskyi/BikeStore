using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataManager;
using System.Data;

namespace BikeStore
{
    /// <summary>
    /// Interaction logic for WNewOrder.xaml
    /// </summary>
    public partial class WNewOrder : Window
    {
        bool IsNew = false;
        public int Id;
        public int ClientId;
        public DateTime DateTime;
        DataTable OrderTable;

        public WNewOrder(int id)
        {
            Id = id;
            IsNew = Id == 0;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OrderTable = Manager.Instance.Orders.GetOrdersTable();


            if (IsNew)
            {
                lblOrderId.Content = Manager.Instance.Orders.GetNewOrderId();

                dtpDate.DisplayDate = DateTime.Now;
                dtpDate.SelectedDate = DateTime.Now;

                cmbStores.DataContext = Manager.Instance.Stores.GetTable();
                cmbStores.DisplayMemberPath = "Name";
                cmbStores.SelectedValuePath = "Id";
            }
            else
            {
                lblOrderId.Content = Id;
                cmbUsers.SelectedValue = ClientId;
                dtpDate.SelectedDate = DateTime;
                dtpDate.DisplayDate = DateTime;

                cmbStores.DisplayMemberPath = "Name";
                cmbStores.SelectedValuePath = "Id";
                cmbStores.SelectedValue = from r in OrderTable.Select()
                                          where r.Field<int>("Id") == Id
                                          select r.Field<int>("StoreId");

                //cmbStores.SelectedItem = from r in Manager.Instance.Stores.GetTable().Select()
                //                         where r.Field<int>("Id") == Convert.ToInt32(cmbStores.SelectedValue)
                //                         select r.Field<string>("Name");


            }


            GridOrderItems.ItemsSource = Manager.Instance.Orders.GetItemsTable(Id).DefaultView;
            Manager.Instance.Orders.GetNewItemsId();
            Manager.Instance.Orders.InitializeTemporaryItemsTable();

            cmbUsers.DataContext = Manager.Instance.Users.GetTable();
            cmbUsers.DisplayMemberPath = "Name";
            cmbUsers.SelectedValuePath = "Id";

            GridOrderItemsSetup();
        }

        private void GridOrderItemsSetup()
        {
            GridOrderItems.Columns[0].Visibility = Visibility.Hidden;
            GridOrderItems.Columns[1].Visibility = Visibility.Hidden;
            GridOrderItems.Columns[2].Visibility = Visibility.Hidden;
        }

        private void BtnAdd_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            WinBikesList bikesList = new WinBikesList(Convert.ToInt16(lblOrderId.Content), 
                Convert.ToInt16(cmbStores.SelectedValue));
            bikesList.ShowDialog();

            //ShowItems();

            GridOrderItems.ItemsSource = Manager.Instance.Orders.GetTemporaryItemsTable().DefaultView;
            GridOrderItemsSetup();
        }

        private void ShowItems()
        {
            GridOrderItems.ItemsSource = Manager.Instance.Orders.GetItemsTable(Id).DefaultView;
        }

        private void BtnOk_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsNew)
            {
                Manager.Instance.Orders.CreateNewOrder(Convert.ToInt16(lblOrderId.Content),
                    Convert.ToInt16(cmbUsers.SelectedValue), (DateTime)dtpDate.SelectedDate, 
                    Convert.ToInt16(cmbStores.SelectedValue));
            }

            Manager.Instance.Orders.AddOrderItemsToDb();

            Close();
        }
    }
}
