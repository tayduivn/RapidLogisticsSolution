﻿using BusinessEntities;
using BusinessServices.Interfaces;
using System;
using System.Windows.Forms;

namespace RapidWarehouse
{
    public partial class FormCreateEditEmployee : Form
    {
        IEmployeeServices mEmployeeService;
        EmployeeEntity employeeCreateOrUpdate;
        public FormCreateEditEmployee(IEmployeeServices employeeServices)
        {
            InitializeComponent();
            mEmployeeService = employeeServices;
            dtpBirthDate.CustomFormat = "dd/MM/yyyy";
            dtpHiredDate.CustomFormat = "dd/MM/yyyy";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateDataInput())
            {
                if (employeeCreateOrUpdate == null)
                {
                    employeeCreateOrUpdate = new EmployeeEntity();
                    employeeCreateOrUpdate.Address = txtAddress.Text;
                    employeeCreateOrUpdate.BirthDate = dtpBirthDate.Value;
                    employeeCreateOrUpdate.DateCreated = DateTime.Now;
                    employeeCreateOrUpdate.Email = txtEmail.Text;
                    employeeCreateOrUpdate.FullName = txtFullName.Text;
                    employeeCreateOrUpdate.Id = 0;
                    employeeCreateOrUpdate.Pasword = txtPassword.Text;
                    employeeCreateOrUpdate.Phone = txtPhone.Text;
                    employeeCreateOrUpdate.Role = cbbRole.Text;
                    employeeCreateOrUpdate.Status = 1;
                    employeeCreateOrUpdate.UserName = txtUserName.Text;
                    mEmployeeService.CreateOrUpdateEmployee(employeeCreateOrUpdate);
                    MessageBox.Show("Tạo " + cbbRole.Text + " thành công !");
                }
                else
                {
                    mEmployeeService.CreateOrUpdateEmployee(employeeCreateOrUpdate);
                    MessageBox.Show("Cập nhật " + cbbRole.Text + " thành công !");
                }
                
                employeeCreateOrUpdate = null;
            }
        }

        private bool ValidateDataInput()
        {
            if(string.IsNullOrEmpty(txtUserName.Text) || txtUserName.Text.Length < 4)
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

            if (string.IsNullOrEmpty(cbbRole.Text))
            {
                MessageBox.Show("Vui lòng chọn quyền cho user!");
                cbbRole.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtFullName.Text) || txtFullName.Text.Length < 4)
            {
                MessageBox.Show("Vui lòng nhập Họ và Tên từ 4 ký tự trở lên!");
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text) || txtPhone.Text.Length < 10)
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại từ 10 ký tự trở lên!");
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập Email!");
                txtEmail.Focus();
                return false;
            }

            if (mEmployeeService.IsExist(txtUserName.Text))
            {
                MessageBox.Show("User Name này đã tồn tại, vui lòng chọn user name khác!");
                txtUserName.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormHome home = new FormHome();
            home.Show();
            this.Dispose();
        }

        private void FormCreateEditEmployee_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormHome home = new FormHome();
            home.Show();
            this.Dispose();
        }
    }
}
