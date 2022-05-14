using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
namespace telebot
{
    public class AppSettings
    {
        private AppSettingsForm settings;
        internal class AppSettingsForm
        {
            [Newtonsoft.Json.JsonProperty("token")]
            public string token { get; set; }
        }

        public AppSettings()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            var json = File.ReadAllText(path);
            settings = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettingsForm>(json);

        }

        public string GetToken()
        {
            return settings.token;
        }

    }
}
