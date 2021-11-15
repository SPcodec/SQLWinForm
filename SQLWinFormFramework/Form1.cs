using System;
using System.Data;
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

        //Method to show the updated grid after every button press
        private void ShowGrid()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from GameList", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Method to check if the name of the game already exists in the database
        private DataTable CheckExistence(string GameName, SqlConnection SqlConnectionString)
        {
            SqlDataAdapter da = new SqlDataAdapter($"Select Name From Gamelist where Name = '{GameName}'", SqlConnectionString);
            DataTable TestTable = new DataTable();
            da.Fill(TestTable);
            return TestTable;
        }

        private string ConnectionString = "Server=(Local);Initial Catalog=Demo;Integrated Security=True";

        private void InsertButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            try
            {
                if (CheckExistence(NameBox.Text, con).Rows.Count >= 1)
                {
                    MessageBox.Show("This game already exists.");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into GameList values (@Name,@Release_Year,@Completed)", con);
                    cmd.Parameters.AddWithValue("@Name", NameBox.Text);
                    cmd.Parameters.AddWithValue("@Release_Year", int.Parse(ReleaseBox.Text));
                    cmd.Parameters.AddWithValue("@Completed", CompletedComboBox.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) when (
                ex is SqlException
                || ex is FormatException)
            {
                MessageBox.Show("Fill every column");
            }

            con.Close();
            ShowGrid();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            try
            {
                if (CheckExistence(NameBox.Text, con).Rows.Count >= 1)
                {
                    SqlCommand cmd = new SqlCommand("Update GameList set Release_Year=@Release_Year, Completed=@Completed where Name=@Name", con);
                    cmd.Parameters.AddWithValue("@Name", NameBox.Text);
                    cmd.Parameters.AddWithValue("@Release_Year", int.Parse(ReleaseBox.Text));
                    cmd.Parameters.AddWithValue("@Completed", CompletedComboBox.Text);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("This game does not exist.");
                }
            }
            catch (Exception ex) when (
                    ex is SqlException
                    || ex is FormatException)
            {
                MessageBox.Show("Fill every column");
            }

            con.Close();
            ShowGrid();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            try
            {
                if (CheckExistence(NameBox.Text, con).Rows.Count >= 1)
                {
                    SqlCommand cmd = new SqlCommand("Delete GameList where Name=@Name", con);
                    cmd.Parameters.AddWithValue("@Name", NameBox.Text);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("This game does not exist.");
                }
            }
            catch (Exception ex) when (
                ex is SqlException
                || ex is FormatException)
            {
                MessageBox.Show("Fill the Name column");
            }

            con.Close();
            ShowGrid();
        }

        //Can search games by name, relase year and/or completion
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            if (NameBox.Text == "" && ReleaseBox.Text == "" && CompletedComboBox.Text == "")
            {
                ShowGrid();
            }
            else
            {
                string Name = "";
                string ReleaseYear = "";
                string Completed = "";

                //Checking how the CommandString needs to be constructed.
                if (NameBox.Text != "")
                {
                    Name = " where Name like @Name";
                    if (ReleaseBox.Text != "")
                    {
                        ReleaseYear = " AND Release_Year like @Release_Year";
                        if (CompletedComboBox.Text != "")
                        {
                            Completed = " AND Completed=@Completed";
                        }
                    }
                    else if (CompletedComboBox.Text != "")
                    {
                        Completed = " AND Completed=@Completed";
                    }
                }
                else if (ReleaseBox.Text != "")
                {
                    ReleaseYear = " where Release_Year like @Release_Year";
                    if (CompletedComboBox.Text != "")
                    {
                        Completed = " AND Completed=@Completed";
                    }
                }
                else if (CompletedComboBox.Text != "")
                {
                    Completed = " where Completed=@Completed";
                }
                string CommandString = $"Select * from GameList{Name}{ReleaseYear}{Completed}";
                SqlCommand cmd = new SqlCommand(CommandString, con);
                if (NameBox.Text != "")
                {
                    cmd.Parameters.AddWithValue("@Name", $"%{NameBox.Text}%");
                }
                if (ReleaseBox.Text != "")
                {
                    cmd.Parameters.AddWithValue("@Release_Year", $"%{ReleaseBox.Text}%");
                }
                if (CompletedComboBox.Text != "")
                {
                    cmd.Parameters.AddWithValue("@Completed", CompletedComboBox.Text);
                }
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
