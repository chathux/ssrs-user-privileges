using SSRSUserPrivileges.GUI;
using SSRSUserPrivileges.SSRSWebService2012;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SSRSUserPrivileges
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //grantPriviledges_1();

            //grantPriviledges_2();
            btnRemoveSelectedUsers.IsEnabled = false;
            btnRemoveSelectedReports.IsEnabled = false;

            ((INotifyCollectionChanged)lvReports.Items).CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e)=>
            {
                //enable/disable grant privi
            };

            ((INotifyCollectionChanged)lvUsers.Items).CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e)=>
            {
                //enable/disable grant privi               
            };

            lvReports.SelectionChanged += (object sender, SelectionChangedEventArgs e)=>
            {
                if (lvReports.SelectedItem == null)
                {
                    btnRemoveSelectedReports.IsEnabled = false;
                }
                else
                {
                    btnRemoveSelectedReports.IsEnabled = true;
                }
            };

            lvUsers.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                if (lvUsers.SelectedItem == null)
                {
                    btnRemoveSelectedUsers.IsEnabled = false;
                }
                else
                {
                    btnRemoveSelectedUsers.IsEnabled = true;
                }
            };


        }




        private void test_1()
        {


            NetworkCredential clientCredentials = new NetworkCredential("Chathuranga", "blahhhhh", "Chathu-Nable");


            ReportingService2010SoapClient client = new ReportingService2010SoapClient();
            client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            client.ClientCredentials.Windows.ClientCredential = clientCredentials;





            client.Open();
            TrustedUserHeader t = new TrustedUserHeader();



            CatalogItem[] items;
            // I need to list of children of a specified folder.
            ServerInfoHeader oServerInfoHeader = client.ListChildren(t, "/", true, out items);
            foreach (var item in items)
            {
                // I can access any properties of item
                Console.WriteLine(item.ID);
            }

            Console.ReadLine();

        }

        private void grantPriviledges_1()
        {


            NetworkCredential clientCredentials = new NetworkCredential("Test", "123", "Chathu-Nable");


            ReportingService2010SoapClient client = new ReportingService2010SoapClient();
            client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            client.ClientCredentials.Windows.ClientCredential = clientCredentials;





            client.Open();
            TrustedUserHeader t = new TrustedUserHeader();







            Policy[] MyPolicyArr = new Policy[2];


            Role[] MyRoles = new Role[3];

            Role browser = new Role();
            browser.Name = "Browser";

            Role myReports = new Role();
            myReports.Name = "My Reports";

            Role publisher = new Role();
            publisher.Name = "Publisher";

            Role contentManager = new Role() { Name = "Content Manager" };

            MyRoles[0] = publisher;
            MyRoles[1] = browser;
            MyRoles[2] = contentManager;


            MyPolicyArr[0] = new Policy() { GroupUserName = @"Chathu-Nable\Test", Roles = MyRoles };
            MyPolicyArr[1] = new Policy() { GroupUserName = @"Chathu-Nable\Test2", Roles = MyRoles };


            client.SetPolicies(t, "/New Folder", MyPolicyArr);

            //rs.SetPolicies("/SecurityTestFolder", MyPolicyArr);


            

            MessageBox.Show("blahhhh");
        }


        private void grantPriviledges_2()
        {


            NetworkCredential clientCredentials = new NetworkCredential("Test", "123", "Chathu-Nable");


            ReportingService2010SoapClient client = new ReportingService2010SoapClient();
            client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            client.ClientCredentials.Windows.ClientCredential = clientCredentials;





            client.Open();
            TrustedUserHeader t = new TrustedUserHeader();


            Policy []arrPolicies = null;
            bool doesInheritParent = false;

            client.GetPolicies(t, "/New Folder", out arrPolicies, out doesInheritParent);


            List<Policy> listPolicies = new List<Policy>();
            if (arrPolicies != null)
            {
                listPolicies = arrPolicies.ToList();
            }


            listPolicies.Add(
                new Policy() { Roles = new[] { new Role() { Name = "Browser" }, new Role() { Name = "Content Manager" } }, GroupUserName = "Test2" }
            );


            client.SetPolicies(t, "/New Folder", listPolicies.ToArray());



            /*

            Policy[] MyPolicyArr = new Policy[2];


            Role[] MyRoles = new Role[3];

            Role browser = new Role();
            browser.Name = "Browser";

            Role myReports = new Role();
            myReports.Name = "My Reports";

            Role publisher = new Role();
            publisher.Name = "Publisher";

            Role contentManager = new Role() { Name = "Content Manager" };

            MyRoles[0] = publisher;
            MyRoles[1] = browser;
            MyRoles[2] = contentManager;


            MyPolicyArr[0] = new Policy() { GroupUserName = @"Chathu-Nable\Test", Roles = MyRoles };
            MyPolicyArr[1] = new Policy() { GroupUserName = @"Chathu-Nable\Test2", Roles = MyRoles };


            client.SetPolicies(t, "/New Folder", MyPolicyArr);

            //rs.SetPolicies("/SecurityTestFolder", MyPolicyArr);

            */


            //MessageBox.Show(policies.Length + "") ;
        }

        private void btnAddReports_Click(object sender, RoutedEventArgs e)
        {
            string[] selectedPaths = new AddReports().ShowAddReportsDialog();

            foreach (string path in selectedPaths)
            {
                if (!lvReports.Items.Contains(path))
                {
                    lvReports.Items.Add(path);
                }
            }

            
        }

        private void btnRemoveSelectedReports_Click(object sender, RoutedEventArgs e)
        {

            List<object> delete_list = new List<object>();
            foreach (object obj in lvReports.SelectedItems)
            {
                delete_list.Add(obj);
            }

            foreach (object obj in delete_list)
            {
                lvReports.Items.Remove(obj);
            }

        }

        private void btnAddSSRSUsers_Click(object sender, RoutedEventArgs e)
        {
            new AddSSRSUsers().ShowDialog();
        }

        private void btnAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            string []userNames = new AddUsers().ShowAddUsersDialog();

            foreach (string userName in userNames)
            {
                if (!lvUsers.Items.Contains(userName))
                {
                    lvUsers.Items.Add(userName);
                }
            }
        }

        private void btnRemoveSelectedUsers_Click(object sender, RoutedEventArgs e)
        {
            List<object> delete_list = new List<object>();
            foreach (object obj in lvUsers.SelectedItems)
            {
                delete_list.Add(obj);
            }

            foreach (object obj in delete_list)
            {
                lvUsers.Items.Remove(obj);
            }
        }

        private void btnGrantPriviledges_Click(object sender, RoutedEventArgs e)
        {
            List<string> listReports = new List<string>();
            List<string> listUsers = new List<string>();
            List<string> listRoles = new List<string>();

            foreach(object objReport in lvReports.Items)
            {
                string strReportPath = objReport as string;

                listReports.Add(strReportPath);
            }

            foreach (object objUser in lvUsers.Items)
            {
                string strUser = objUser as string;

                listUsers.Add(strUser);
            }

            if (chkRoleBrowser.IsChecked.Value)
            {
                listRoles.Add("Browser");
            }

            if (chkRoleContentManager.IsChecked.Value)
            {
                listRoles.Add("Content Manager");
            }


            if (chkRoleMyReports.IsChecked.Value)
            {
                listRoles.Add("My Reports");
            }

            if (chkRolePublisher.IsChecked.Value)
            {
                listRoles.Add("Publisher");
            }

            if (chkRoleReportBuilder.IsChecked.Value)
            {
                listRoles.Add("Report Builder");
            }


            SSRSServiceHandler.CurrentInstance.GrantPriviledges(listReports, listUsers, listRoles);
        }
    }
}
