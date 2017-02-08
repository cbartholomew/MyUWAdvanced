using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using WinAdvance.Configuration;
using WinAdvance.Settings;
using WinAdvance.Classes;
using WinAdvanced.Utility;

namespace WinAdvance
{
    public partial class frmMain : Form
    {
        public string excelFileLocation { get; set; }
        public bool breakOutExited { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // track enter and exit
            this.breakOutExited = false;

            if (lblExcelFileStatus.BackColor != Color.DarkGreen ||
               lblConfirmedWorksheet.BackColor != Color.DarkGreen ||
               lblAdvancedOptions.BackColor != Color.DarkGreen)
            {
                MessageBox.Show("Donna, you forgot a step make sure everything is green!", "Hey Donna... :-)");
                return;
            }

            KeyInput input = new KeyInput();
            ApplicationSettings settings 
                = new ApplicationSettings();

            // set csv output
            string csvOutput = settings.FILE_EXPORT_PATH; 
   
            // remove files
            CleanUp(settings.WEB_EXPORT_PATH);

            try
            {
                // on new run, remove output file
                if (File.Exists(csvOutput))
                {
                    File.Copy(csvOutput, csvOutput.Replace(".csv","_" + DateTime.Now.ToFileTime() + ".csv"));
                    File.Delete(csvOutput);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Donna, did you close the Output.csv file? Close it, then press OK.","Hey Donna... :-)");
                File.Copy(csvOutput, csvOutput.Replace(".csv", "_" + DateTime.Now.ToFileTime() + ".csv"));
                File.Delete(csvOutput);
            }


            // check what was chosen
            Advanced.TREE_TYPE treeChosen = (this.radioEmailInactive.Checked) 
                ? Advanced.TREE_TYPE.EMAIL_INACTIVE : Advanced.TREE_TYPE.EMAIL_ACTIVE;

            // based on the chosen value, load the new configuration
            Advanced advanced =
                    new Advanced(treeChosen);

            // exit if the file goes away
            if (!File.Exists(this.excelFileLocation)) {
                return;
            }

            List<string[]> idNumbers = uwExcel.createFromExcel(this.excelFileLocation, 
            "A", 
            "B", 
            txtWorksheetName.Text);

            // excel row number from original
            int rowIndexNo = 1;

            int startFromIndex = 0;
            int endIndexRow = 0;

            // startFrom
            if (settings.START_AT_ROW > 0)
            {
                startFromIndex = settings.START_AT_ROW;
            }
            
            // startFrom
            if (settings.END_AT_ROW > 0)
            {
                endIndexRow = settings.END_AT_ROW;
            }

            foreach (string[] person in idNumbers)
            {

                if (startFromIndex > 0) {
                    if (rowIndexNo - 1 <= startFromIndex) {
                        rowIndexNo++;
                        continue;
                    }
                }

                // skip if missing id number
                if (String.IsNullOrEmpty(person[0]))
                {
                    rowIndexNo++;
                    continue;
                }

                Process chrome = Process.Start(settings.DEFAULT_BROWSER_EXE,
                   advanced.GetNewEndpoint(person[0], treeChosen));

                Thread.Sleep(settings.DEFAULT_SLEEP_INTERVAL);

                foreach (var browserInstruction in input.InstructionsBrowser)
                {
                    SendKeys.SendWait(browserInstruction);

                    if (browserInstruction != "^{w}") { 
                        Thread.Sleep(settings.DEFAULT_SLEEP_INTERVAL);
                    }
                }

                // remove files
                CleanUp(settings.WEB_EXPORT_PATH);

                var html = Clipboard.GetText();

                Dictionary<string, EmailRecord> emailList = loadHtmlGetElementsBySelector(html, advanced);

                // if no emails returned skip
                if (emailList.Count == 0)
                {
                    rowIndexNo++;
                    continue;
                }

                // check index
                rowIndexNo++;

                // This text is always added, making the file longer over time
                // if it is not deleted.
                using (StreamWriter sw = File.AppendText(csvOutput))
                {
                    foreach (string key in emailList.Keys)
                    {                        
                        EmailRecord tempRecord = emailList[key];

                        if (tempRecord.status != Advanced.GetTreeTypeStatus(treeChosen))
                        {
                            continue;
                        }

                        sw.WriteLine("{0},{1},{2},{3}", 
                            person[0], 
                            tempRecord.emailAddress, 
                            tempRecord.status,
                            rowIndexNo); 
                    }           
                }

                if (this.breakOutExited)
                {
                    // break out
                    break;
                }

                if (endIndexRow > 0)
                {
                    if (rowIndexNo - 1 >= endIndexRow)
                    {
                        rowIndexNo++;

                        // break out as it is the end.
                        break;
                    }
                }
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="advanced"></param>
        /// <returns></returns>
        public static Dictionary<string, EmailRecord> loadHtmlGetElementsBySelector(string html, Advanced advanced)
        {
            var source = new HtmlAgilityPack.HtmlDocument();

            source.LoadHtml(html);

            return getNameValueByElementType(source, advanced);
        }

        public static Dictionary<string, EmailRecord> loadHtmlGetElementsBySelector(string html, SalesForce salesForce)
        {
            var source = new HtmlAgilityPack.HtmlDocument();

            source.LoadHtml(html);

            return getNameValueByElementType(source, salesForce);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="advanced"></param>
        /// <returns></returns>
        public static void loadHtmlGetElementsBySelectorSalesForceGetEditLink(string html, SalesForce salesForce)
        {
            var source = new HtmlAgilityPack.HtmlDocument();

            source.LoadHtml(html);

            getNameValueByElementTypeVoid(source, salesForce);
        }

        private static Dictionary<string, EmailRecord> getNameValueByElementType(
        HtmlAgilityPack.HtmlDocument source,
        SalesForce salesForce
    )
        {

            Dictionary<string, EmailRecord> output
                = new Dictionary<string, EmailRecord>();

            var document = source.DocumentNode;

            foreach (KeyValuePair<string,bool> emailItem 
                in salesForce.emailHeaderIdentities)
            {
                if (emailItem.Value)
                {
                    HtmlAgilityPack.HtmlNode editNode = source.GetElementbyId(emailItem.Key);

                    if (editNode.Attributes.ToList().Count(x=>x.Name == "value") >= 1)
                    {
                        HtmlAgilityPack.HtmlAttribute attribute = editNode.Attributes["value"];

                        EmailRecord record = new EmailRecord();
                        
                        record.emailAddress = attribute.Value;

                        output.Add(emailItem.Key, record);
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="advanced"></param>
        /// <returns></returns>
        private static Dictionary<string, EmailRecord> getNameValueByElementType(
            HtmlAgilityPack.HtmlDocument source,
            Advanced advanced
            )
        {

            Dictionary<string, EmailRecord> output 
                = new Dictionary<string, EmailRecord>();

            var document = source.DocumentNode;

            IEnumerable<HtmlAgilityPack.HtmlNode> emailNodes 
                = document.QuerySelectorAll(advanced.elementOne);
            IEnumerable<HtmlAgilityPack.HtmlNode> spanNodes 
                = document.QuerySelectorAll(advanced.elementTwo);

            foreach (HtmlAgilityPack.HtmlNode node in emailNodes)
            {
                // if node is found or not
                bool IsRequiredNode = false;

                // check if node is in header settings
                advanced.emailHeaderIdentities.TryGetValue(node.Id, out IsRequiredNode);
                                       
                if (IsRequiredNode)
                {
                    if (node.Attributes["fieldname"].Value == advanced.fieldNameOne)
                    {
                        if (node.InnerText.Contains("@")) {
                            EmailRecord record = new EmailRecord() { 
                                emailAddress = node.InnerText,
                                nodeIdentity = node.Id,
                                status = "UNDEFINED"                                
                            };
                            output.Add(node.InnerText, record);                            
                        }
                    }
                }     
            }

            Dictionary<string, EmailRecord> newOutput = new Dictionary<string, EmailRecord>();
            foreach (HtmlAgilityPack.HtmlNode node in spanNodes)
            {
                foreach (var key in output.Keys)
                {
                    EmailRecord tempRecord = output[key];

                    if (node.Id == tempRecord.nodeIdentity + "_status")
                    {
                        tempRecord.status = node.InnerText;
                        newOutput[key] = tempRecord;
                    }
                }
            }

            return newOutput;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="advanced"></param>
        /// <returns></returns>
        private static void getNameValueByElementTypeVoid(
            HtmlAgilityPack.HtmlDocument source,
            SalesForce salesForce
            )
        {       
            var document = source.DocumentNode;

            HtmlAgilityPack.HtmlNode editNode = document.QuerySelector(salesForce.elementOne);

            HtmlAgilityPack.HtmlAttribute attribute = editNode.Attributes["href"];

            salesForce.endpointEditSearchFieldTemplate = attribute.Value;       
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void CleanUp(string path)
        {
            File.Delete(path);                 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender,EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            Stream myStream = null;

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.excelFileLocation = openFileDialog1.FileName;

                    if (File.Exists(this.excelFileLocation)) {
                        lblFileStatus.Text = "File Found!";
                        lblExcelFileStatus.BackColor = Color.DarkGreen;
                    }
                    else
                    {
                        lblFileStatus.Text = "File Not Found!";
                        lblExcelFileStatus.BackColor = Color.DarkRed;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirmWorksheet_Click(object sender, EventArgs e)
        {
            lblConfirmedWorksheet.BackColor = Color.DarkGreen;
        }

        private void radioEmailInactive_CheckedChanged(object sender, EventArgs e)
        {
            lblAdvancedOptions.BackColor = Color.DarkGreen;
        }

        private void radioEmailActive_CheckedChanged(object sender, EventArgs e)
        {
            lblAdvancedOptions.BackColor = Color.DarkGreen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKill_Click(object sender, EventArgs e)
        {
            this.breakOutExited = true;
            Application.ExitThread();
            Application.Exit();
        }

        public static string GetEmail(List<string> emailList)
        {
            string emailToReturn = "";
            Dictionary<string, SalesForce.EMAIL_WEIGHT> emailWeights
                = new Dictionary<string, SalesForce.EMAIL_WEIGHT>();

            foreach (string email in emailList)
            {


                string hostDomain = email.Split('@')[1];
                string domain = hostDomain.Split('.')[0];

                if (emailWeights.ContainsKey(domain))
                {
                    continue;
                }
                else
                {
                    emailWeights.Add(domain, SalesForce.GetWeight(domain.ToUpper()));
                }
            }

            var myList = emailWeights.ToList();

            myList.Sort((pair2, pair1) => pair1.Value.CompareTo(pair2.Value));

            emailToReturn = myList[0].Key;

            return emailToReturn;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // track enter and exit
            this.breakOutExited = false;

            if (lblExcelFileStatus.BackColor != Color.DarkGreen ||
               lblConfirmedWorksheet.BackColor != Color.DarkGreen)
            {
                MessageBox.Show("Donna, you forgot a step make sure everything is green!", "Hey Donna... :-)");
                return;
            }

            KeyInput input = new KeyInput();
            ApplicationSettings settings
                = new ApplicationSettings();

            // set csv output
            string csvOutput = settings.FILE_EXPORT_PATH;

            // remove files
            CleanUp(settings.WEB_EXPORT_PATH);

            try
            {
                // on new run, remove output file
                if (File.Exists(csvOutput))
                {
                    File.Copy(csvOutput, csvOutput.Replace(".csv", "_" + DateTime.Now.ToFileTime() + ".csv"));
                    File.Delete(csvOutput);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Donna, did you close the Output.csv file? Close it, then press OK.", "Hey Donna... :-)");
                File.Copy(csvOutput, csvOutput.Replace(".csv", "_" + DateTime.Now.ToFileTime() + ".csv"));
                File.Delete(csvOutput);
            }


            // check what was chosen
            //Advanced.TREE_TYPE treeChosen = (this.radioEmailInactive.Checked)
                //? Advanced.TREE_TYPE.EMAIL_INACTIVE : Advanced.TREE_TYPE.EMAIL_ACTIVE;

            //// based on the chosen value, load the new configuration
            //Advanced advanced =
            //        new Advanced(treeChosen);

            SalesForce salesForce = new SalesForce(SalesForce.TREE_TYPE.PERSON_SEARCH);

            // exit if the file goes away
            if (!File.Exists(this.excelFileLocation))
            {
                return;
            }

            List<string[]> idNumbers = uwExcel.createFromExcel(this.excelFileLocation,
            "A",
            "E",
            txtWorksheetName.Text);

            // excel row number from original
            int rowIndexNo = 1;

            int startFromIndex = 0;
            int endIndexRow = 0;

            // startFrom
            if (settings.START_AT_ROW > 0)
            {
                startFromIndex = settings.START_AT_ROW;
            }

            // startFrom
            if (settings.END_AT_ROW > 0)
            {
                endIndexRow = settings.END_AT_ROW;
            }

            foreach (string[] person in idNumbers)
            {

                if (startFromIndex > 0)
                {
                    if (rowIndexNo - 1 <= startFromIndex)
                    {
                        rowIndexNo++;
                        continue;
                    }
                }

                // skip if missing id number
                if (String.IsNullOrEmpty(person[0]))
                {
                    rowIndexNo++;
                    continue;
                }

                Process chrome = Process.Start(settings.DEFAULT_BROWSER_EXE,
                   salesForce.GetNewEndpoint(person[0]));

                Thread.Sleep(settings.DEFAULT_SLEEP_INTERVAL);

                foreach (var browserInstruction in input.InstructionsBrowser)
                {
                    SendKeys.SendWait(browserInstruction);

                    //if (browserInstruction != "^{w}")
                    //{
                        Thread.Sleep(settings.DEFAULT_SLEEP_INTERVAL);
                    //}
                }

                var html = Clipboard.GetText();

             

                loadHtmlGetElementsBySelectorSalesForceGetEditLink(html, salesForce);

                // if no edit link returned skip
                if (salesForce.endpointEditSearchFieldTemplate == "")
                {
                    rowIndexNo++;
                    continue;
                }

                Process chromeEdit = Process.Start(settings.DEFAULT_BROWSER_EXE,
                salesForce.GetEditEndpoint());

                Thread.Sleep(settings.DEFAULT_SLEEP_INTERVAL);

                foreach (var browserInstruction in input.InstructionsBrowser)
                {
                    SendKeys.SendWait(browserInstruction);

                    //if (browserInstruction != "^{w}")
                    //{
                        Thread.Sleep(settings.DEFAULT_SLEEP_INTERVAL);
                    //}
                }

                var htmlEdit = Clipboard.GetText();

               
                Dictionary<string, EmailRecord> emailDictionary = loadHtmlGetElementsBySelector(htmlEdit, salesForce);

                string emailToWrite = "";

                // if no emails returned skip
                if (emailDictionary.Count > 0)
                {
                    List<string> emailList = new List<string>();

                    foreach (KeyValuePair<string, EmailRecord> emailRecord in emailDictionary)
                    {
                        EmailRecord record = emailRecord.Value;

                        emailList.Add(record.emailAddress);
                    }

                    // weigh out emails
                    string domain = GetEmail(emailList);

                    // find the email to write
                    emailToWrite = emailList.Find(p => p.Contains(domain));
                }

                // check index
                rowIndexNo++;

                // This text is always added, making the file longer over time
                // if it is not deleted.
                using (StreamWriter sw = File.AppendText(csvOutput))
                {
                    sw.WriteLine("{0},{1},{2}",
                        person[0],
                        emailToWrite,
                        rowIndexNo);
                }

                if (this.breakOutExited)
                {
                    // break out
                    break;
                }

                if (endIndexRow > 0)
                {
                    if (rowIndexNo - 1 >= endIndexRow)
                    {
                        rowIndexNo++;

                        // break out as it is the end.
                        break;
                    }
                }
            }            
        }
    }
}
