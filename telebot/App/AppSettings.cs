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
            [Newtonsoft.Json.JsonProperty("UsersDataBase")]
            public string DBsettings { get; set; }
        }

        public AppSettings()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "settings/appsettings.json");
            if (!File.Exists(path))
                Environment.Exit(1);
            var json = File.ReadAllText(path);
            settings = Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettingsForm>(json);

        }

        public string GetToken()
        {
            return settings.token;
        }
        public string GetDBPath()
        {
            return settings.DBsettings;
        }

    }
}
