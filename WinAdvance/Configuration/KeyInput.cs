using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinAdvance.Settings;

namespace WinAdvance.Configuration
{
    public class KeyInput
    {
        public const string VIEW_SOURCE         = "^{u}";
        public const string SELECT_ALL          = "^{a}";
        public const string COPY_HTML           = "^{c}";
        public const string PASTE_HTML          = "^{v}";
        public const string CLOSE_WINDOW        = "^{w}";
        public const string SAVE_FILE           = "^{s}";
        public const string ENTER_KEY           = "{ENTER}";
        public const string CLOSE_APPLICATION   = "%{F4}";

        public List<string> InstructionsBrowser  { get; set; }        

        public KeyInput() 
        {
            this.InstructionsBrowser = new List<string>();           
            
            initializeInstructionsForBrowsers();            
        }

        private void initializeInstructionsForBrowsers() 
        {
            this.InstructionsBrowser.Insert(0,VIEW_SOURCE);
            this.InstructionsBrowser.Insert(1,SELECT_ALL);
            this.InstructionsBrowser.Insert(2,COPY_HTML);
            this.InstructionsBrowser.Insert(3,CLOSE_WINDOW);
            this.InstructionsBrowser.Insert(4,CLOSE_WINDOW);                                    
        }

    }
}
