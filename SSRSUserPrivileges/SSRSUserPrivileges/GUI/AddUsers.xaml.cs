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

namespace SSRSUserPrivileges.GUI
{
    /// <summary>
    /// Interaction logic for AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Window
    {
        public AddUsers()
        {
            InitializeComponent();
        }


        public string[] ShowAddUsersDialog()
        {
            Nullable<bool> res = ShowDialog();

            List<string> listUsers = new List<string>();
            if (res.Value)
            {
                string []names = txtUsers.Text.Trim().Split(new char[] { ' ', ',', '\n'});

                return names.Where((string name) =>
                {
                    if(String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
                    {
                        return false;
                    }

                    return true;
                }).ToArray();
            }


            return new string[]{};
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
