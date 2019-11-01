using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteHopper.Types
{
    [Serializable]
    public class PlayerVisual
    {
        public Sprite sprite;
        public Color32 color = new Color32(255, 255, 255, 255);
        public bool disable;
    }
}
