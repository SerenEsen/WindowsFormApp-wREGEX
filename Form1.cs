using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Project
{
    public partial class Form1 : Form
    {
        private string variableRegexPattern = @"p_(\w*) * IN * VARCHAR2";

        public Form1()
        {
            InitializeComponent();
        }

        private void convert_Click(object sender, EventArgs e)
        {
            
            string sp = rctOldSp.Text;
            Regex rx = new Regex(variableRegexPattern,
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var matchedVariables = rx.Matches(sp)
                                            .Cast<Match>()
                                            .Select(m => m.Value)
                                            .ToArray(); ;

            foreach(string variable in matchedVariables)
            {
                string pattern = @"( *)\'( *)\'( *)\'( *)\|\|( *)" + variable.Replace("IN VARCHAR2", " ").Trim() + @"( *)\|\|( *)\'( *)\'( *)\'( *)\'( *)\;( *)";
                string replacement = @"'||DBMS_ASSERT.ENQUOTE_LITERAL(" + variable.Replace("IN VARCHAR2", " ").Trim() + @");";

                sp=Regex.Replace(sp, pattern, replacement);

                rctNewSp.Text = sp; 
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
