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
        public List<string> InstructionsNotepad  { get; set; }

        public KeyInput() 
        {
            this.InstructionsBrowser = new List<string>();
            this.InstructionsNotepad = new List<string>();
            
            initializeInstructionsForBrowsers();
            initializeInstructionsForNotePad();
        }

        private void initializeInstructionsForBrowsers() 
        {
            this.InstructionsBrowser.Insert(0,VIEW_SOURCE);
            this.InstructionsBrowser.Insert(1,SELECT_ALL);
            this.InstructionsBrowser.Insert(2,COPY_HTML);
            this.InstructionsBrowser.Insert(3,CLOSE_WINDOW);
            this.InstructionsBrowser.Insert(4,CLOSE_WINDOW);                                    
        }

        private void initializeInstructionsForNotePad()
        {
            ApplicationSettings settings 
                = new ApplicationSettings();

            this.InstructionsNotepad.Insert(0,PASTE_HTML);
            this.InstructionsNotepad.Insert(1,SAVE_FILE); 
            this.InstructionsNotepad.Insert(2,settings.WEB_EXPORT_PATH);
            this.InstructionsNotepad.Insert(3,ENTER_KEY);
            this.InstructionsNotepad.Insert(4,CLOSE_APPLICATION);
        }
    }
}
