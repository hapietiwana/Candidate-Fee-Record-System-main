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
using System.Xml.Linq;

namespace UserSystem
{
    public partial class Form1 : Form
    {
        public Form1() 
        {
            InitializeComponent();
            load();
            this.CenterToScreen();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-6M4QRQE\\MSSQLSERVER01; Initial Catalog=hi; User Id=yogi ; Password=yogi1");
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool mode = true;
        string sql;


        public void load() {
            try {
                sql = "select * from candidateDB2";
                cmd= new SqlCommand(sql, con);
                con.Open();

                read= cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read()) {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();

            }catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message); 
            }
            
        }


        public void getID(string ID) {
            sql = "select * from candidateDB2 where id= '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();
            while (read.Read()) {
                txtname.Text = read[1].ToString();
                txtcourse.Text = read[2].ToString();
                txtfee.Text = read[3].ToString();
            }
            con.Close();
        
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtname.Text;
            string course = txtcourse.Text;
            string fee = txtfee.Text;

            if (mode == true)
            {
                sql = "Insert into candidateDB2(cname ,course,fee) values(@cname, @course, @fee)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);

                MessageBox.Show("Record Successfully Added !");

                cmd.ExecuteNonQuery();

                txtname.Clear();
                txtcourse.Clear();
                txtfee.Clear();

                txtname.Focus();
                con.Close();
            }
            else {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update candidateDB2 set cname=@cname,course=@course,fee=@fee where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updated!");

                cmd.ExecuteNonQuery();

                txtname.Clear();
                txtcourse.Clear();
                txtfee.Clear();

                txtname.Focus();
                button1.Text = "Save";
                mode = true;
                con.Close() ;

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0) {
                
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from candidateDB2 where id  = @id ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id ", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted !");
                con.Close();
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtname.Clear();
            txtcourse.Clear();
            txtfee.Clear();
            txtname.Focus();
            button1.Text = "Save";
            mode = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtcourse_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            Form4 frmb= new Form4();
            this.Hide();
            frmb.ShowDialog();
        }

       

        private void txtfee_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
