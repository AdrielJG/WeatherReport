using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            getWeatherInfo();
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

        private Button lb;

        protected void getWeatherInfo()
        {
            string apikey = "c4a4479a3686031556cd91adaf097eac";
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", txt1.Text, apikey);

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                root weatherInfo = JsonConvert.DeserializeObject<root>(json);

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
                descriptionLabel.Text = $"{CapitalizeFirstLetter(weatherInfo.weather[0].description)}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Humidity: {weatherInfo.main.humidity}%";
                weatherDiv.Controls.Add(descriptionLabel);

                weatherDiv.Controls.Add(new LiteralControl("<br>"));

                Label tempLabel = new Label();
                tempLabel.Text = $"Min: {Math.Round((weatherInfo.main.temp_min - 273.15), 1)}°C&nbsp;&nbsp;&nbsp;&nbsp;Max: {Math.Round((weatherInfo.main.temp_max - 273.15), 1)}°C<br>Day: {Math.Round((weatherInfo.main.temp - 273.15), 1)}°C&nbsp;&nbsp;&nbsp;&nbsp;Night: {Math.Round((weatherInfo.main.feels_like - 273.15), 1)}°C<br>";
                weatherDiv.Controls.Add(tempLabel);

                Label windLabel = new Label();
                windLabel.Text = $"Winds: {weatherInfo.wind.speed}°C&nbsp;&nbsp;&nbsp;&nbsp;Pressure: {weatherInfo.main.pressure}";
                weatherDiv.Controls.Add(windLabel);

                Label timezone = new Label();
                timezone.Text = $"Timezone: {weatherInfo.timezone}";

                string cityName = weatherInfo.name.ToString();
                string countryCode = weatherInfo.sys.country.ToString();
                string weatherIconUrl = string.Format("http://openweathermap.org/img/w/{0}.png", weatherInfo.weather[0].icon);
                string weatherDescription = weatherInfo.weather[0].description.ToString();
                string humidity = weatherInfo.main.humidity.ToString();
                string minTemperature = weatherInfo.main.temp_min.ToString();
                string maxTemperature = weatherInfo.main.temp_max.ToString();
                string dayTemperature = weatherInfo.main.temp.ToString();
                string nightTemperature = weatherInfo.main.feels_like.ToString();

                if (Session["username"] != null)
                {
                    LinkButton1.Visible = true;
                    weatherDiv.Controls.Add(new LiteralControl("<br>"));
                    string weatherInfoJson = JsonConvert.SerializeObject(weatherInfo);
                    LinkButton1.CommandArgument = weatherInfoJson;
                    if (CheckIfBookmarked(Session["username"].ToString(), weatherInfo.name.ToString()))
                    {
                        LinkButton1.Text = "Remove";
                    }
                    else
                    {
                        LinkButton1.Text = "Bookmark";
                    }

                }
                else
                {
                    LinkButton1.Visible = false;
                }
                tblweather.Controls.Add(weatherDiv);
            }
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string weatherInfoJson = ((LinkButton)sender).CommandArgument;
            root weatherInfo = JsonConvert.DeserializeObject<root>(weatherInfoJson);
            string path = "server=localhost;user=root;password=admin123;database=project;";
            MySqlConnection conn = new MySqlConnection(path);
            string sql1 = "insert into bookmarks values(@username, @cityName, @countryCode, @weatherIconUrl, @weatherDescription, @humidity, @minTemperature, @maxTemperature, @dayTemperature, @nightTemperature)";
            string sql2 = "delete from bookmarks where user=@username and cityName=@cityName";

            try
            {
                conn.Open();
                if (CheckIfBookmarked(Session["username"].ToString(), weatherInfo.name))
                {
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@username", Session["username"]);
                    cmd2.Parameters.AddWithValue("@cityName", weatherInfo.name);
                    int numrow = cmd2.ExecuteNonQuery();
                    LinkButton1.Text = "Bookmark";
                }
                else
                {
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    cmd1.Parameters.AddWithValue("@username", Session["username"]);
                    cmd1.Parameters.AddWithValue("@cityName", weatherInfo.name);
                    cmd1.Parameters.AddWithValue("@countryCode", weatherInfo.sys.country);
                    cmd1.Parameters.AddWithValue("@weatherIconUrl", weatherInfo.weather[0].icon);
                    cmd1.Parameters.AddWithValue("@weatherDescription", weatherInfo.weather[0].description);
                    cmd1.Parameters.AddWithValue("@humidity", weatherInfo.main.humidity);
                    cmd1.Parameters.AddWithValue("@minTemperature", weatherInfo.main.temp_min);
                    cmd1.Parameters.AddWithValue("@maxTemperature", weatherInfo.main.temp_max);
                    cmd1.Parameters.AddWithValue("@dayTemperature", weatherInfo.main.temp);
                    cmd1.Parameters.AddWithValue("@nightTemperature", weatherInfo.main.feels_like);
                    LinkButton1.Text = "Remove";
                }

                // Make sure to close the connection
                conn.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
        }

        private bool CheckIfBookmarked(string username, string cityName)
        {
            string path = "server=localhost;user=root;password=admin123;database=project;";
            MySqlConnection conn = new MySqlConnection(path);
            string sql = "select * from bookmarks where user=@username and cityName=@cityName";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@cityName", cityName);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("form.aspx");
        }

        
    }
}