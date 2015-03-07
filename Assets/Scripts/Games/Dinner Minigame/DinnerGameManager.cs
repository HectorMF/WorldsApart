using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Games.DinnerMinigame
{
    public class DinnerGameManager : MonoBehaviour
    {
        public List<Sprite> moods;

        public Sprite getMoodSprite(int mood)
        {
            return moods[mood];
        }
    }
}
