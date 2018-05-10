using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alphabeth
{
    static class MyForm
    {
        public static List<string> GetFilesFullPath(OpenFileDialog openFileDialog)
        {
            openFileDialog.FileName = "";
            List<string> filesFullPath = new List<string>();
 
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) 
                foreach (String file in openFileDialog.FileNames)
                {
                    try
                    {
                        filesFullPath.Add(file);
                    }
                    catch (SecurityException ex)
                    {
                        // The user lacks appropriate permissions to read files, discover paths, etc.
                        MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                                        "Error message: " + ex.Message + "\n\n" +
                                        "Details (send to Support):\n\n" + ex.StackTrace
                        );
                    }
                    catch (Exception ex)
                    {
                        // Could not load file.
                        MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                                        + ". You may not have permission to read the file, or " +
                                        "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }
            return filesFullPath;
        }
    }
}
