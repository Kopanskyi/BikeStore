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
    public partial class WNewStore : Window
    {
        int id;

        public WNewStore(int Id = 0)
        {
            InitializeComponent();
            id = Id;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (id > 0)
            {
                Manager.Instance.Stores.Edit(id, txtName.Text, txtAddress.Text, txtPhone.Text);
            }
            else
            {
                Manager.Instance.Stores.Add(txtName.Text, txtAddress.Text, txtPhone.Text);
            }

            Close();
        }
    }
}
