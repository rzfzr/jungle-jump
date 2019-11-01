using System;
using System.Collections.Generic;

namespace InfiniteHopper.Types
{
    [Serializable]
    public class DataCloud
    {
        //Last score
        public int lastScore;
        //Current highscore
        public int highScore;
        //Total amount of score
        public int totalScore;
        //Total of feathers
        public int tokens;
        //Current longest distance
        public int longestDistance;
        //Current longest streak
        public int longestStreak;
        //Total of powerups
        public int totalPowerups;
        //Current longest powerup
        public int longestPowerUpStreak;
        //Total of game played
        public int rounds;
    }
}
