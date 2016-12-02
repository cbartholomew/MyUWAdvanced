using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uwWebRequest.Classes;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace MyUWAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRequest();
        }

        static void TestRequest() 
        {

            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "https://advance.admin.washington.edu/advdb/action.aspx?treestate=--11-&PageId=50002&AppId=80900&idnumber=0001003621");

            Thread.Sleep(2000);

            SendKeys.Send("^{U}");

        }

    }
}
