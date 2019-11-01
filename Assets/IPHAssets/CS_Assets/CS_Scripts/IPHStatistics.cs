using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace InfiniteHopper
{
    public class IPHStatistics : MonoBehaviour
    {
        [SerializeField]
        private Text m_textDistance;
        [SerializeField]
        private Text m_textStreak;
        [SerializeField]
        private Text m_textTokens;
        [SerializeField]
        private Text m_textPowerups;
        [SerializeField]
        private Text m_textPowerupStreak;
        [SerializeField]
        private Text m_textCharacters;

        private void OnEnable()
        {
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            // Set the statistics values in the statistics canvas
            m_textDistance.text = "LONGEST DISTANCE: " + IPHDataStorage.playerData.longestDistance;
            m_textStreak.text = "LONGEST STREAK: " + IPHDataStorage.playerData.longestStreak;
            m_textTokens.text = "TOTAL FEATHERS: " + IPHDataStorage.playerData.tokens;
            m_textPowerups.text = "TOTAL POWERUPS: " + IPHDataStorage.playerData.totalPowerups;
            m_textPowerupStreak.text = "LONGEST POWERUP: " + IPHDataStorage.playerData.longestPowerUpStreak;
            m_textCharacters.text = "CHARACTERS UNLOCKED: " + (int)(IPHDataStorage.playerData.tokens / 5f);
        }

        public void ResetData()
        {
            IPHDataStorage.instance.ResetData();
            UpdateStatistics();
        }
    }
}
