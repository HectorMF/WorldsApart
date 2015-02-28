using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Games;

namespace Assets.Scripts.Conditions
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
