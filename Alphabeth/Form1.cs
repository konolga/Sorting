using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Alphabeth;

namespace Alphabeth
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 5;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control item in Controls.OfType<RichTextBox>())
            {
                Controls.Remove(item);
            }

            List<string> filesFullPath = MyForm.GetFilesFullPath(openFileDialog1);
            List<List<string>> result = Check.FindNotSorted(filesFullPath);
            RichTextBox dynamicRichTextBox = Check.DisplayResult(result);
            Controls.Add(dynamicRichTextBox);
           
        }
    }
}

