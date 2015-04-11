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
        public Crop crop = new Crop();

        public override bool isSatisfied()
        {
            try
            {
                return crop.currentPhase == CropPhaseEnum.Harvestable;
            }
            catch(Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }
    }
}
