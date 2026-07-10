using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static WebApplication1.form;
using Newtonsoft.Json;
using System.Net;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class form : System.Web.UI.Page
    {
        private LinkButton lb;
        protected void Page_Load(object sender, EventArgs e)
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

            GetWeatherInfo();
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            string menuItemVal = e.Item.Value;
            if(menuItemVal == "Bookmarked Cities")
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

        protected void GetWeatherInfo()
        {
            try
            {
                string[] cities = { "Abidjan", "Abu%20Dhabi", "Abuja", "Accra", "Addis%20Ababa", "Ahmedabad", "Aleppo", "Alexandria", "Algiers", "Almaty", "Amman", "Amsterdam", "Anchorage", "Andorra%20la%20Vella", "Ankara", "Antananarivo", "Apia", "Arnold", "Ashgabat", "Asmara", "Asuncion", "Athens", "Auckland", "Avarua", "Baghdad", "Baku", "Bamako", "Banda%20Aceh", "Bandar%20Seri%20Begawan", "Bandung", "Bangkok", "Bangui", "Banjul", "Barcelona", "Barranquilla", "Basrah", "Basse-Terre", "Basseterre", "Beijing", "Beirut", "Bekasi", "Belem", "Belgrade", "Belmopan", "Belo%20Horizonte", "Bengaluru", "Berlin", "Bern", "Bishkek", "Bissau", "Bogota", "Brasilia", "Bratislava", "Brazzaville", "Bridgetown", "Brisbane", "Brussels", "Bucharest", "Budapest", "Buenos%20Aires", "Bujumbura", "Bursa", "Busan", "Cairo", "Cali", "Caloocan", "Camayenne", "Canberra", "Cape%20Town", "Caracas", "Casablanca", "Castries", "Cayenne", "Charlotte%20Amalie", "Chengdu", "Chennai", "Chicago", "Chisinau", "Chittagong", "Chongqing", "Colombo", "Conakry", "Copenhagen", "Cordoba", "Curitiba", "Daegu", "Daejeon", "Dakar", "Dallas", "Damascus", "Dar%20es%20Salaam", "Delhi", "Denver", "Dhaka", "Dili", "Djibouti", "Dodoma", "Doha", "Dongguan", "Douala", "Douglas", "Dubai", "Dublin", "Durban", "Dushanbe", "Faisalabad", "Fort-de-France", "Fortaleza", "Freetown", "Fukuoka", "Funafuti", "Gaborone", "George%20Town", "Georgetown", "Gibraltar", "Gitega", "Giza", "Guadalajara", "Guangzhou", "Guatemala%20City", "Guayaquil", "Gujranwala", "Gustavia", "Gwangju", "Hamburg", "Hanoi", "Harare", "Havana", "Helsinki", "Ho%20Chi%20Minh%20City", "Hong%20Kong", "Honiara", "Honolulu", "Houston", "Hyderabad", "Hyderabad", "Ibadan", "Incheon", "Isfahan", "Islamabad", "Istanbul", "Izmir", "Jaipur", "Jakarta", "Jeddah", "Jerusalem", "Johannesburg", "Juarez", "Juba", "Kabul", "Kaduna", "Kampala", "Kano", "Kanpur", "Kaohsiung", "Karachi", "Karaj", "Kathmandu", "Kawasaki", "Kharkiv", "Khartoum", "Khulna", "Kigali", "Kingsburg", "Kingston", "Kingstown", "Kinshasa", "Kobe", "Kolkata", "Kota%20Bharu", "Kowloon", "Kuala%20Lumpur", "Kumasi", "Kuwait", "Kyiv", "Kyoto", "La%20Paz", "Lagos", "Lahore", "Libreville", "Lilongwe", "Lima", "Lisbon", "Ljubljana", "Lome", "London", "Los%20Angeles", "Luanda", "Lubumbashi", "Lusaka", "Luxembourg", "Macau", "Madrid", "Majuro", "Makassar", "Malabo", "Male", "Mamoudzou", "Managua", "Manama", "Manaus", "Manila", "Maputo", "Maracaibo", "Maracay", "Mariehamn", "Marigot", "Maseru", "Mashhad", "Mbabane", "Mecca", "Medan", "Medellin", "Medina", "Melbourne", "Mexico%20City", "Miami", "Minsk", "Mogadishu", "Monaco", "Monrovia", "Montevideo", "Montreal", "Moroni", "Moscow", "Mosul", "Multan", "Mumbai", "Muscat", "N'Djamena", "Nagoya", "Nairobi", "Nanchong", "Nanjing", "Nassau", "Nay%20Pyi%20Taw", "New%20York", "Niamey", "Nicosia", "Nouakchott", "Noumea", "Novosibirsk", "Nuku'alofa", "Nur-Sultan", "Nuuk", "Oranjestad", "Osaka", "Oslo", "Ottawa", "Ouagadougou", "Pago%20Pago", "Palembang", "Palo%20Alto", "Panama", "Papeete", "Paramaribo", "Paris", "Perth", "Philadelphia", "Phnom%20Penh", "Phoenix", "Podgorica", "Port%20Louis", "Port%20Moresby", "Port%20of%20Spain", "Port-Vila", "Port-au-Prince", "Porto%20Alegre", "Porto-Novo", "Prague", "Praia", "Pretoria", "Pristina", "Puebla", "Pune", "Pyongyang", "Quezon%20City", "Quito", "Rabat", "Rawalpindi", "Recife", "Reykjavik", "Riga", "Rio%20de%20Janeiro", "Riyadh", "Road%20Town", "Rome", "Roseau", "Saint%20George's", "Saint%20Helier", "Saint%20John's", "Saint%20Peter%20Port", "Saint%20Petersburg", "Saint-Denis", "Saint-Pierre", "Saipan", "Salvador", "San%20Antonio", "San%20Diego", "San%20Francisco", "San%20Jose", "San%20Juan", "San%20Marino", "San%20Salvador", "Sanaa", "Santa%20Cruz%20de%20la%20Sierra", "Santiago", "Santo%20Domingo", "Sao%20Paulo", "Sao%20Tome", "Sapporo", "Sarajevo", "Seattle", "Semarang", "Seoul", "Shanghai", "Sharjah", "Shenzhen", "Singapore", "Skopje", "Sofia", "South%20Tangerang", "Soweto", "Stockholm", "Sucre", "Surabaya", "Surat", "Suva", "Sydney", "Tabriz", "Taipei", "Tallinn", "Tangerang", "Tarawa", "Tashkent", "Tbilisi", "Tegucigalpa", "Tehran", "Tel%20Aviv", "Thimphu", "Tianjin", "Tijuana", "Tirana", "Tokyo", "Toronto", "Torshavn", "Tripoli", "Tunis", "Ulan%20Bator", "Vaduz", "Valencia", "Valletta", "Vancouver", "Victoria", "Vienna", "Vientiane", "Vilnius", "Warsaw", "Washington", "Wellington", "Willemstad", "Windhoek", "Wuhan", "Xi'an", "Yamoussoukro", "Yangon", "Yaounde", "Yekaterinburg", "Yerevan", "Yokohama", "Zagreb" };
                double[] lon = { -4.00167, 54.39696, 7.49508, -0.1969, 38.74689, 72.58727, 37.16117, 29.91582, 3.08746, 76.92861, 35.94503, 4.88969, -149.90028, 1.52109, 32.85427, 47.53613, -171.76666, -120.351935, 58.38333, 38.93184, -57.647, 23.72784, 174.76349, -159.77545, 44.40088, 49.89201, -8, 95.33333, 114.94006, 107.60694, 100.50144, 18.55496, -16.57803, 2.15899, -74.78132, 47.7804, -61.73214, -62.72499, 116.39723, 35.50157, 106.9896, -48.50444, 20.46513, -88.76667, -43.93778, 77.59369, 13.41053, 7.44744, 74.59, -15.59767, -74.08175, -47.92972, 17.10674, 15.28318, -59.62021, 153.02809, 4.34878, 26.10626, 19.04045, -58.37723, 29.36142, 29.06013, 129.03004, 31.24967, -76.5225, 120.96788, -13.68778, 149.12807, 18.42322, -66.87919, -7.61138, -61.00614, -52.33333, -64.9307, 104.06667, 80.27847, -87.65005, 28.8575, 91.83168, 106.55278, 79.84868, -13.67729, 12.56553, -64.18105, -49.27306, 128.59111, 127.38493, -17.44406, -96.80667, 36.29128, 39.26951, 77.23149, -104.9847, 90.40744, 125.57361, 43.14503, 35.73947, 51.53096, 113.74866, 9.70428, -4.48333, 55.30927, -6.24889, 31.0292, 68.77905, 73.08969, -61.07418, -38.54306, -13.2356, 130.41667, 179.19417, 25.90859, -81.37436, -58.15527, -5.35257, 29.92463, 31.20861, -103.39182, 113.25, -90.51327, -79.88621, 74.18705, -62.84978, 126.91556, 9.99302, 105.84117, 31.05337, -82.38304, 24.93545, 106.62965, 114.17469, 159.95, -157.85833, -95.36327, 78.45636, 68.37366, 3.90591, 126.70515, 51.67462, 73.04329, 28.94966, 27.13838, 75.78781, 106.84513, 39.18624, 35.21633, 28.04363, -106.46084, 31.58247, 69.17233, 7.43879, 32.58219, 8.51672, 80.34975, 120.31333, 67.0104, 50.99155, 85.3206, 139.71722, 36.25272, 32.53241, 89.56439, 30.05885, -119.554, -76.79358, -61.22742, 15.31357, 135.183, 88.36304, 102.24333, 114.18333, 101.68653, -1.62443, 47.97833, 30.5238, 135.75385, -68.15, 3.39467, 74.35071, 9.45356, 33.78725, -77.02824, -9.13333, 14.50513, 1.22154, -0.12574, -118.24368, 13.23432, 27.47938, 28.28713, 6.13, 113.54611, -3.70256, 171.38027, 119.43194, 8.78166, 73.50916, 45.22878, -86.2504, 50.58565, -60.025, 120.9822, 32.58322, -71.61245, -67.59113, 19.93481, -63.08302, 27.48333, 59.56796, 31.13333, 39.82563, 98.66667, -75.56359, 39.61417, 144.96332, -99.12766, -80.19366, 27.56667, 45.34375, 7.41667, -10.7969, -56.18816, -73.58781, 43.25506, 37.61556, 43.11889, 71.47824, 72.88261, 58.40778, 15.0444, 136.90641, 36.81667, 106.08473, 118.77778, -77.34306, 96.12972, -74.00597, 2.1098, 33.3642, -15.9785, 166.44884, 82.9346, -175.2018, 71.44598, -51.72157, -70.02703, 135.50218, 10.74609, -75.69812, -1.53388, -170.7025, 104.7458, -122.14302, -79.51973, -149.5665, -55.16682, 2.3488, 115.8614, -75.16379, 104.91601, -112.07404, 19.26361, 57.49889, 147.15089, -61.51889, 168.31366, -72.33881, -51.23019, 2.60359, 14.42076, -23.51254, 28.18783, 21.16688, -98.20346, 73.85535, 125.75432, 121.0509, -78.52495, -6.83255, 73.0479, -34.88111, -21.89541, 24.10589, -43.18223, 46.72185, -64.62079, 12.51133, -61.38808, -61.75226, -2.10491, -61.84329, -2.53527, 30.31413, 55.4504, -56.1773, 145.7545, -38.51083, -98.49363, -117.16472, -122.41942, -84.08333, -66.10572, 12.44639, -89.18718, 44.20667, -63.18117, -70.64827, -69.89232, -46.63611, 6.72732, 141.35, 18.35644, -122.33207, 110.42083, 126.9784, 121.45806, 55.41206, 114.0683, 103.85007, 21.43141, 23.32415, 106.71789, 27.85849, 18.06871, -65.26274, 112.75083, 72.83023, 178.44149, 151.20732, 46.2919, 121.53185, 24.75353, 106.63, 172.97696, 69.21627, 44.83368, -87.20681, 51.42151, 34.78057, 89.64191, 117.17667, -117.00371, 19.81889, 139.69171, -79.4163, -6.77164, 13.18733, 10.16579, 106.88324, 9.52154, -68.00765, 14.5148, -123.11934, 55.45501, 16.37208, 102.6, 25.2798, 21.01178, -77.03637, 174.77557, -68.93354, 17.08323, 114.26667, 108.92861, -5.27674, 96.15611, 11.51667, 60.6122, 44.51361, 139.65, 15.97798 };
                double[] lat = { 5.35444, 24.45118, 9.05785, 5.55602, 9.02497, 23.02579, 36.20124, 31.20176, 36.73225, 43.25667, 31.95522, 52.37403, 61.21806, 42.50779, 39.91987, -18.91368, -13.83333, 38.255366, 37.95, 15.33805, -25.28646, 37.98376, -36.84853, -21.2075, 33.34058, 40.37767, 12.65, 5.54167, 4.89035, -6.92222, 13.75398, 4.36122, 13.45274, 41.38879, 10.96854, 30.50852, 15.99714, 17.2955, 39.9075, 33.89332, -6.2349, -1.45583, 44.80401, 17.25, -19.92083, 12.97194, 52.52437, 46.94809, 42.87, 11.86357, 4.60971, -15.77972, 48.14816, -4.26613, 13.10732, -27.46794, 50.85045, 44.43225, 47.49835, -34.61315, -3.38193, 40.19559, 35.10168, 30.06263, 3.43722, 14.64953, 9.535, -35.28346, -33.92584, 10.48801, 33.58831, 13.9957, 4.93333, 18.3419, 30.66667, 13.08784, 41.85003, 47.00556, 22.3384, 29.56278, 6.93548, 9.53795, 55.67594, -31.4135, -25.42778, 35.87028, 36.34913, 14.6937, 32.78306, 33.5102, -6.82349, 28.65195, 39.73915, 23.7104, -8.55861, 11.58901, -6.17221, 25.28545, 23.01797, 4.04827, 54.15, 25.07725, 53.33306, -29.8579, 38.53575, 31.41554, 14.60365, -3.71722, 8.48714, 33.6, -8.52425, -24.65451, 19.2866, 6.80448, 36.14474, -3.42708, 30.00944, 20.66682, 23.11667, 14.64072, -2.19616, 32.15567, 17.89618, 35.15472, 53.55073, 21.0245, -17.82772, 23.13302, 60.16952, 10.82302, 22.27832, -9.43333, 21.30694, 29.76328, 17.38405, 25.39242, 7.37756, 37.45646, 32.65246, 33.72148, 41.01384, 38.41273, 26.91962, -6.21462, 21.49012, 31.76904, -26.20227, 31.72024, 4.85165, 34.52813, 10.52641, 0.31628, 12.00012, 26.46523, 22.61626, 24.8608, 35.83266, 27.70169, 35.52056, 49.98081, 15.55177, 22.80979, -1.94995, 36.5138, 17.99702, 13.15527, -4.32758, 34.6913, 22.56263, 6.12361, 22.31667, 3.1412, 6.68848, 29.36972, 50.45466, 35.02107, -16.5, 6.45407, 31.558, 0.39241, -13.96692, -12.04318, 38.71667, 46.05108, 6.12874, 51.50853, 34.05223, -8.83682, -11.66089, -15.40669, 49.61167, 22.20056, 40.4165, 7.08971, -5.14861, 3.75578, 4.17521, -12.78234, 12.13282, 26.22787, -3.10194, 14.6042, -25.96553, 10.66663, 10.23535, 60.09726, 18.06819, -29.31667, 36.31559, -26.31667, 21.42664, 3.58333, 6.25184, 24.46861, -37.814, 19.42847, 25.77427, 53.9, 2.03711, 43.73333, 6.30054, -34.90328, 45.50884, -11.70216, 55.75222, 36.335, 30.19679, 19.07283, 23.58413, 12.10672, 35.18147, -1.28333, 30.79508, 32.06167, 25.05823, 19.745, 40.71427, 13.51366, 35.17531, 18.08581, -22.27407, 55.0415, -21.13938, 51.1801, 64.18347, 12.52398, 34.69374, 59.91273, 45.41117, 12.36566, -14.27806, -2.91673, 37.44188, 8.9936, -17.53733, 5.86638, 48.85341, -31.95224, 39.95233, 11.56245, 33.44838, 42.44111, -20.16194, -9.47723, 10.66668, -17.73648, 18.54349, -30.03283, 6.49646, 50.08804, 14.93152, -25.74486, 42.67272, 19.03793, 18.51957, 39.03385, 14.6488, -0.22985, 34.01325, 33.59733, -8.05389, 64.13548, 56.946, -22.90642, 24.68773, 18.42693, 41.89193, 15.30174, 12.05288, 49.18804, 17.12096, 49.45981, 59.93863, -20.88231, 46.77914, 15.21233, -12.97111, 29.42412, 32.71571, 37.77493, 9.93333, 18.46633, 43.93667, 13.68935, 15.35472, -17.78629, -33.45694, 18.47186, -23.5475, 0.33654, 43.06667, 43.84864, 47.60621, -6.99306, 37.566, 31.22222, 25.33737, 22.54554, 1.28967, 41.99646, 42.69751, -6.28862, -26.26781, 59.32938, -19.03332, -7.24917, 21.19594, -18.14161, -33.86785, 38.08, 25.04776, 59.43696, -6.17806, 1.3278, 41.26465, 41.69411, 14.0818, 35.69439, 32.08088, 27.46609, 39.14222, 32.5027, 41.3275, 35.6895, 43.70011, 62.00973, 32.88743, 36.81897, 47.90771, 47.14151, 10.16202, 35.89968, 49.24966, -4.62001, 48.20849, 17.96667, 54.68916, 52.22977, 38.89511, -41.28664, 12.1084, -22.55941, 30.58333, 34.25833, 6.82055, 16.80528, 3.86667, 56.8519, 40.18111, 35.43333, 45.81444 };
                string apikey = "c4a4479a3686031556cd91adaf097eac";
                Table dynamicTable = new Table();
                for (int i = 0; i < 12; i++)
                {
                    string url = string.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}", lat[i], lon[i], apikey);

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
                        tempLabel.Text = $"Min: {Math.Round((weatherInfo.main.temp_min - 273.15), 1)}°C&nbsp;&nbsp;&nbsp;&nbsp;Max: {Math.Round((weatherInfo.main.temp_max - 273.15), 1)}°C<br>Day: {Math.Round((weatherInfo.main.temp - 273.15), 1)}°C&nbsp;&nbsp;&nbsp;&nbsp;Night: {Math.Round((weatherInfo.main.feels_like - 273.15), 1)}°C";
                        weatherDiv.Controls.Add(tempLabel);

                        if (Session["username"]  != null)
                        {
                            string cityName = weatherInfo.name.ToString();
                            string countryCode = weatherInfo.sys.country.ToString();
                            string weatherIconUrl = string.Format("http://openweathermap.org/img/w/{0}.png", weatherInfo.weather[0].icon);
                            string weatherDescription = weatherInfo.weather[0].description.ToString();
                            string humidity = weatherInfo.main.humidity.ToString();
                            string minTemperature = weatherInfo.main.temp_min.ToString();
                            string maxTemperature = weatherInfo.main.temp_max.ToString();
                            string dayTemperature = weatherInfo.main.temp.ToString();
                            string nightTemperature = weatherInfo.main.feels_like.ToString();
                        
                            weatherDiv.Controls.Add(new LiteralControl("<br>"));
                            weatherDiv.Controls.Add(new LiteralControl("<br>"));
                            weatherDiv.Controls.Add(new LiteralControl("<br>"));

                            lb = new LinkButton();
                            lb.Text = "<i class=\"fa-solid fa-bookmark\"></i>";
                            lb.ForeColor = System.Drawing.Color.Black;
                            weatherDiv.Controls.Add(lb);
                            lb.Click += (sender, e) => LinkButton_Click(cityName, countryCode, weatherIconUrl, weatherDescription, humidity, minTemperature, maxTemperature, dayTemperature, nightTemperature, sender, e);
                        }

                        tblweather.Controls.Add(weatherDiv);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        protected void LinkButton_Click(string cityName, string countryCode, string weatherIconUrl, string weatherDescription, string humidity, string minTemperature, string maxTemperature, string dayTemperature, string nightTemperature, object sender, EventArgs e)
        {
            string path = "server=localhost;user=root;password=admin123;database=project;";
            MySqlConnection conn = new MySqlConnection(path);
            string sql = "insert into bookmarks values(@username, @cityName, @countryCode, @weatherIconUrl, @weatherDescription, @humidity, @minTemperature, @maxTemperature, @dayTemperature, @nightTemperature)";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", Session["username"]);
                cmd.Parameters.AddWithValue("@cityName", cityName);
                cmd.Parameters.AddWithValue("@countryCode", countryCode);
                cmd.Parameters.AddWithValue("@weatherIconUrl", weatherIconUrl);
                cmd.Parameters.AddWithValue("@weatherDescription", weatherDescription);
                cmd.Parameters.AddWithValue("@humidity", humidity);
                cmd.Parameters.AddWithValue("@minTemperature", minTemperature);
                cmd.Parameters.AddWithValue("@maxTemperature", maxTemperature);
                cmd.Parameters.AddWithValue("@dayTemperature", dayTemperature);
                cmd.Parameters.AddWithValue("@nightTemperature", nightTemperature);

                int numrows = cmd.ExecuteNonQuery();
                if(numrows > 0)
                {
                    lb.Text += "<i class=\"fa-solid fa-check\"></i>";
                }
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

