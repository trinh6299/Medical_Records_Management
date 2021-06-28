using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using System.Data.SqlClient;
namespace GUI.QuanTriVien
{
    public partial class FormAdmin_QuanLyKhoa : Form
    {
        public FormAdmin_QuanLyKhoa()
        {
            InitializeComponent();
        }
        BUS_tbl_Khoa bus = new BUS_tbl_Khoa();

        private void ClearAll()
        {
            txt_MaKhoa.Text = string.Empty;
            txt_TenKhoa.Text = string.Empty;
        }
        private void FormQuanLyKhoa_Load(object sender, EventArgs e)
        {
            dgv_Khoa.DataSource = bus.SelectAll();
            FormBorderStyle = FormBorderStyle.None;
            ClearAll();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int row;
        private void dgv_Khoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;

            if(row >= 0)
            {
                txt_MaKhoa.Text = dgv_Khoa.Rows[row].Cells[0].Value.ToString();
                txt_TenKhoa.Text = dgv_Khoa.Rows[row].Cells[1].Value.ToString();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                bus.DeleteKhoa(dgv_Khoa.Rows[row].Cells[0].Value.ToString());
                FormQuanLyKhoa_Load(sender, e);
            }
            catch(SqlException sql)
            {
                if(sql.Message.Contains("DELETE statement conflicted with the REFERENCE"))
                {
                    MessageBox.Show("Hiện đang tồn tại bác sĩ trong khoa này, chỉ có thể xóa khoa không có bác sĩ!","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaKhoa.Text.Trim() == string.Empty)
                {
                    throw new Exception("Mã khoa không được để trống!");
                }
                if (txt_TenKhoa.Text.Trim() == string.Empty)
                {
                    throw new Exception("Tên khoa không được để trống!");
                }
                DTO_tbl_Khoa khoa = new DTO_tbl_Khoa();
                khoa.MaKhoa = txt_MaKhoa.Text;
                khoa.TenKhoa = txt_TenKhoa.Text;
                bus.InsertKhoa(khoa);

                FormQuanLyKhoa_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Tài khoản đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(sql.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaKhoa.Text.Trim() == string.Empty)
                {
                    throw new Exception("Mã khoa không được để trống!");
                }
                if (txt_TenKhoa.Text.Trim() == string.Empty)
                {
                    throw new Exception("Tên khoa không được để trống!");
                }
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Tài khoản đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(sql.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DTO_tbl_Khoa khoa = new DTO_tbl_Khoa();
            khoa.MaKhoa = txt_MaKhoa.Text;/*dgv_Khoa.Rows[row].Cells[0].Value.ToString();*/
            khoa.TenKhoa = txt_TenKhoa.Text;
            bus.UpdateKhoa(khoa);

            FormQuanLyKhoa_Load(sender, e);
        }


    }
}
