using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace WorldsApart.GUI
{
    public class PlayButton : MonoBehaviour
    {

        private RectTransform _button;

        public void LoadLevel()
        {
			gameObject.transform.DOScale(new Vector3(0,0,0),.5f);
			gameObject.transform.parent.transform.FindChild("Header").transform.DOScale(new Vector3(0,0,0),.5f);
			Fader.Instance.SetTitle("1 in 9 people do not have access \nto an improved water source.")
				.FadeOutOnComplete(()=>Application.LoadLevel("WorldsApart"))
				.SetTitleSize(30)
				.FadeOutIn();
        }
    }
}