using System.Collections;
using System.Collections.Generic;
using InfiniteHopper.Types;
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

        void OnEnable () {
            ClearList ();
            foreach (Achievement achievement in IPHAchievementsManager.achievements) {

                GameObject go = Instantiate (achievementPrefab.gameObject) as GameObject;
                go.transform.SetParent (list, false);

                IPHAchievement iphAchievement = go.GetComponent<IPHAchievement> ();
                iphAchievement.name = achievement.name;
                iphAchievement.isAchieved = achievement.CheckIsAchieved ();

                //  new IPHAchievement (achievement.name, achievement.CheckIsAchieved ());
                // IPHAchievement iphAchievement = go.GetComponent<IPHAchievement> ();

                // achievement = go.GetComponent<IPHAchievement> ();
            }

        }

        private void ClearList () {
            foreach (Transform t in list)
                Destroy (t.gameObject);
        }
    }
}