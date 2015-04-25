using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;
using WorldsApart.Clickables;

namespace WorldsApart.GUI
{
    public class PlayButton : MonoBehaviour
    {

        private RectTransform _button;

        public void LoadLevel()
        {
			gameObject.transform.DOScale(new Vector3(0,0,0),.5f);
			gameObject.transform.parent.transform.FindChild("Header").transform.DOScale(new Vector3(0,0,0),.5f);
			Clickable.enabledAll = false;
			Fader.Instance
				.SetTitle("Day: 1")
					.SetSubTitle("Weather: " + ThirdWorldManager.Instance.CurrentWeather)
					.FadeOutOnComplete(()=> 
					{
						Application.LoadLevel("WorldsApart");
					})
					.FadeInOnComplete(()=>Clickable.enabledAll = true)
					.FadeOutIn();
        }

		#if UNITY_EDITOR
		public void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				Clickable.enabledAll = false;
				Fader.Instance
					.SetTitle("Day: 1")
						.SetSubTitle("Weather: " + ThirdWorldManager.Instance.CurrentWeather)
						.FadeOutOnComplete(()=> 
						                   {
							Application.LoadLevel("WorldsApart");
						})
						.FadeInOnComplete(()=>Clickable.enabledAll = true)
						.FadeOutIn();
			}
		}
		#endif
    }
}