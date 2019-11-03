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
            for (int y = 1; y < grid.GetUpperBound (1); y++) {
                GameObject go = Instantiate (achievementPrefab.gameObject) as GameObject;
                go.transform.SetParent (list, false);
                // print (y);
                IPHAchievement achievement = go.GetComponent<IPHAchievement> ();

                // for (int x = 0; x < grid.GetUpperBound (0); x++) {
                // if (x == 0) {

                achievement.achievementText = grid[0, y];
                // achievement.achievementText = grid[x, y];
                // } else if (x == 1) {
                IPHAchievement.Type type;
                System.Enum.TryParse (grid[1, y], true, out type);
                achievement.achievementType = type;
                // } else if (x == 2) {

                achievement.achievementValue = grid[2, y];
                // }
                // }

            }
        }

        private void ClearList () {
            foreach (Transform t in list)
                Destroy (t.gameObject);
        }
    }
}