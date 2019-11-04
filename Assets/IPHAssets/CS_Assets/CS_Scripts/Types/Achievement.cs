using System;
using UnityEngine;

namespace InfiniteHopper.Types {
    [Serializable]
    public class Achievement {

        public string name;
        public string type;
        public int value;

        public bool isAchieved;
        public Achievement (string name, string type, int value) {
            this.name = name;
            this.type = type;
            this.value = value;

        }

        //check whether the achievement is unlocked or not
        public bool CheckIsAchieved () {
            if (GetCurrentData () >= value) {
                isAchieved = true;
                return true;
            }
            return false;
        }

        //check whether it was just unlocked
        public bool CheckIsNewAchieved () {
            if (isAchieved) return false; //already achieved
            if (GetCurrentData () >= value) { //new achievement!
                isAchieved = true;
                return true;
            }
            return false;
        }

        //get the current quantity of this achievement goal
        public int GetCurrentData () {
            if (type == ("highScore")) return IPHDataStorage.playerData.highScore;
            if (type == ("longestDistance")) return IPHDataStorage.playerData.longestDistance;
            if (type == ("totalPowerups")) return IPHDataStorage.playerData.totalPowerups;
            if (type == ("totalScore")) return IPHDataStorage.playerData.totalScore;
            if (type == ("rounds")) return IPHDataStorage.playerData.rounds;
            if (type == ("tokens")) return IPHDataStorage.playerData.tokens;
            return -1; //wrong type
        }

    }
}

// public enum Type {
//     score,
//     column,
//     power,
//     totalScore,
//     run,
//     feather,
//     unlock
// }