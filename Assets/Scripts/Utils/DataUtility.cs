using Zindeaxx.Saves;
using Newtonsoft.Json;

namespace Utils
{
    public static class DataUtility
    {
        static DataUtility()
        {
            SaveGame.EncryptionEnabled = true;
        }

        public static void Save(string key, object value)
        {
            string jsonNotation = JsonConvert.SerializeObject(value, Formatting.Indented);
            SaveGame.SetObjectValue(key, jsonNotation);
        }
        
        public static T Get<T>(string key)
        {
            string jsonNotation = SaveGame.GetStringValue(key);
            return JsonConvert.DeserializeObject<T>(jsonNotation);
        }

        public static void Clear(string key)
        {
            SaveGame.DestroySave();
        }
    }
}