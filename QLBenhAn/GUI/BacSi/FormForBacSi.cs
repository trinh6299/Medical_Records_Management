using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.BacSi
{
    public partial class FormForBacSi : Form
    {
        public FormForBacSi()
        {
            InitializeComponent();
        }
        private void CloseOtherForm()//Hàm đóng tất cả các form hiện tại trên panel container rồi mới mở form mới
        {
            while (panelBacSi.Controls.Count > 0)
            {
                panelBacSi.Controls[0].Dispose();
            }
        }
        private void FormForBacSi_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CloseOtherForm();
            FormBacSi_QuanLyBenhAn frm = new FormBacSi_QuanLyBenhAn() { TopLevel = false };
            panelBacSi.Controls.Add(frm);
            frm.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            CloseOtherForm();
            FormBacSi_QuanLyBenhNhan frm1 = new FormBacSi_QuanLyBenhNhan() { TopLevel = false };
            panelBacSi.Controls.Add(frm1);
            frm1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }  
}
