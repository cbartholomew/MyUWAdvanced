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
                    File.Copy(csvOutput, csvOutput.Replace(".csv","_" + DateTime.Now.ToShortTimeString() + ".csv"));
                    File.Delete(csvOutput);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Donna, did you close the Output.csv file? Close it, then press OK.","Hey Donna... :-)");
                File.Copy(csvOutput, csvOutput.Replace(".csv", "_" + DateTime.Now.ToShortTimeString() + ".csv"));
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

            // startFrom
            if (settings.START_AT_ROW > 0)
            {
                startFromIndex = settings.START_AT_ROW;
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
                    Thread.Sleep(settings.DEFAULT_SLEEP_INTERVAL);
                }

                // remove files
                CleanUp(settings.WEB_EXPORT_PATH);

                Process notepad = Process.Start(settings.DEFAULT_TEXT_EDITOR);
                Thread.Sleep(settings.DEFAULT_SLEEP_INTERVAL);
                foreach (var notepadInstruction in input.InstructionsNotepad)
                {
                    SendKeys.SendWait(notepadInstruction);
                    Thread.Sleep(settings.DEFAULT_SLEEP_INTERVAL);
                }
                              
                var html = File.ReadAllText(settings.WEB_EXPORT_PATH);

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
    }
}
