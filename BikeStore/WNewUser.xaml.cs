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
    public partial class WNewUser : Window
    {
        int id;

        public WNewUser(int Id = 0)
        {
            InitializeComponent();
            id = Id;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (id > 0)
            {
                Manager.Instance.Users.Edit(id, txtName.Text, txtPhone.Text);
            }
            else
            {
                Manager.Instance.Users.Add(txtName.Text, txtPhone.Text);
            }

            Close();
        }
    }
}
