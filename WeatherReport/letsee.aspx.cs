using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1
{
    public partial class letsee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            weatherDetails();
        }

        private LinkButton lb;

        protected async Task weatherDetails()
        {
            string name = "rahul";
            string path = "server=localhost;user=root;password=admin123;database=project;";
            MySqlConnection conn = new MySqlConnection(path);
            string sql = "select city from bookmarks where username=@username";
            string apikey = "c4a4479a3686031556cd91adaf097eac";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", name);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string city = reader["city"].ToString();
                            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", city, apikey);
                            using (HttpClient client = new HttpClient())
                            {
                                
                                root weatherInfo = JsonSerializer.Deserialize<root>(url);

                                HtmlGenericControl weatherDiv = new HtmlGenericControl("div");
                                weatherDiv.Attributes["class"] = "weather-info";

                                Label cityCountryLabel = new Label();
                                cityCountryLabel.Text = $"{weatherInfo.name}, {weatherInfo.sys.country}";
                                weatherDiv.Controls.Add(cityCountryLabel);

                                weatherDiv.Controls.Add(new LiteralControl("<br>"));

                                Image weatherIcon = new Image();
                                weatherIcon.ImageUrl = string.Format("http://openweathermap.org/img/w/{0}.png", weatherInfo.weather[0].icon);
                                weatherIcon.Width = 100;
                                weatherIcon.Height = 100;
                                weatherDiv.Controls.Add(weatherIcon);

                                weatherDiv.Controls.Add(new LiteralControl("<br>"));

                                Label descriptionLabel = new Label();
                                descriptionLabel.Text = $"{weatherInfo.weather[0].description}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Humidity: {weatherInfo.main.humidity}%";
                                weatherDiv.Controls.Add(descriptionLabel);

                                weatherDiv.Controls.Add(new LiteralControl("<br>"));

                                Label tempLabel = new Label();
                                tempLabel.Text = $"Min: {Math.Round((weatherInfo.main.temp_min - 273.15), 1)}°C&nbsp;&nbsp;&nbsp;&nbsp;Max: {Math.Round((weatherInfo.main.temp_max - 273.15), 1)}°C<br>Day: {Math.Round((weatherInfo.main.temp - 273.15), 1)}°C&nbsp;&nbsp;&nbsp;&nbsp;Night: {Math.Round((weatherInfo.main.feels_like - 273.15), 1)}°C";
                                weatherDiv.Controls.Add(tempLabel);
                                weatherDiv.Controls.Add(lb);

                                aweather.Controls.Add(weatherDiv);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
            }
        }

        protected void weatherUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Call the method to update weather information
            weatherDetails();
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