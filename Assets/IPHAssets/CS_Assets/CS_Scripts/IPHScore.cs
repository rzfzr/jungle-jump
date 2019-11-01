using InfiniteHopper.Types;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper
{
    public class IPHScore : MonoBehaviour
    {
        /// <summary>
        /// Referente to score text object
        /// </summary>
        [SerializeField]
        private Text scoreValue;

        /// <summary>
        /// Value of score that show in UI
        /// </summary>
        public string Score { get; set; }


        // Start is called before the first frame update
        void Start()
        {
            scoreValue.text = Score;
        }

        /// <summary>
        /// Play again the level of this score
        /// </summary>
        public void PlayAgain()
        {
            Debug.LogWarning("Load Level");
        }

        public void Replay()
        {
            Debug.LogWarning("Load Replay");
        }
    }
}
