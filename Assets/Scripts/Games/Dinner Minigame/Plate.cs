using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Games.DinnerMinigame
{
    public class Plate : MonoBehaviour
    {
        private const int filledServings = 5;
        private int Servings;
        private MeshRenderer renderer;

        void Start()
        {
            Servings = filledServings;
            renderer = GetComponent<MeshRenderer>();
            renderer.material.color = Color.red;
        }

        public bool Eat()
        {
            if (Servings == 0) return false;
            else
            {
                renderer.material.color -= Color.red / filledServings;
                Servings--;
                return true; 
            }
        }

        void OnMouseUpAsButton()
        {
            Servings = filledServings;
            renderer.material.color = Color.red;
        }
    }
}
