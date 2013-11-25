using System;
using System.Linq;
using System.Windows.Forms;
using TestHotKey.STORAGE;

namespace TestHotKey
{
    public partial class AddPic : Form
    {
        public AddPic()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox3.Text.Equals(string.Empty) && !richTextBox1.Text.Equals(string.Empty))
            {
                var newTags = richTextBox1.Text.Split(' ').ToList();
                PicStorage.AddNewPic(textBox3.Text, newTags);
                PicStorage.Save();
                Close();
            }
            else
            {
                MessageBox.Show(@"Please fill up all fields!", @"Add new URL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
