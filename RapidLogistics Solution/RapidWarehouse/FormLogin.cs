﻿using BusinessEntities;
using BusinessServices.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RapidWarehouse
{
    public partial class FormLogin : Form
    {
        IEmployeeServices mEmployeeService;
        public static EmployeeEntity mEmployee;

        public FormLogin(IEmployeeServices employeeServices)
        {
            InitializeComponent();
            mEmployeeService = employeeServices;
            //CheckConnection();
        }

        private void CheckConnection()
        {
            try
            {
                mEmployeeService.IsExist("Admin");
                lblError.Text = "Đã có kết nối, mời bạn đăng nhập!";
            }
            catch
            {
                ShowDialogQuestionConnectToDb();
            }
        }

        private void ShowDialogQuestionConnectToDb()
        {
            if (MessageBox.Show("Kết nối đến CSDL thất bại !\nBạn có muốn thiết lập bây giờ không ?", "Thiết lập thông tin cơ sở dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Program.Container.GetInstance<FormConfigDB>().Show();
            }

            lblError.Text = "Không có kết nối đến cơ sở dữ liệu!";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                try
                {
                    mEmployee = mEmployeeService.Login(txtUserName.Text, txtPassword.Text);
                    if (mEmployee != null)
                    {
                        lblError.Text = "";
                        Program.Container.GetInstance<FormHome>().Show();
                        this.Hide();
                    }
                    else
                    {
                        lblError.Text = "Bạn đã nhập sai User name hoặc Password, hãy thử lại!";
                    }
                }
                catch (Exception ex)
                {
                    Ultilities.FileHelper.WriteLog(Ultilities.ExceptionLevel.Function, "Đã có lỗi xảy ra khi đăng nhập, vui lòng thử lại sau", ex);
                    lblError.Text = "Đã có lỗi xảy ra khi đăng nhập, vui lòng thử lại sau!";
                    ShowDialogQuestionConnectToDb();
                }
            }
        }

        private bool ValidateLogin()
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || txtUserName.Text.Length < 4)
            {
                MessageBox.Show("Vui lòng nhập User Name từ 4 ký tự trở lên!");
                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text) || txtPassword.Text.Length < 4)
            {
                MessageBox.Show("Vui lòng nhập Password từ 4 ký tự trở lên!");
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblConfigDb_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.Container.GetInstance<FormConfigDB>().Show();
        }
        public List<Control> GetAll(Control control)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl))
                                      .Concat(controls).ToList();
        }
        
    }
}
