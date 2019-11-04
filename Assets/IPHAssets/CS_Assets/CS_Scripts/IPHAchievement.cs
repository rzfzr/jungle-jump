using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InfiniteHopper.Types;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper {
    public class IPHAchievement : MonoBehaviour {

        [SerializeField]
        public Text text;
        [SerializeField]
        private Image icon;
        public string name;
        public bool isAchieved;
        public IPHAchievement (string name, bool isAchieved) {
            this.name = name;
            this.isAchieved = isAchieved;
        }
        void Start () {
            text.text = name;
            //if unlocked then brighten the achievement icon
            if (isAchieved) icon.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
            else icon.GetComponent<Image> ().color = new Color32 (0, 0, 0, 255);

        }

    }
}