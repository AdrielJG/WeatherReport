using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string[][] accs = new string[][]
        {
            new string[] {"admin", "admin123"}
        };
            
        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = txt1.Text;
            string email = mail.Text;
            string password = pass.Text;
            string gender = "";
            if(RadioButton1.Checked == true)
            {
                gender += RadioButton1.Text;
            }
            else if(RadioButton2.Checked == true)
            {
                gender += RadioButton2.Text;
            }
            else if(RadioButton3.Checked == true)
            {
                gender += RadioButton3.Text;
            }

            string path = "server=localhost;user=root;password=admin123;database=project;";
            MySqlConnection conn = new MySqlConnection(path);
            string sql = "insert into accounts values(@username, @email, @password, @gender)";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@gender", gender);

                int numrows = cmd.ExecuteNonQuery();

                if(numrows > 0)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    lbl1.Text = "Couldn't create account!";
                }
            } 
            catch (Exception ex) 
            {
                lbl1.Text += ex.Message;
            }
            Response.Redirect("login.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}