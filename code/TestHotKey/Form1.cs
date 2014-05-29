using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TestHotKey.HOOK;
using TestHotKey.STORAGE;

namespace TestHotKey
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public Form1()
        {
            InitializeComponent();

            const int id = 0;
            //set your global hot key
            //RegisterHotKey(Handle, id, (int)KeyModifier.Shift, Keys.A.GetHashCode()); if you want single key modifier
            RegisterHotKey(Handle, id, (int)KeyModifier.Shift | (int)KeyModifier.Control, Keys.D.GetHashCode());
        }
        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x0312)
            {
                var p = Cursor.Position;
                Location = new Point(p.X+20, p.Y+20);
                WindowState = FormWindowState.Normal;
                Show();
                textBox3.Focus();
                textBox3.SelectAll();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            var data = PicStorage.GetFunPic(textBox3.Text.Split(' ').ToList());
            dataGridView1.Rows.Clear();
            foreach (var f in data)
            {
                dataGridView1.Rows.Add(f.PrintTags(), f.Url);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Clipboard.SetText(dataGridView1[1, e.RowIndex].Value.ToString());
            Hide();
            notifyIcon1.ShowBalloonTip(500);
        }

        private void hIDEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(Handle, 0);
        }

        private void aDDURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addUrlForm = new AddPic();
            addUrlForm.ShowDialog();
        }
    }
}
