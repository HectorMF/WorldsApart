using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace WorldsApart.GUI
{
    public class PlayButton : MonoBehaviour
    {

        private RectTransform _button;

        public void LoadLevel()
        {
			Fader.FadeToBlack(Fader.Gesture.None,0, 4,"1 in 9 people do not have access \nto an improved water source.","",() =>
				Application.LoadLevel("WorldsApart"),30
			);

        }
    }
}