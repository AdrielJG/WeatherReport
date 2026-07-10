using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class location : System.Web.UI.Page
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
                    MenuItem saved = new MenuItem("Bookmarked Locations", "Locations");
                }
                else
                {
                    LoginPanel.Visible = true;
                    WelcomePanel.Visible = false;
                }

                Menu1.Items[3].Text = "<span class='search'><i class=\"fa-solid fa-magnifying-glass\"></i></span>";

                ScriptManager.RegisterStartupScript(this, GetType(), "GetUserLocation", "getUserLocation();", true);
                Console.WriteLine(hiddenCityName.Value);
            }
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

        public void SaveCityName()
        {
            string cityName = hiddenCityName.Value;
            string apikey = "c4a4479a3686031556cd91adaf097eac";
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", cityName, apikey);

            try
            {
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
                    windLabel.Text = $"Winds: {weatherInfo.wind.speed}°C&nbsp;&nbsp;&nbsp;&nbsp;Pressure: {weatherInfo.main.pressure} hPa";
                    weatherDiv.Controls.Add(windLabel);

                    weatherDiv.Controls.Add(new LiteralControl("<br>"));

                    Label timezone = new Label();
                    timezone.Text = $"Timezone: {weatherInfo.timezone}";
                    weatherDiv.Controls.Add(timezone);

                    updatePanel.ContentTemplateContainer.Controls.Add(weatherDiv);

                    updatePanel.Update();
                }
            }
            catch(Exception ex)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            string menuItemVal = e.Item.Value;
            if (menuItemVal == "Bookmarked Cities")
            {
                if (Session["username"] != null)
                {

                }
                else
                {
                    string alertScript = "alert('You have to be logged in to use this feature.');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", alertScript, true);
                }
            }
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            SaveCityName();
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