using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using static WebApplication1.dashboard;

namespace WebApplication1
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetWeatherInfo();

            if (Session["username"] != null)
            {
                LoginPanel.Visible = false;
                WelcomePanel.Visible = true;
                lblWelcome.Text = "Welcome, " + Session["username"].ToString();
                MenuItem saved = new MenuItem("Bookmarked Locations", "Locations");
            }
            else
            {
                LoginPanel.Visible = true;
                WelcomePanel.Visible = false;
                Response.Redirect("form.aspx");
            }

            Menu1.Items[3].Text = "<span class='search'><i class=\"fa-solid fa-magnifying-glass\"></i></span>";

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
                List<Bookmark> bookmarks = new List<Bookmark>();
                using (MySqlConnection conn = new MySqlConnection(path))
                {
                    string sql = "SELECT * FROM bookmarks WHERE user=@user";

                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", Session["username"]);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Bookmark bookmark = new Bookmark
                                {
                                    CityName = reader["cityName"].ToString(),
                                    CountryCode = reader["countryCode"].ToString(),
                                    WeatherIconUrl = reader["weatherIconUrl"].ToString(),
                                    WeatherDescription = reader["weatherDescription"].ToString(),
                                    Humidity = reader["humidity"].ToString(),
                                    MinTemperature = double.Parse(reader["minTemperature"].ToString()),
                                    MaxTemperature = double.Parse(reader["maxTemperature"].ToString()),
                                    DayTemperature = double.Parse(reader["dayTemperature"].ToString()),
                                    NightTemperature = double.Parse(reader["nightTemperature"].ToString())
                                };

                                bookmarks.Add(bookmark); 
                            }
                        }
                    }
                }

                foreach (var bookmark in bookmarks)
                {
                    HtmlGenericControl weatherDiv = new HtmlGenericControl("div");
                    weatherDiv.Attributes["class"] = "weather-info";

                    Label cityCountryLabel = new Label();
                    cityCountryLabel.Text = $"{bookmark.CityName}, {bookmark.CountryCode}";
                    weatherDiv.Controls.Add(cityCountryLabel);

                    weatherDiv.Controls.Add(new LiteralControl("<br>"));

                    Image weatherIcon = new Image();
                    weatherIcon.ImageUrl = bookmark.WeatherIconUrl;
                    weatherIcon.Width = 100;
                    weatherIcon.Height = 100;
                    weatherDiv.Controls.Add(weatherIcon);

                    weatherDiv.Controls.Add(new LiteralControl("<br>"));

                    Label descriptionLabel = new Label();
                    descriptionLabel.Text = $"{CapitalizeFirstLetter(bookmark.WeatherDescription)}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Humidity: {bookmark.Humidity}%";
                    weatherDiv.Controls.Add(descriptionLabel);

                    weatherDiv.Controls.Add(new LiteralControl("<br>"));

                    Label tempLabel = new Label();
                    tempLabel.Text = $"Min: {Math.Round((bookmark.MinTemperature - 273.15), 1)}°C&nbsp;&nbsp;&nbsp;&nbsp;Max: {Math.Round((bookmark.MaxTemperature - 273.15), 1)}°C<br>Day: {Math.Round((bookmark.DayTemperature - 273.15), 1)}°C&nbsp;&nbsp;&nbsp;&nbsp;Night: {Math.Round((bookmark.NightTemperature - 273.15), 1)}°C";
                    weatherDiv.Controls.Add(tempLabel);

                    string cityN = bookmark.CityName;

                    weatherDiv.Controls.Add(new LiteralControl("<br>"));
                    weatherDiv.Controls.Add(new LiteralControl("<br>"));

                    lb = new LinkButton();
                    lb.Text = "<i class=\"fa-solid fa-xmark\"></i>";
                    weatherDiv.Controls.Add(lb);
                    lb.Click += (sender, e) => LinkButton_Click(cityN, sender, e);

                    tblweather.Controls.Add(weatherDiv);
                }

                Label2.Text = bookmarks.Count.ToString(); 
            }
            catch (Exception ex)
            {
            }
        }

        protected void LinkButton_Click(string cityN, object sender, EventArgs e)
        {
            string path = "server=localhost;user=root;password=admin123;database=project;";
            MySqlConnection conn = new MySqlConnection(path);
            string sql = "delete from bookmarks where cityName=@cityN";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cityN", cityN);

                int numrows = cmd.ExecuteNonQuery();
                if (numrows > 0)
                {
                    Response.Redirect("dashboard.aspx");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        public class Bookmark
        {
            public string CityName { get; set; }
            public string CountryCode { get; set; }
            public string WeatherIconUrl { get; set; }
            public string WeatherDescription { get; set; }
            public string Humidity { get; set; }
            public double MinTemperature { get; set; }
            public double MaxTemperature { get; set; }
            public double DayTemperature { get; set; }
            public double NightTemperature { get; set; }
        }
    }
}