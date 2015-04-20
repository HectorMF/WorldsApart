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
			gameObject.SetActive(false);
			Fader.Instance.SetTitle("1 in 9 people do not have access \nto an improved water source.")
				.FadeOutOnComplete(()=>Application.LoadLevel("WorldsApart"))
				.SetTitleSize(30)
				.FadeOutIn();
        }
    }
}