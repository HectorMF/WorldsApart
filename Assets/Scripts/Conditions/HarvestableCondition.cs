using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Games;
using WorldsApart.Games.CropsMinigame;

namespace WorldsApart.Conditions
{
    public class HarvestableCondition: Condition
    {
        public Crop crop;

        public override bool isSatisfied()
        {
            return crop.currentPhase == CropPhaseEnum.Harvestable;
        }
    }
}
