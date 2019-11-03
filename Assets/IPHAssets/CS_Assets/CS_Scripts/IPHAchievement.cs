using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfiniteHopper.Types;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper {
    public class IPHAchievement : MonoBehaviour {

        public enum Type {
            score,
            column,
            power,
            totalScore,
            run,
            feather,
            unlock
        }

        // public IPHAchievement (string text, string achievementType, string achievementValue) {

        // }

        /// <summary>
        /// Referente to score text object
        /// </summary>
        [SerializeField]
        public Text text;

        /// <summary>
        /// Value of score that show in UI
        /// </summary>
        public string achievementText { get; set; }
        public Type achievementType;
        public string achievementValue;
        // private Type Type1 { get => type1; set => type1 = value; }

        void Start () {
            // print (achievementText);
            text.text = achievementText;

        }

        /// <summary>
        /// Play again the level of this score
        /// </summary>
        // public void PlayAgain () {
        //     Debug.LogWarning ("Load Level");
        // }

        // public void Replay () {
        //     Debug.LogWarning ("Load Replay");
        // }

        bool CheckAchievement () {

            return true;

            return false;
        }

    }
}