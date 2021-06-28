﻿using System;
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
using GUI.BenhNhan;
using System.Data.SqlClient;

namespace GUI.QuanTriVien
{
    public partial class FormAdmin_QuanLyBenhNhan : Form
    {
        public FormAdmin_QuanLyBenhNhan()
        {
            InitializeComponent();
        }
        //Hàm clear hết dữ liệu textbox, combobox,.. trên form
        private void ClearAll()
        {
            txt_CCCD.Text = string.Empty;
            txt_HoTen.Text = string.Empty;
            txt_SDT.Text = string.Empty;
            dtp_NgaySinh.Text = string.Empty;
            txt_MaBHYT.Text = string.Empty;
            txt_MatKhau.Text = string.Empty;
            txt_DiaChi.Text = string.Empty;
            rdb_Nam.Checked = true;
        }
        BUS_tbl_BenhNhan bus = new BUS_tbl_BenhNhan();
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormQuanLyBenhNhan_Load(object sender, EventArgs e)
        {
            dgv_BenhNhan.DataSource = bus.SelectAllFromBenhNhan();

            cb_TimKiemTheo.SelectedItem = "Họ tên";
            
            FormBorderStyle = FormBorderStyle.None;
            ClearAll();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            if(cb_TimKiemTheo.Text == "Họ tên")
            {
                dgv_BenhNhan.DataSource = bus.FindBenhNhanByName(txt_TimKiem.Text);
            }
            else
            {
                dgv_BenhNhan.DataSource = bus.FindBenhNhanByCCCD(txt_TimKiem.Text);
            }
            ClearAll();
            
        }
        int row;
        private void dgv_BenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
            if (row >= 0)
            {
                txt_CCCD.Text = dgv_BenhNhan.Rows[row].Cells[0].Value.ToString();
                txt_MatKhau.Text = dgv_BenhNhan.Rows[row].Cells[1].Value.ToString();
                txt_HoTen.Text = dgv_BenhNhan.Rows[row].Cells[2].Value.ToString();
                if (dgv_BenhNhan.Rows[row].Cells[3].Value.ToString() == "Nam")
                {
                    rdb_Nam.Checked = true;
                }
                else
                {
                    rdb_Nu.Checked = true;
                }
                dtp_NgaySinh.Text = dgv_BenhNhan.Rows[row].Cells[4].Value.ToString();
                txt_MaBHYT.Text = dgv_BenhNhan.Rows[row].Cells[5].Value.ToString();
                txt_DiaChi.Text = dgv_BenhNhan.Rows[row].Cells[6].Value.ToString();
                txt_SDT.Text = dgv_BenhNhan.Rows[row].Cells[7].Value.ToString();
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_CCCD.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập căn cước công dân");
                }
                if (txt_HoTen.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập họ tên bệnh nhân");
                }
                if (txt_DiaChi.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa chỉ bệnh nhân");
                }
                if (txt_MaBHYT.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa mã bảo hiểm y tế");
                }
                if (txt_MatKhau.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập mật khẩu");
                }
                if (txt_SDT.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập số điện thoại");
                }
                DTO_tbl_BenhNhan bn = new DTO_tbl_BenhNhan();
                bn.CCCD = txt_CCCD.Text;
                bn.MatKhau = txt_MatKhau.Text;
                bn.HoTen = txt_HoTen.Text;
                if (rdb_Nam.Checked == true)
                {
                    bn.GioiTinh = "Nam";
                }
                else
                {
                    bn.GioiTinh = "Nữ";
                }
                bn.NgaySinh = dtp_NgaySinh.Value;
                bn.MaBHYT = txt_MaBHYT.Text;
                bn.DiaChi = txt_DiaChi.Text;
                bn.SDT = txt_SDT.Text;
                bus.InsertBenhNhan(bn);

                FormQuanLyBenhNhan_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Bệnh nhân đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                bus.DeleteBenhNhan(dgv_BenhNhan.Rows[row].Cells[0].Value.ToString());
                FormQuanLyBenhNhan_Load(sender, e);
            }
            catch(SqlException sql)
            {
                if (sql.Message.Contains("DELETE statement conflicted with the REFERENCE"))
                {
                    MessageBox.Show("Hiện có bệnh án của bệnh nhân này trong CSDL, chỉ xóa được bệnh nhân có bệnh án", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(sql.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_CCCD.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập căn cước công dân");
                }
                if (txt_HoTen.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập họ tên bệnh nhân");
                }
                if (txt_DiaChi.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa chỉ bệnh nhân");
                }
                if (txt_MaBHYT.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa mã bảo hiểm y tế");
                }
                if (txt_MatKhau.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập mật khẩu");
                }
                if (txt_SDT.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập số điện thoại");
                }
                DTO_tbl_BenhNhan bn = new DTO_tbl_BenhNhan();
                bn.CCCD = dgv_BenhNhan.Rows[row].Cells[0].Value.ToString();//không lấy trên textbox vì textbox có thể bị chỉnh sửa
                bn.MatKhau = txt_MatKhau.Text;
                bn.HoTen = txt_HoTen.Text;
                if (rdb_Nam.Checked == true)
                {
                    bn.GioiTinh = "Nam";
                }
                else
                {
                    bn.GioiTinh = "Nữ";
                }
                bn.NgaySinh = dtp_NgaySinh.Value;
                bn.MaBHYT = txt_MaBHYT.Text;
                bn.DiaChi = txt_DiaChi.Text;
                bn.SDT = txt_SDT.Text;
                bus.UpdateBenhNhan(bn);

                FormQuanLyBenhNhan_Load(sender, e);
            }
            catch (SqlException sql)
            {
                if (sql.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Bệnh nhân đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btn_XemBenhAn_Click(object sender, EventArgs e)
        {
            FormXemBenhAn frm = new FormXemBenhAn();
            frm.TenBenhNhan = dgv_BenhNhan.Rows[row].Cells[2].Value.ToString();
            frm.CCCD1 = dgv_BenhNhan.Rows[row].Cells[0].Value.ToString();
            frm.Show();
        }
    }
}
