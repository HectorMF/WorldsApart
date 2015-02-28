using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vexe.Runtime.Types;

namespace WorldsApart.Games
{
    public class Crop : BetterBehaviour
    {
        public List<CropPhase> phases;



        
    }

    [Serializable]
    public class CropPhase
    {
        public Sprite sprite;
        public float waitTime;
        public CropPhaseEnum phase;
    }

    [Serializable]
    public enum CropPhaseEnum
    {
        Budding,
        Harvestable,
        Decaying,
        Weed
    }
}
