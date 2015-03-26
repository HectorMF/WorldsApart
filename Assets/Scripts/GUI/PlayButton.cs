using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WorldsApart.GUI
{
    public class PlayButton : MonoBehaviour
    {

        private RectTransform _button;

        public void LoadLevel()
        {
            Application.LoadLevel("WorldsApart");
        }

    }
}