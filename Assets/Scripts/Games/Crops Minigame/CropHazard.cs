using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorldsApart.Handlers;

namespace WorldsApart.Games.CropsMinigame
{
    public class CropHazard : MonoBehaviour
    {
        public float range;

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Crop")
            {
                var handler = new DeactivateHandler();
                handler.gameObject = col.gameObject;
                handler.Invoke();
            }
        }
    }
}
