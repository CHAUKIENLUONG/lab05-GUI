using lab05_BUS;
using lab05_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab05_GUI
{
    public partial class Form1 : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        public Form1()
        {
            InitializeComponent();
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SetGridViewStyle(dgvStudent);
                var listFacultys = facultyService.GetALL();
                var listStudents = studentService.GetALL();
                FillFalcultyCombobox(listFacultys);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            listFacultys.Insert(0, new Faculty());
            this.cmbKhoa.DataSource = listFacultys;
            this.cmbKhoa.DisplayMember = "Facultyname";
            this.cmbKhoa.ValueMember = "FacultyID";
        }

        private void BindGrid(List<Student> ListStudents)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in ListStudents) 
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName
                    ;
                if (item.Faculty != null)
                    dgvStudent.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore + "";
                ShowAvatar(item.Avatar);
            }
        }

        public void SetGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ShowAvatar(string ImageName)
        {
            if (string.IsNullOrEmpty(ImageName))
            {
                picAvatar.Image = null;
            }
            else
            {
                string parentDirectory =
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string imagePath = Path.Combine(parentDirectory, "Images", ImageName);
                picAvatar.Image = Image.FromFile(imagePath);
                picAvatar.Refresh();
            }
        }
         
        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionForeColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnAddVsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMSSV.Text == "" || txtHoTen.Text == "" || cmbKhoa.Text == "" || txtDTB.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ các thông tin!");
                StudentService studentService = new StudentService();
                studentService.InsertUpdate(GetStudent());
                Form1_Load(sender, e);
                SetDefaultForm();
                MessageBox.Show("Insert update thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvStudent.CurrentCell.RowIndex;
            StudentService studentDAO = new StudentService();
            Student f = studentDAO.FindById(dgvStudent.Rows[r].Cells[0].Value.ToString());
            if (f != null)
            {
                txtMSSV.Text = f.StudentID;
                txtHoTen.Text = f.FullName;
                if (f.FacultyID != null)
                    cmbKhoa.SelectedValue = f.FacultyID;
                txtDTB.Text = f.AverageScore.ToString();
                ShowAvatar(f.Avatar);
            }
        }

        private void SetDefaultForm()
        {
            txtMSSV.ResetText();
            txtHoTen.ResetText();
            cmbKhoa.ResetText();
            txtDTB.ResetText();
        }

        Student GetStudent()
        {
            Student s = new Student();
            s.StudentID = txtMSSV.Text.Trim();
            s.FullName = txtHoTen.Text.Trim();
            if (cmbKhoa.Text != "")
                s.FacultyID = int.Parse(cmbKhoa.SelectedValue.ToString());
            if (txtDTB.Text != "")
                s.AverageScore = double.Parse(txtDTB.Text);
            return s;
        }
    }
}
