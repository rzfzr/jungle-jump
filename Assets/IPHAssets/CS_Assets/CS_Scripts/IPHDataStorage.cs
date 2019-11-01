using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using InfiniteHopper.Types;
using System.Collections.Generic;

namespace InfiniteHopper
{
    /// <summary>
    /// Class that control the storage data
    /// </summary>
    public class IPHDataStorage : MonoBehaviour
    {
        /// <summary>
        /// Reference to singleton
        /// </summary>
        public static IPHDataStorage instance;
        /// <summary>
        /// The persistent player data
        /// </summary>
        public static DataCloud playerData;

        /// <summary>
        /// Create the singleton
        /// </summary>
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            playerData = LoadData();
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Called when destroy the object
        private void OnDestroy()
        {
            SaveData(playerData);    
        }

        /// <summary>
        /// Save score
        /// </summary>
        /// <param name="score"></param>
        public void AddNewHightscore(int score)
        {
            Debug.LogWarning("To Be Implemented");
        }

        /// <summary>
        /// Save the player data to json file
        /// </summary>
        /// <param name="data">Reference data to save</param>
        public void SaveData(DataCloud data)
        {
            string json = JsonUtility.ToJson(data);

            StreamWriter tw = new StreamWriter(Application.dataPath + "/data.sv");
            tw.Write(json);
            tw.Close();
            Debug.Log("Saving as JSON: " + json);
        }

        /// <summary>
        /// Load data from json
        /// </summary>
        /// <param name="json">Json to load data</param>
        /// <returns>Return instance of DataCloud</returns>
        public DataCloud LoadData()
        {
            if (!File.Exists(Application.dataPath + "/data.sv"))
                return new DataCloud();

            TextReader tr = new StreamReader(Application.dataPath + "/data.sv");
            string json = tr.ReadToEnd();

            Debug.Log("Load as JSON: " + json);

            DataCloud data = JsonUtility.FromJson<DataCloud>(json);

            return data;
        }

        /// <summary>
        /// Delete all data save and reset profile
        /// </summary>
        public void ResetData()
        {
            if (File.Exists(Application.dataPath + "/data.sv"))
                File.Delete(Application.dataPath + "/data.sv");

            PlayerPrefs.DeleteAll();

            playerData = new DataCloud();
        }
    }
}
