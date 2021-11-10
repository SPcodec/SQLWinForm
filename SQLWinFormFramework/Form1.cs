using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShowGrid();
        }

        private void ShowGrid()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from UserTab", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private string ConnectionString = "Server=(Local);Initial Catalog=Demo;Integrated Security=True";

        private void InsertButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into UserTab values (@ID,@Name,@Age)", con);
            cmd.Parameters.AddWithValue("@ID", int.Parse(IDBox.Text));
            cmd.Parameters.AddWithValue("@Name", NameBox.Text);
            cmd.Parameters.AddWithValue("@Age", double.Parse(AgeBox.Text));
            cmd.ExecuteNonQuery();
            con.Close();
            ShowGrid();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Update UserTab set Name=@Name, Age=@Age where ID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", int.Parse(IDBox.Text));
            cmd.Parameters.AddWithValue("@Name", NameBox.Text);
            cmd.Parameters.AddWithValue("@Age", double.Parse(AgeBox.Text));
            cmd.ExecuteNonQuery();
            con.Close();
            ShowGrid();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete UserTab where ID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", int.Parse(IDBox.Text));
            cmd.ExecuteNonQuery();
            con.Close();
            ShowGrid();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            if (IDBox.Text == "")
            {
                ShowGrid();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Select * from UserTab where ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", int.Parse(IDBox.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
    }
}
