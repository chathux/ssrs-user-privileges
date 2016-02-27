using SSRSUserPrivileges.Model;
using SSRSUserPrivileges.SSRSWebService2012;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class TreeViewReportNode
    {

        private ObservableCollection<TreeViewReportNode> children;

        public TreeViewReportNode()
        {
            children = new ObservableCollection<TreeViewReportNode>();
        }


        public bool IsChecked { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }


        public ObservableCollection<TreeViewReportNode> Children
        {
            get{
                return children;
            }

        }


    }
    /// <summary>
    /// Interaction logic for AddReports.xaml
    /// </summary>
    public partial class AddReports : Window
    {

        private ObservableCollection<TreeViewReportNode> items= new ObservableCollection<TreeViewReportNode>();
        private SSRSServiceHandler handler;
        public AddReports()
        {

            InitializeComponent();
            handler = SSRSServiceHandler.CurrentInstance;


            FillReportsTreeView();

        }

        private void FillReportsTreeView()
        {
            List<CatalogItem> liCatalogItems = handler.GetReportHierarchy();
            items.Clear();

            TreeViewReportNode rootNode = new TreeViewReportNode(){ Name = "/", Path="/"};
            items.Add(rootNode);

            //sort the list by path length

            liCatalogItems.Sort((CatalogItem item1, CatalogItem item2)=>{

                if (item1.Path.Split('/').Length > item2.Path.Split('/').Length)
                {
                    return 1;
                }
                else if (item2.Path.Split('/').Length > item1.Path.Split('/').Length)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });

            //flattening the hierarchies
            List<TreeViewReportNode> flatTreeNodeList = new List<TreeViewReportNode>();
            /*foreach (TreeViewReportNode item in items)
            {
                flatTreeNodeList.Add(item);
            }
            int i = 0;
            while (i < flatTreeNodeList.Count)
            {
                foreach (TreeViewReportNode node in flatTreeNodeList[i].Children)
                {
                    flatTreeNodeList.Add(node);
                }
                i++;
            }*/



            while( liCatalogItems.Count > 0)
            {
                CatalogItem cItem = liCatalogItems[0];
                
                //find the parent folder
                List<TreeViewReportNode> result = flatTreeNodeList.Where((TreeViewReportNode paramReportNode)=>
                {
                    //for this condition to be valid, liCatalogItems list have to be 
                    //sorted by no of '/' within the Path
                    //catalogitems with short path(Folders) should come first

                    if (cItem.Path.Equals(paramReportNode.Path + "/" + cItem.Name))
                    {
                        return true;
                    }
                    return false;
                }).ToList() ;


                TreeViewReportNode newNode = new TreeViewReportNode() { Path = cItem.Path, Name = cItem.Name };
                if (result.Count > 0)
                {
                    //add the item to the parent
                    result[0].Children.Add(newNode);                    
                }
                else
                {
                    //default parent is root
                    rootNode.Children.Add(newNode);
                }

                //maintaining a flat list
                flatTreeNodeList.Add(newNode);

                liCatalogItems.RemoveAt(0);
            }


        }

        public string[] ShowAddReportsDialog(){

            Nullable<bool> res = ShowDialog();
            List<String> selectedReports = new List<string>();

            if (res.Value)
            {
                
                List<TreeViewReportNode> listTemp = new List<TreeViewReportNode>();

                foreach (TreeViewReportNode item in items)
                {
                    listTemp.Add(item);
                }

                int i = 0;
                while (i < listTemp.Count)
                {
                    if (listTemp[i].IsChecked)
                    {
                        selectedReports.Add(listTemp[i].Path);
                    }

                    foreach (TreeViewReportNode node in listTemp[i].Children)
                    {
                        listTemp.Add(node);
                    }


                    i++;
                }

            }

            return selectedReports.ToArray();



        }


        public ObservableCollection<TreeViewReportNode> Items { 
            get{               
                return items;
            }
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
