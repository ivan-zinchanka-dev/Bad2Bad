using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

namespace Zindeaxx.Saves
{
    public static class SaveGame
    {
        public const string SaveFileName = @"userData.dat";


        /// <summary>
        /// This stores the location of the save data folder.
        /// </summary>
        public static string SaveFolderLocation => Path.Combine(Application.persistentDataPath);

        /// <summary>
        /// This stores the location of the main save file.
        /// </summary>
        public static string SaveFileLocation => Path.Combine(SaveFolderLocation, SaveFileName);


        public static bool EncryptionEnabled = true;
        private const string SaveFileKey = "BHaPquE6NQnVnfuVgbtJg75x2QzmoFySxpET6";

        /// <summary>
        /// This returns true if a save file is present. If not a save will be created. Returns false if a file cannot be found or created!
        /// </summary>
        public static bool SaveExists => File.Exists(SaveFileLocation);



        private static List<SaveGameEntry> m_SaveKeys;
        /// <summary>
        /// This stores all the current keys stored in the games memory
        /// </summary>
        public static List<SaveGameEntry> SaveValues => m_SaveKeys;


        public static void DestroySave()
        {
            File.Delete(SaveFileLocation);
        }

        /// <summary>
        /// Returns a given keys value as a string.
        /// </summary>
        public static string GetStringValue(string Key)
        {
            return GetObjectValue(Key)?.ToString() ?? ""; ;
        }

        /// <summary>
        /// Returns a given keys value as a float number.
        /// </summary>
        public static float GetFloatValue(string Key)
        {
            float? floatValue = null;
            if (float.TryParse(GetObjectValue(Key)?.ToString(), out float parsedValue))
            {
                floatValue = parsedValue;
            }
            return floatValue ?? 0.0f;
        }

        /// <summary>
        /// Returns a given keys value as a float number.
        /// </summary>
        public static int GetIntValue(string Key)
        {
            int? intValue = null;
            if (int.TryParse(GetObjectValue(Key)?.ToString(), out int parsedValue))
            {
                intValue = parsedValue;
            }
            return intValue ?? 0;
        }

        public static Vector4 GetVectorValue(string Key)
        { //Complex but loads safely from save if key doesnt exist
            Vector4 myVector = GetObjectValue(Key) as Vector4? ?? Vector4.zero;
            return myVector;
        }

        public static bool HasKey (string Key)
        {
            if (SaveValues == null)
                RefreshSaveKeys();

            return SaveValues.Any(x => x.Key.ToLower() == Key.ToLower());
        }

        private static object GetValue (string Key)
        {
            return SaveValues.Find(x => x.Key.ToLower() == Key.ToLower()).Value;
        }

        private static void SetValue (string Key, object value)
        {
            SaveValues.Find(x => x.Key.ToLower() == Key.ToLower()).Value = value;
        }

        public static object GetObjectValue (string Key)
        {
            CheckSaveValues();

            if (HasKey(Key))
            {
                return GetValue(Key);
            }
            return null;
        }

        public static T[] GetArray<T>(string key)
        {
            return (GetObjectValue(key) as T[]) ?? new T[0];
        }

        /// <summary>
        /// Writes a given Key value as a string to the save file.
        /// </summary>
        public static void SetObjectValue(string Key, object Value)
        {
            string actualKey = Key.ToLower();

            CheckSaveValues();

            if (HasKey(Key))
            {
                SetValue(Key, Value);
            }
            else
            {
                m_SaveKeys.Add(new SaveGameEntry(actualKey, Value));
            }

            SaveKeys();
        }

        private static void CheckSaveValues()
        {
            if (SaveValues is null)
                RefreshSaveKeys();
        }

        private static void RefreshSaveKeys()
        {

            m_SaveKeys = new List<SaveGameEntry>();

            string saveString = "";
            if (File.Exists(SaveFileLocation))
                 saveString = GetSaveString();

            if (!string.IsNullOrEmpty(saveString))
            {
                SaveDataStrcuture data = JsonConvert.DeserializeObject<SaveDataStrcuture>(saveString);
                if(data.Entries != null && data.Entries.Length > 0)
                m_SaveKeys = new List<SaveGameEntry>(data.Entries);
            }
        }

        private static void SaveKeys()
        {
            SaveData();
            RefreshSaveKeys();
        }

        private static string GetSaveString ()
        {
            if (EncryptionEnabled)
                return SaveGameEncryption.Decrypt(File.ReadAllBytes(SaveFileLocation), SaveFileKey);
            else
                return File.ReadAllText(SaveFileLocation);
        }

        private static void SaveData()
        {
            // Create a new SaveData object and set its Entries property to the SaveValues array
            SaveDataStrcuture savedata = new SaveDataStrcuture { Entries = SaveValues.ToArray() };

            // Serialize the SaveData object to JSON
            string data = JsonConvert.SerializeObject(savedata);

            if (EncryptionEnabled)
                File.WriteAllBytes(SaveFileLocation, SaveGameEncryption.Encrypt(data, SaveFileKey));
            else
                File.WriteAllText(SaveFileLocation, data);
        }


        [Serializable]
        public class SaveDataStrcuture
        {
            public SaveGameEntry[] Entries;
        }

        [Serializable]
        public class SaveGameEntry
        {
            public string Key;
            public object Value;

            public SaveGameEntry(string key, object value)
            {
                Key = key;
                Value = value;
            }
        }
    }

}