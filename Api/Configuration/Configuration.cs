using Newtonsoft.Json;
using System.Reflection;
using static Api.Configuration.Configuration.ConnectionString;

namespace Api.Configuration
{
    internal static class GlobalSettings
    {
        public static Configuration? Configuration { get; set; }
    }

    public class Configuration
    {
        public ConnectionString? ConnectionStrings { get; set; }
        public ConfigEncryptAES? ConfigEncryptAESs { get; set; }

        public static ConfigEncryptAES GetConfigEncryptAES()
        {
            return LoadJson().ConfigEncryptAESs ?? new ConfigEncryptAES();
        }
        public static ConnectionString GetConnectionString()
        {
            return LoadJson().ConnectionStrings ?? new ConnectionString();
        }

        private static Configuration LoadJson()
        {
            Configuration? configuration = null;
            if(GlobalSettings.Configuration == null)
            {
                string file = "appsettings.Development.json";
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if(path == null) 
                {
                    throw new ArgumentException();
                }
                var configPath = Path.Combine(path, file);
                if (!File.Exists(configPath))
                {
                    configPath = Path.Combine(path, file);
                }
                var reader = new JsonTextReader(new StringReader(File.ReadAllText(configPath)));
                var serializer = new JsonSerializer();
                configuration = serializer.Deserialize<Configuration>(reader);

                if (configuration == null)
                {
                    throw new ArgumentException();
                }
                GlobalSettings.Configuration = configuration;
            }
            return GlobalSettings.Configuration;
        }

        public class ConnectionString
        {
            public string? DefaultConnection { get; set; }
        }


        public class ConfigEncryptAES
        {
            public string Key { get; set; }
            public string Salt { get; set; }
            public string TypeAlgotihm { get; set; }
            public int Interactions { get; set; }
            public int LenghtKey { get; set; }
        }
    }
}
