using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Alphabeth
{
    static class Check
    {

        //create list of sorted arrays per each file: list of lists. 
        public static List<List<string>> FindNotSorted(List<string> pathsStrings)
        {
            List<List<string>> miltiPathOutput = new List<List<string>>();

            //exceptions in order to skip not relevant parts for sorting
            string[] exceptions =
            {
                "port", "generic", "component",
                "rosetta", "ieee", "library"
            };
            //gets paths of multiple files
            foreach (string path in pathsStrings)
            {
                List<string> singePathOutput=new List<string>();
                singePathOutput.Add(path);
                using (StreamReader reader = new StreamReader(path))
                {
                    string lineA = reader.ReadLine();
                    string lineB = null;
                    string pattern = @"\s|\;|\(|\)";//user use both tabs and spaces, this makes challange to compare
                    for (int i = 2; !reader.EndOfStream; i++)
                    {
                        lineB = reader.ReadLine();
                        lineB = Regex.Replace(lineB, pattern, "");
                        if (//compare line A and B, check if there no eceptions
                            String.Compare(lineA, lineB) == 1
                            && !String.IsNullOrEmpty(lineA)
                            && !String.IsNullOrEmpty(lineB))
                        {
                            bool noexception = true;
                            foreach (string ex in exceptions)
                            {
                                if (lineB.Contains(ex) || lineA.Contains(ex))
                                {
                                    noexception = false;
                                    break;
                                }
                            }
                            if (noexception)//if row doesn't contain word from exception list
                            {
                                singePathOutput.Add(i.ToString());
                                
                            }
                        }
                        lineA = lineB;
                    }
                }
                miltiPathOutput.Add(singePathOutput);
            }
            
            return miltiPathOutput;
        }


        public static RichTextBox DisplayResult(List<List<string>> list)
        {
            List<List<string>> miltiPathOutput = list;
            //create text box to display results, properties
            RichTextBox dynamicRichTextBox = new RichTextBox();
            int pointA = 30;
            int pointB = 80;
            dynamicRichTextBox.Location = new Point(pointA, pointB);
            dynamicRichTextBox.AutoSize = true;
            dynamicRichTextBox.Height = (int)(System.Windows.Forms.Form.ActiveForm.Height*0.8);
            dynamicRichTextBox.Width= (int)(System.Windows.Forms.Form.ActiveForm.Width * 0.7);
            dynamicRichTextBox.Name = "DynamicRichTextBox";
            dynamicRichTextBox.Font = new Font("Verdana", 16);

            //originally code was buit to present outputs in different dynamic text boxes.
            List<string> consolidatedOutput = new List<string>();
            foreach (List<string> singleOutput in miltiPathOutput)
            {
                foreach (var s in singleOutput)
                    consolidatedOutput.Add(s);
            }
            dynamicRichTextBox.Text= String.Join(Environment.NewLine, consolidatedOutput);

           
            return dynamicRichTextBox;
        }
    }
}
