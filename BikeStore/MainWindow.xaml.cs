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
using ReportManager;

namespace BikeStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Manager.Instance.Users.GetTable();

            BtnStores_Click(new object(), new RoutedEventArgs());
        }

        private void BtnStores_Click(object sender, RoutedEventArgs e)
        {
            DBGrid.ItemsSource = Manager.Instance.Stores.GetTable().DefaultView;
            Manager.Instance.ActiveTable = Active.Stores;
        }

        private void BtnBikes_Click(object sender, RoutedEventArgs e)
        {
            DBGrid.ItemsSource = Manager.Instance.Bikes.GetTable().DefaultView;
            Manager.Instance.ActiveTable = Active.Bikes;
        }

        private void BtnModels_Click(object sender, RoutedEventArgs e)
        {
            DBGrid.ItemsSource = Manager.Instance.Models.GetTable().DefaultView;
            Manager.Instance.ActiveTable = Active.Models;
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            DBGrid.ItemsSource = Manager.Instance.Orders.GetOrdersTable().DefaultView;
            Manager.Instance.ActiveTable = Active.Orders;
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            DBGrid.ItemsSource = Manager.Instance.Users.GetTable().DefaultView;
            Manager.Instance.ActiveTable = Active.Users;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (Manager.Instance.ActiveTable)
            {
                case Active.Stores:
                    {
                        WNewStore newStore = new WNewStore();
                        newStore.ShowDialog();
                        BtnStores_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Bikes:
                    {
                        WNewBike addBike = new WNewBike();
                        addBike.ShowDialog();
                        BtnBikes_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Models:
                    {
                        WNewModel wNewModel = new WNewModel();
                        wNewModel.ShowDialog();
                        BtnModels_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Users:
                    {
                        WNewUser newUser = new WNewUser();
                        newUser.ShowDialog();
                        BtnUser_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Orders:
                    {
                        WNewOrder newOrder = new WNewOrder(0);
                        newOrder.ShowDialog();
                        BtnOrders_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                default:
                    break;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt16(((DataRowView)DBGrid.SelectedItems[0]).Row["Id"].ToString());
            
            switch (Manager.Instance.ActiveTable)
            {
                case Active.Stores:
                    {
                        WNewStore editStore = new WNewStore(id);
                        editStore.txtName.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Name"].ToString();
                        editStore.txtAddress.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Address"].ToString();
                        editStore.txtPhone.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Phone"].ToString();
                        editStore.ShowDialog();
                        BtnStores_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Bikes:
                    {
                        WNewBike editBike = new WNewBike(id);
                        //editBike.BikeName.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Name"].ToString();
                        editBike.Price.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Price"].ToString();
                        //editBike.Stores.SelectedValue = ((DataRowView)DBGrid.SelectedItems[0]).Row["Store"].ToString() as object;
                        editBike.Description.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Status"].ToString();
                        editBike.ShowDialog();
                        BtnBikes_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Models:
                    {
                        WNewModel editModel = new WNewModel(id);
                        editModel.txtName.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Name"].ToString();
                        editModel.txtDescription.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Desc"].ToString();
                        editModel.ShowDialog();
                        BtnModels_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Users:
                    {
                        WNewUser editUser = new WNewUser(id);
                        editUser.txtName.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Name"].ToString();
                        editUser.txtPhone.Text = ((DataRowView)DBGrid.SelectedItems[0]).Row["Phone"].ToString();
                        editUser.ShowDialog();
                        BtnUser_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Orders:
                    {
                        //int id  = Convert.ToInt16(((DataRowView)DBGrid.SelectedItems[0]).Row["Id"].ToString());
                        WNewOrder newOrder = new WNewOrder(id)
                        {
                            ClientId = Convert.ToInt16(((DataRowView)DBGrid.SelectedItems[0]).Row["UserId"].ToString()),
                            DateTime = Convert.ToDateTime(((DataRowView)DBGrid.SelectedItems[0]).Row["Date"])
                        };
                        newOrder.ShowDialog();
                    }
                    break;
                default:
                    break;
            }

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt16(((DataRowView)DBGrid.SelectedItems[0]).Row["Id"].ToString());

            switch (Manager.Instance.ActiveTable)
            {
                case Active.Stores:
                    break;
                case Active.Bikes:
                    {
                        Manager.Instance.Bikes.Delete(id);
                        //DBGrid.ItemsSource = Manager.Instance.Bikes.GetTable().DefaultView;
                        BtnBikes_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Models:
                    {
                        Manager.Instance.Models.Delete(id);
                        BtnModels_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Users:
                    {
                        Manager.Instance.Users.Delete(id);
                        BtnUser_Click(new object(), new RoutedEventArgs());
                    }
                    break;
                case Active.Orders:
                    break;
                default:
                    break;
            }
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            switch (Manager.Instance.ActiveTable)
            {
                case Active.Stores:
                    break;
                case Active.Bikes:
                    {
                        Report.Instance.ExcelReport.CreateBikesList();
                        MessageBox.Show("Complete");
                    }
                    break;
                case Active.Models:
                    break;
                case Active.Users:
                    break;
                case Active.Orders:
                    break;
                default:
                    break;
            }

        }
    }
}
