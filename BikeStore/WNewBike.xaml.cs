using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace BikeStore
{
    /// <summary>
    /// Interaction logic for WNewBike.xaml
    /// </summary>
    public partial class WNewBike : Window
    {
        int Id;

        public WNewBike(int id = 0)
        {
            InitializeComponent();
            Id = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbManufacture.DataContext = Manager.Instance.Manufactures.GetTable();
            cmbManufacture.DisplayMemberPath = "Name";
            cmbManufacture.SelectedValuePath = "Id";

            cmbYear.DataContext = Manager.Instance.Year.GetTable();
            cmbYear.DisplayMemberPath = "Year";
            cmbYear.SelectedValuePath = "Id";

            cmbColor.DataContext = Manager.Instance.Colors.GetTable();
            cmbColor.DisplayMemberPath = "Name";
            cmbColor.SelectedValuePath = "Id";

            cmbStores.DataContext = Manager.Instance.Stores.GetTable();
            cmbStores.DisplayMemberPath = "Name";
            cmbStores.SelectedValuePath = "Id";

            cmbModel.DataContext = Manager.Instance.Models.GetTable();
            cmbModel.DisplayMemberPath = "Name";
            cmbModel.SelectedValuePath = "Id";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Id > 0)
            {
                Manager.Instance.Bikes.Edit(Id, Convert.ToInt32(cmbStores.SelectedValue),
                    Convert.ToInt32(cmbModel.SelectedValue), Convert.ToDecimal(Price.Text),
                    Description.Text, Convert.ToInt32(cmbColor.SelectedValue),
                    Convert.ToInt32(cmbYear.SelectedValue));
            }
            else
            {
                Manager.Instance.Bikes.Add(Convert.ToInt32(cmbStores.SelectedValue),
                    Convert.ToInt32(cmbModel.SelectedValue), Convert.ToDecimal(Price.Text),
                    Description.Text, Convert.ToInt32(cmbColor.SelectedValue),
                    Convert.ToInt32(cmbYear.SelectedValue));
            }
            Close();
        }

        private void CmbManufacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbModel.DataContext = Manager.Instance.Models.GetTable(Convert.ToInt32(cmbManufacture.SelectedValue));
            cmbModel.DisplayMemberPath = "Name";
            cmbModel.SelectedValuePath = "Id";
        }
    }
}
