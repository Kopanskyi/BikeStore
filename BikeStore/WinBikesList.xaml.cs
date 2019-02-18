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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataManager;
using System.Data;
using System.Data.SqlClient;

namespace BikeStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WinBikesList : Window
    {
        int OrderId;
        int StoreId;

        public WinBikesList(int orderId, int storeId)
        {
            InitializeComponent();
            OrderId = orderId;
            StoreId = storeId;
        }       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DBGrid.ItemsSource =  Manager.Instance.Bikes.GetTable(StoreId).DefaultView;          
        }

        private void DBGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int goodsId = Convert.ToInt16(((DataRowView)DBGrid.SelectedItems[0]).Row["Id"]);
            Manager.Instance.Orders.AddTemporaryOrderItem(OrderId, goodsId);
            Close();
        }

        private void BtnAddToOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
