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
        private IPHAchievement achievementPrefab;
        /// <summary>
        /// Reference to list of highscores
        /// </summary>
        [SerializeField]
        private Transform list;

        // Start is called before the first frame update
        void OnEnable () {
            ClearList ();

            //TODO: Replace this mock data with real data.
            string[] MockData = new string[2] { "1000 Pontos em uma unica Jogada", "5000 Pontos em uma unica jogada" };

            for (int i = 0; i < MockData.Length; i++) {

                GameObject go = Instantiate (achievementPrefab.gameObject) as GameObject;
                go.transform.SetParent (list, false);

                IPHAchievement achievement = go.GetComponent<IPHAchievement> ();
                achievement.Achievement = MockData[i].ToString ();
            }
        }

        private void ClearList () {
            foreach (Transform t in list)
                Destroy (t.gameObject);
        }
    }
}