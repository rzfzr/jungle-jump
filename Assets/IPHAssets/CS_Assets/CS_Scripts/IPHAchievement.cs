using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfiniteHopper.Types;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper {
    public class IPHAchievement : MonoBehaviour {
        // public enum Type {
        //     score,
        //     column,
        //     power,
        //     totalScore,
        //     run,
        //     feather,
        //     unlock
        // }
        // public IPHAchievement (string text, string achievementType, string achievementValue) {
        // }

        [SerializeField]
        public Text text;

        [SerializeField]
        private Image icon;

        public string achievementText { get; set; }
        // public Type achievementType;
        public int achievementValue;
        // private Type Type1 { get => type1; set => type1 = value; }

        // public bool isAchieved;

        void Start () {
            text.text = achievementText;
            // isAchieved = CheckAchievement ();
            if (CheckAchievement ()) { //if unlocked then brighten the achievement icon
                icon.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
            }
        }

        //check whether the achievement is unlocked or not
        bool CheckAchievement () {
            if (GetCurrentData () >= achievementValue) return true;
            else return false;
        }

        //get the current quantity of this achievement goal
        int GetCurrentData () {
            if (this.CompareTag ("highScore")) return IPHDataStorage.playerData.highScore;
            else if (this.CompareTag ("longestDistance")) return IPHDataStorage.playerData.longestDistance;
            else if (this.CompareTag ("totalPowerups")) return IPHDataStorage.playerData.totalPowerups;
            else if (this.CompareTag ("totalScore")) return IPHDataStorage.playerData.totalScore;
            else if (this.CompareTag ("rounds")) return IPHDataStorage.playerData.rounds;
            else if (this.CompareTag ("tokens")) return IPHDataStorage.playerData.tokens;
            else {
                print ("wrong tag on Achievement");
                return 0;
            }
        }

    }
}