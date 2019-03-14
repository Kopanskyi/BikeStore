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
        int OrderId;

        public int ClientId;
        public DateTime DateTime;
        DataTable OrderTable;

        public WNewOrder(int id)
        {
            OrderId = id;
            IsNew = OrderId == 0;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OrderTable = Manager.Instance.Orders.GetOrdersTable();

            cmbStores.DataContext = Manager.Instance.Stores.GetTable();
            cmbStores.DisplayMemberPath = "Name";
            cmbStores.SelectedValuePath = "Id";

            if (IsNew)
            {
                lblOrderId.Content = Manager.Instance.Orders.GetNewOrderId();

                dtpDate.DisplayDate = DateTime.Now;
                dtpDate.SelectedDate = DateTime.Now;

            }
            else
            {
                lblOrderId.Content = OrderId;
                cmbUsers.SelectedValue = ClientId;
                dtpDate.SelectedDate = DateTime;
                dtpDate.DisplayDate = DateTime;


                //int storeId = (from r in OrderTable.Select()
                //         where r.Field<int>("Id") == OrderId
                //         select r.Field<int>("StoreId")).First();

                cmbStores.IsEnabled = false;

                cmbStores.SelectedValue = (from r in OrderTable.Select()
                                          where r.Field<int>("Id") == OrderId
                                          select r.Field<int>("StoreId")).First();
            }


            GridOrderItems.ItemsSource = Manager.Instance.Orders.GetItemsTable(OrderId).DefaultView;
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

            WinBikesList bikesList = new WinBikesList(Convert.ToInt32(lblOrderId.Content), 
                Convert.ToInt32(cmbStores.SelectedValue));
            bikesList.ShowDialog();

            //ShowItems();

            GridOrderItems.ItemsSource = Manager.Instance.Orders.GetTemporaryItemsTable().DefaultView;
            GridOrderItemsSetup();
        }

        private void ShowItems()
        {
            GridOrderItems.ItemsSource = Manager.Instance.Orders.GetItemsTable(OrderId).DefaultView;
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
