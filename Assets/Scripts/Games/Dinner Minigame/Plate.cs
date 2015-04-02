using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WorldsApart.Games.DinnerMinigame
{
    public class Plate : MonoBehaviour
    {
        public DinnerGameManager manager;

        private const int filledServings = 4;
        private int servings;
        private SpriteRenderer renderer;
        private List<Sprite> sprites;

        void Start()
        {
            servings = filledServings;
            renderer = GetComponent<SpriteRenderer>();
            sprites = manager.GetPlateSprites();
            renderer.sprite = sprites[servings];
        }

        public bool Eat()
        {
            if (servings == 0) return false;
            else
            {
                servings--;
                renderer.sprite = sprites[servings];
                return true; 
            }
        }

        public void AssignSprites(List<Sprite> sprites)
        {
            this.sprites = sprites;
        }

        void OnMouseUpAsButton()
        {
            servings = filledServings;
            renderer.sprite = sprites[servings];
        }
    }
}
