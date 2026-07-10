using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = txt1.Text;
            string password = txt2.Text;
            bool isAuthenticated = false;

            string path = "server=localhost;user=root;password=admin123;database=project;";
            MySqlConnection conn = new MySqlConnection(path);
            string sql = "select * from accounts where username = @username and password = @password";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        isAuthenticated = true;
                    }
                    else
                    {
                        isAuthenticated = false;
                    }
                }
            }
            catch (Exception ex)
            {
                msg.Text += ex.Message;
            }

            if (isAuthenticated)
            {
                Session["username"] = username;
                Response.Redirect("~/form.aspx");
            }
            else
            {
                msg.Text = "Login Failed";
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }
    }
}