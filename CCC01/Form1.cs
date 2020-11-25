using CC01.BLL;
using CC01.BO;
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

namespace CCC01
{
    public partial class FrmEtudiant : Form
    {
        private Action callback;
        private Etudiant oldEtudiant;
        private EtudiantBLO etudiantBLO;
        Ecole ecole = new Ecole();
        public FrmEtudiant()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void loadData()
        {
            string value = txtRecherche.Text;
            var students = etudiantBLO.GetBy(
                x =>
                x.FirstName.ToLower().Contains(value) ||
                x.LastName.ToLower().Contains(value) ||
                x.BornAt.ToLower().Contains(value)
                ).OrderBy(x => x.FirstName).ToArray();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;
            dataGridView1.ClearSelection();
        }
        public FrmEtudiant(Action callback) : this()
        {
            this.callback = callback;
        }
        public FrmEtudiant(Etudiant etudiant, Action callback) : this(callback)
        {
            this.oldEtudiant = etudiant;
            txtFirstName.Text = etudiant.FirstName;
            txtLastName.Text = etudiant.LastName;
            dateTimePicker1.Text = etudiant.BornOn.ToString();
            txtAt.Text = etudiant.BornAt;
            txtTel.Text = etudiant.TelS.ToString();
            pictureBox1.Image = etudiant.Photo != null ? Image.FromStream(new MemoryStream(etudiant.Photo)) : null;
            txtEmail.Text = etudiant.EmailS;

            if (etudiant.Sexe.ToString() == "Male")
                checkBox1.Checked = true;
            else if (etudiant.Sexe.ToString() == "Female")
                checkBox2.Checked = true;
            else
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }

            //radioButton1.Checked = (student.Sexe.ToString() == "Male") ? true : false;
            //radioButton2.Checked = (student.Sexe.ToString() == "Female") ? true : false;
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {

                    Form f = new FrmEtudiant
                        (
                            dataGridView1.SelectedRows[i].DataBoundItem as Etudiant,
                            loadData
                        );
                    this.Close();
                    f.Show();
                    f.WindowState = FormWindowState.Maximized;
                }

            }
        }
        private void checkForm()
        {
            string text = string.Empty;
            txtFirstName.BackColor = Color.White;
            txtLastName.BackColor = Color.White;

            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                text += "- FirstName or LastName can't be empty !\n";
                txtFirstName.BackColor = Color.LightPink;
                txtLastName.BackColor = Color.LightPink;

            }

            if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                text += "- le sex est vide  !\n";

            }

            if (string.IsNullOrWhiteSpace(txtTel.Text))
            {
                text += "- Telephone est vide !\n";
                txtTel.BackColor = Color.LightPink;

            }
            if (string.IsNullOrWhiteSpace(txtAt.Text))
            {
                text += "- Place of birth can't be empty !\n";
                txtAt.BackColor = Color.LightPink;

            }
            if (!string.IsNullOrEmpty(text))
                throw new TypingException(text);
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            try
            {
                checkForm();
                string sex;

                if (checkBox1.Checked)
                    sex = checkBox1.Text;
                else if (checkBox2.Checked)
                    sex = checkBox2.Text;
                else
                    sex = "";



                Etudiant newEtudiant = new Etudiant(
                    txtFirstName.Text,
                    txtLastName.Text,
                    txtEmail.Text,
                    int.Parse(txtTel.Text),
                    sex,
                    Convert.ToDateTime(bunifuDatepicker1.Value),
                    txtAt.Text,
                    !string.IsNullOrEmpty(pictureBox1.ImageLocation) ? File.ReadAllBytes(pictureBox1.ImageLocation) : this.oldEtudiant?.Photo,
                    ecole
                    );


                EtudiantBLO etudiantBLO = new EtudiantBLO(ConfigurationManager.AppSettings["DbFolder"]);

                if (this.oldEtudiant == null)
                    etudiantBLO.CreateEtudiant(newEtudiant);
                else
                    etudiantBLO.EditEtudiant(oldEtudiant, newEtudiant);


                MessageBox.Show(
                    "Save Done !",
                    "Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );


                if (callback != null)
                    callback();
                txtFirstName.Clear();
                txtLastName.Clear();
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                txt.Clear();
                bunifuDatepicker1.Value = DateTime.Now;
                pictureBox1.ImageLocation = null;
                txtTel.Clear();
                txtEmail.Clear();
                txtFirstName.Focus();
                loadData();

            }
            catch (TypingException ex)
            {
                MessageBox.Show(
                ex.Message,
                "Typing error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );

            }
            catch (DuplicateNameException ex)
            {
                MessageBox.Show(
                ex.Message,
                "Duplicate error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );

            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(
                ex.Message,
                "key not found error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );

            }
            catch (Exception ex)
            {
                ex.WriteToFile();

                MessageBox.Show(
                "An error occured please try again",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );

            }
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Confirmation ? ",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        etudiantBLO.DeleteEtudiant(dataGridView1.SelectedRows[i].DataBoundItem as Etudiant);
                    }
                    loadData();


                }

            }
        }
    }
}
