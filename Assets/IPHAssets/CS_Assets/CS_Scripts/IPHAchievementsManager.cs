using System.Collections;
using System.Collections.Generic;
using System.IO;
using InfiniteHopper.Types;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper {
    public class IPHAchievementsManager : MonoBehaviour {
        [SerializeField]
        public TextAsset csv;

        [SerializeField]
        public static List<Achievement> achievements;

        public static IPHAchievementsManager instance;
        private void Awake () {
            if (instance != null) {
                Destroy (gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad (gameObject);
            achievements = LoadAchievements ();
        }

        List<Achievement> LoadAchievements () {

            // TextReader tr = new StreamReader(Application.dataPath + "/achievement.csv");
            // string json = tr.ReadToEnd();

            print ("getting achievements");
            List<Achievement> list = new List<Achievement> ();
            string[, ] grid = CSVReader.SplitCsvGrid (csv.text);
            for (int line = 1; line < grid.GetUpperBound (1); line++) { //starts at 1 because 0 is labels
                Achievement achievement = new Achievement (grid[0, line], grid[1, line], int.Parse (grid[2, line]));
                list.Add (achievement);
            }
            return list;
        }

        public static void CheckAchievements () {

            foreach (Achievement achievement in achievements) {

                if (achievement.CheckIsNewAchieved ()) {
                    print (achievement.name + " unlocked!!!");

                }
            }
        }
    }
}