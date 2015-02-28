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
        public CropPhaseEnum currentPhase;
        private int index;
        private float timer;

        void Start()
        {
            index = 0;
            var startPhase = phases[index];
            gameObject.GetComponent<SpriteRenderer>().sprite = startPhase.sprite;
            currentPhase = startPhase.phase;
            timer = 0;
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (index != phases.Count - 1 && timer > phases[index].waitTime)
            {
                index++;
                var phase = phases[index];
                gameObject.GetComponent<SpriteRenderer>().sprite = phase.sprite;
                currentPhase = phase.phase;
                timer = 0;
            }
        }
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
