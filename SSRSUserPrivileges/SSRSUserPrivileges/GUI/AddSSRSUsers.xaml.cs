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
    /// Interaction logic for AddSSRSUsers.xaml
    /// </summary>
    public partial class AddSSRSUsers : Window
    {

        private SSRSServiceHandler handler;

        public AddSSRSUsers()
        {

            InitializeComponent();

            handler = SSRSServiceHandler.CurrentInstance;

        }
    }
}
