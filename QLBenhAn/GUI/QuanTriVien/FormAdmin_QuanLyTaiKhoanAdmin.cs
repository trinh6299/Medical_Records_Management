using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI.QuanTriVien
{
    public partial class FormAdmin_QuanLyTaiKhoanAdmin : Form
    {
        public FormAdmin_QuanLyTaiKhoanAdmin()
        {
            InitializeComponent();
        }
        private void ClearAll()
        {
            txt_TaiKhoan.Text = string.Empty;
            txt_MatKhau.Text = string.Empty;
        }
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        BUS_tbl_Admin bus = new BUS_tbl_Admin();
        private void FormQuanLyTaiKhoanAdmin_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            dgv_Admin.DataSource = bus.SelectAll();
            ClearAll();
        }
        int row;
        private void dgv_Admin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;

            if (row >= 0)
            {
                txt_TaiKhoan.Text = dgv_Admin.Rows[row].Cells[0].Value.ToString();
                txt_MatKhau.Text = dgv_Admin.Rows[row].Cells[1].Value.ToString();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if(bus.SelectAll().Rows.Count == 1)
            {
                MessageBox.Show("Đây là tải khoản cuối cùng, nếu xóa bạn sẽ mất quyền truy cập admin, không thể xóa tài khoản admin cuối cùng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DTO_tbl_Admin admin = new DTO_tbl_Admin();
            admin.TaiKhoan = dgv_Admin.Rows[row].Cells[0].Value.ToString();
            bus.DeleteAdmin(admin);

            FormQuanLyTaiKhoanAdmin_Load(sender, e);
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_TaiKhoan.Text.Trim() == string.Empty)
                {
                    throw new Exception("Tài khoản không được để trống!");
                }
                if(txt_MatKhau.Text.Trim() == string.Empty)
                {
                    throw new Exception("Mật khẩu không được để trống!");
                }
                DTO_tbl_Admin admin = new DTO_tbl_Admin();
                admin.TaiKhoan = txt_TaiKhoan.Text;
                admin.MatKhau = txt_MatKhau.Text;
                bus.InsertAdmin(admin);

                FormQuanLyTaiKhoanAdmin_Load(sender, e);
            }catch(SqlException sql)
            {
                if(sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Tài khoản đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(sql.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {

                if (txt_TaiKhoan.Text.Trim() == string.Empty)
                {
                    throw new Exception("Tài khoản không được để trống!");
                }
                if (txt_MatKhau.Text.Trim() == string.Empty)
                {
                    throw new Exception("Mật khẩu không được để trống!");
                }
                DTO_tbl_Admin admin = new DTO_tbl_Admin();
                admin.TaiKhoan = dgv_Admin.Rows[row].Cells[0].Value.ToString();
                admin.MatKhau = txt_MatKhau.Text;
                bus.UpdateAdmin(admin);

                FormQuanLyTaiKhoanAdmin_Load(sender, e);
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
    }
}
