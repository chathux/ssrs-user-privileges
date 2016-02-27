using SSRSUserPrivileges.Model;
using SSRSUserPrivileges.SSRSWebService2012;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SSRSUserPrivileges
{
    public class SSRSServiceHandler
    {
        private static SSRSServiceHandler currentInstance;

        private ReportingService2010SoapClient serviceClient;
        private TrustedUserHeader userHeader;

        private SSRSServiceHandler()
        {

            NetworkCredential clientCredentials = new NetworkCredential("Test", "123", "Chathu-Nable");


            serviceClient = new ReportingService2010SoapClient();
            serviceClient.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            serviceClient.ClientCredentials.Windows.ClientCredential = clientCredentials;
            serviceClient.Open();

            userHeader = new TrustedUserHeader();

        }


        public static SSRSServiceHandler CurrentInstance {

            get
            {
                if (currentInstance == null)
                {
                    currentInstance = new SSRSServiceHandler();
                }

                return currentInstance;
            }
        }


        public List<CatalogItem> GetReportHierarchy()
        {

            List<ReportItem> liReportItems = new List<ReportItem>();

            CatalogItem[] catalogItems = null;
            serviceClient.ListChildren(userHeader, "/", true, out catalogItems);

            if (catalogItems != null)
            {
                return catalogItems.ToList();
            }

            return null;
        }


        public void GrantPriviledges(List<string> reportPaths, List<string> userNames, List<string> roles)
        {

            

            foreach (string reportPath in reportPaths)
            {

                List<Policy> listPolicies = new List<Policy>();

                bool doesInheritParent = false;
                Policy[] arrPolicies = null;

                serviceClient.GetPolicies(userHeader, reportPath , out arrPolicies, out doesInheritParent);

                if (arrPolicies != null)
                {
                    listPolicies.InsertRange(0, arrPolicies.ToList());
                }

                foreach (string userName in userNames)
                {

                    
                    //if the policy exist for the same user modify that policy object
                    Policy newPolicy = listPolicies.Find((Policy paramPolicy) =>
                    {
                        return paramPolicy.GroupUserName.ToLower().Equals(userName.ToLower());
                    });


                    if (newPolicy == null)
                    {
                        newPolicy = new Policy();

                        listPolicies.Add(newPolicy);
                    }

                    newPolicy.GroupUserName = userName;

                    List<Role> listCurrentRoles = newPolicy.Roles != null ? newPolicy.Roles.ToList() : new List<Role>();
                    foreach(string role in roles)
                    {
                        if(!listCurrentRoles.Exists((Role paramRole)=>{
                            return paramRole.Name.ToLower().Equals(role.ToLower());
                        }))
                        {
                            listCurrentRoles.Add(new Role(){Name = role});
                        }
                    }

                    newPolicy.Roles = listCurrentRoles.ToArray();

                }

                serviceClient.SetPolicies(userHeader, reportPath, listPolicies.ToArray());

            }

            
            /*
            listPolicies.Add(
                new Policy() { Roles = new[] { new Role() { Name = "Browser" }, new Role() { Name = "Content Manager" } }, GroupUserName = "Test2" }
            );


            client.SetPolicies(t, "/New Folder", listPolicies.ToArray());
            */

        }

    }
}
