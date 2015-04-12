using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;

public enum Weather { None, Dry, Rain, EarthQuake};

public class WeatherManager : MonoBehaviour
{
    public static Weather weather = Weather.None;

    public Camera camera;
    public GameObject rain;
    public GameObject dry;
	public MonoBehaviour bloom;
	public MonoBehaviour grayscale;

    private Weather oldWeather;

    void Start()
    {
        UpdateWeather();
    }

    void Update()
    {
        if (oldWeather == weather) return;
        oldWeather = weather;
        UpdateWeather();
    }

    private void TurnOffWeather()
    {
		bloom.enabled = false;
		grayscale.enabled = false;
        dry.SetActive(false);
        rain.SetActive(false);
    }

    private void UpdateWeather()
    {
        TurnOffWeather();

        if (weather == Weather.Dry){
			bloom.enabled = true;
			grayscale.enabled = true;
            dry.SetActive(true);
			((Grayscale)grayscale).effectAmount = -.3f;
		}
        if (weather == Weather.Rain){
			grayscale.enabled = true;
            rain.SetActive(true);
			((Grayscale)grayscale).effectAmount = .3f;
		}
        if (weather == Weather.EarthQuake){
            camera.DOShakePosition(10, .4f, 5);
			#if UNITY_IPHONE || UNITY_ANDROID
			Handheld.Vibrate();
			#endif
		}
    }
}