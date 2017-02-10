using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAdvance.Classes
{
    public class SalesForce
    {
        public const string ENDPOINT_BASE = "https://na43.salesforce.com";
        public const string ENDPOINT_SEARCH_TEMPLATE = "/_ui/search/ui/UnifiedSearchResults?searchType=2&sen=001&sen=003&sen=701&sen=00O&str=#IDNUMBER#";
        public Dictionary<string, bool> emailHeaderIdentities { get; set; }
        public List<string> emailRecordList { get; set; }
        public string endpointEditSearchFieldTemplate { get; set; }
        public string elementOne { get; set; }
        public string elementTwo { get; set; }
        public string elementThree { get; set; }
        public string oldEmail { get; set; }
        public string idNumber { get; set; }
        
        public TREE_TYPE treeType { get; set; }
        public enum TREE_TYPE
        {
            PERSON_SEARCH,
            PERSON_EDIT
        }

        public SalesForce(TREE_TYPE tree) {
            this.treeType = tree;
            this.emailRecordList = new List<string>();
            setEmailMeta(tree);
            loadEmailSettings();


        }

        public string GetNewEndpoint(string idNumber)
        {
            string output = "";

            output = String.Concat(ENDPOINT_BASE,
                        ENDPOINT_SEARCH_TEMPLATE.Replace("#IDNUMBER#", idNumber));

            return output;
        }

        public string GetEditEndpoint()
        {
            string output = "";

            output = String.Concat(ENDPOINT_BASE,
                        this.endpointEditSearchFieldTemplate);

            return output;
        }


        private void loadEmailSettings()
        {
            this.emailHeaderIdentities = new Dictionary<string, bool>() 
            { 
                { "con15"    , true },
                { "00NF000000CYld5"    , true },
                { "00NF000000CYldP"    , true },
                { "00NF000000CYldU"    , true },
                { "00NF000000Caw7K"    , true },
                { "00NF000000Caw7P"    , true }     
            };
        }

        private void setEmailMeta(TREE_TYPE tree)
        {
            switch (tree)
            {
                case TREE_TYPE.PERSON_SEARCH:
                    this.elementOne = ".actionLink";
                    this.elementTwo = "span";

                    break;
                case TREE_TYPE.PERSON_EDIT:
                    this.elementOne = "div";
                    this.elementTwo = "span";

                    break;
                default:
                    break;
            }
        }

        public enum EMAIL_WEIGHT
        {
            UW = 0,
            CS = 0,
            MATH = 0,
            AMAZON = 0,
            OTHER = 1,
            COMCAST = 2,
            HOTMAIL = 3,
            YAHOO = 4,
            LIVE = 5,
            GMAIL = 6
        }

        public static EMAIL_WEIGHT GetWeight(string email)
        {
            if (email == "UW" ||
                email == "CS" ||
                email == "MATH")
            {
                return EMAIL_WEIGHT.UW;
            }
            switch (email)
            {
                case "GMAIL":
                    return EMAIL_WEIGHT.GMAIL;
                case "LIVE":
                    return EMAIL_WEIGHT.LIVE;
                case "YAHOO":
                    return EMAIL_WEIGHT.YAHOO;
                case "HOTMAIL":
                    return EMAIL_WEIGHT.HOTMAIL;
                default:
                    return EMAIL_WEIGHT.OTHER;
            }

        }

        public SalesForce DeepCopy()
        {
            SalesForce other = (SalesForce)this.MemberwiseClone();
            other.emailRecordList = new List<string>();            
            return other;
        }
     
    }
}
