using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAdvance.Classes
{
    public class Advanced
    {
        public enum TREE_TYPE
        {
            EMAIL_ACTIVE,
            EMAIL_INACTIVE
        }

        public const string ENDPOINT_BASE = "https://advance.admin.washington.edu/advdb/action.aspx?";
        public const string EMAIL_ENDPOINT_TEMPLATE = "treestate=--11-&PageId=50002&AppId=80900&idnumber=#IDNUMBER#";
        public Dictionary<string,bool> emailHeaderIdentities { get; set; }

        public string elementOne { get; set; }
        public string elementTwo { get; set; }
        public string elementThree { get; set; }
        public string itemStatus { get; set; }
        public string fieldNameOne { get; set; }
        public string fieldNameTwo { get; set; }
        
        public Advanced(TREE_TYPE tree) 
        {
            loadEmailSettings();
            setEmailMeta(tree);
        }

        public static string GetTreeTypeStatus(TREE_TYPE treeType)
        {
            string output = "";
            switch (treeType)
            {
                case TREE_TYPE.EMAIL_ACTIVE:
                    output = "Active";
                    break;
                case TREE_TYPE.EMAIL_INACTIVE:
                    output = "Past";
                    break;
            }
            return output;
        }

        public string GetNewEndpoint(string idNumber, TREE_TYPE tree)
        {
            string output = "";

            switch (tree)
            {
                case TREE_TYPE.EMAIL_ACTIVE:
                    output = String.Concat(ENDPOINT_BASE, 
                        EMAIL_ENDPOINT_TEMPLATE.Replace("#IDNUMBER#", idNumber)
                    );
                    break;
                case TREE_TYPE.EMAIL_INACTIVE:
                      output = String.Concat(ENDPOINT_BASE, 
                        EMAIL_ENDPOINT_TEMPLATE.Replace("#IDNUMBER#", idNumber)
                    );
                    break;
                default:
                    break;
            }

            return output;
        }
        
            private void loadEmailSettings()
        {
            this.emailHeaderIdentities = new Dictionary<string, bool>() 
            { 
                { "rw1_header_email"    , true },
                { "rw2_header_email"    , true },
                { "rw3_header_email"    , true },
                { "rw4_header_email"    , true },
                { "rw5_header_email"    , true },
                { "rw6_header_email"    , true },
                { "rw7_header_email"    , true },
                { "rw8_header_email"    , true },
                { "rw9_header_email"    , true },
                { "rw10_header_email"   , true },
                { "rw11_header_email"   , true },
                { "rw12_header_email"   , true },
                { "rw13_header_email"   , true },
                { "rw14_header_email"   , true },
                { "rw15_header_email"   , true },
                { "rw17_header_email"   , true },
                { "rw18_header_email"   , true },
                { "rw19_header_email"   , true },
                { "rw20_header_email"   , true }            
            };
        }

        private void setEmailMeta(TREE_TYPE tree)
        {
            switch (tree)
            {
                case TREE_TYPE.EMAIL_ACTIVE:
                    this.elementOne = "div";
                    this.elementTwo = "span";
                    this.itemStatus = "Active";
                    this.fieldNameOne = "80925.EMAIL_ADDRESS";
                    break;
                case TREE_TYPE.EMAIL_INACTIVE:
                    this.elementOne = "div";
                    this.elementTwo = "span";
                    this.itemStatus = "Past";
                    this.fieldNameOne = "80925.EMAIL_ADDRESS";
                    break;
                default:
                    break;
            }
        }
    }
}
