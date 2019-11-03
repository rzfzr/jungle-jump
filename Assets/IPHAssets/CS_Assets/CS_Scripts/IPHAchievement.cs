using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfiniteHopper.Types;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper {
    public class IPHAchievement : MonoBehaviour {
        /// <summary>
        /// Referente to score text object
        /// </summary>
        [SerializeField]
        private Text achievementValue;

        /// <summary>
        /// Value of score that show in UI
        /// </summary>
        public string Achievement { get; set; }

        // Start is called before the first frame update
        void Start () {
            achievementValue.text = Achievement;
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
    }
}