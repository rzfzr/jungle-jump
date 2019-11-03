using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper {
    public class IPHAchievements : MonoBehaviour {
        /// <summary>
        /// Reference to score prefab that instantiate in list of highscores
        /// </summary>
        [SerializeField]
        private InfiniteHopper.IPHAchievement achievementPrefab;
        /// <summary>
        /// Reference to list of highscores
        /// </summary>
        [SerializeField]
        private Transform list;

        [SerializeField]
        public TextAsset csv;

        void Start () { }

        void OnEnable () {
            ClearList ();

            string[, ] grid = CSVReader.SplitCsvGrid (csv.text);
            for (int line = 1; line < grid.GetUpperBound (1); line++) { //starts at 1 because 0 is labels
                GameObject go = Instantiate (achievementPrefab.gameObject) as GameObject;
                go.transform.SetParent (list, false);
                IPHAchievement achievement = go.GetComponent<IPHAchievement> ();
                achievement.achievementText = grid[0, line];
                achievement.tag = grid[1, line];
                achievement.achievementValue = int.Parse (grid[2, line]);
            }
            // IPHAchievement.Type type;
            // System.Enum.TryParse (grid[1, y], true, out type);
        }

        private void ClearList () {
            foreach (Transform t in list)
                Destroy (t.gameObject);
        }
    }
}