using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Net;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;
namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null)
                {
                    LoginPanel.Visible = false;
                    WelcomePanel.Visible = true;
                    lblWelcome.Text = "Welcome, " + Session["username"].ToString();
                }
                else
                {
                    LoginPanel.Visible = true;
                    WelcomePanel.Visible = false;
                    Response.Redirect("form.aspx");
                }

                
                GetWeatherInfo();
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }

        static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            char[] chars = input.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);

            return new string(chars);
        }

        private LinkButton lb;

        protected void GetWeatherInfo()
        {
            try
            {
                string path = "server=localhost;user=root;password=admin123;database=project;";
                MySqlConnection conn = new MySqlConnection(path);
                string sql = "SELECT city FROM bookmarks WHERE username = @username";

                List<string> cityList = new List<string>();

                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", Session["username"]);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string city = reader["city"].ToString();
                        cityList.Add(city);
                    }
                }
                Label2.Text = cityList.Count.ToString();
                string[] cityarr = cityList.ToArray();
                string apikey = "c4a4479a3686031556cd91adaf097eac";
                foreach (string city in cityarr)
                {
                    string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", city, apikey);

                    using (WebClient client = new WebClient())
                    {
                        string json = client.DownloadString(url);
                        root weatherInfo = JsonConvert.DeserializeObject<root>(json);

                        HtmlGenericControl weatherContainer = new HtmlGenericControl("div");

                        Label cityCountryLabel = new Label();
                        cityCountryLabel.Text = $"{weatherInfo.name}, {weatherInfo.sys.country}";
                        weatherContainer.Controls.Add(cityCountryLabel);

                        weatherContainer.Controls.Add(new LiteralControl("<br>"));

                        Label descriptionLabel = new Label();
                        descriptionLabel.Text = $"{weatherInfo.weather[0].description}";
                        weatherContainer.Controls.Add(descriptionLabel);

                        tblweather.Controls.Add(weatherContainer);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }

        }

        protected void LinkButton_Click(string city, object sender, EventArgs e)
        {
            string path = "server=localhost;user=root;password=admin123;database=project;";
            MySqlConnection conn = new MySqlConnection(path);
            string sql = "delete from bookmarks where city=@city";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@city", city);

                int numrows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        public class coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class main
        {
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
        }

        public class wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
        }

        public class root
        {
            public coord coord { get; set; }
            public List<weather> weather { get; set; }
            public main main { get; set; }
            public wind wind { get; set; }
            public Sys sys { get; set; }
            public int timezone { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }

        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }
    }

}
