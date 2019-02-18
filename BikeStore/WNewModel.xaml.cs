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

namespace BikeStore
{
    /// <summary>
    /// Interaction logic for WNewModel.xaml
    /// </summary>
    public partial class WNewModel : Window
    {
        int id;

        public WNewModel(int Id = 0)
        {
            InitializeComponent();
            id = Id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbManufacture.DataContext = Manager.Instance.Manufactures.GetTable();
            cmbManufacture.DisplayMemberPath = "Name";
            cmbManufacture.SelectedValuePath = "Id";

            cmbCountry.DataContext = Manager.Instance.ProducingCountry.GetTable();
            cmbCountry.DisplayMemberPath = "Name";
            cmbCountry.SelectedValuePath = "Id";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (id > 0)
            {
                Manager.Instance.Models.Edit(id, Convert.ToInt32(cmbManufacture.SelectedValue), txtName.Text,
                    txtDescription.Text, Convert.ToInt32(cmbCountry.SelectedValue));
            }
            else
            {
                Manager.Instance.Models.Add(Convert.ToInt32(cmbManufacture.SelectedValue), txtName.Text,
                    txtDescription.Text, Convert.ToInt32(cmbCountry.SelectedValue));
            }

            Close();
        }
    }
}
