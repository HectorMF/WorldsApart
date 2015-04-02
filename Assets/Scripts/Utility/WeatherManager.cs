using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public enum Weather { None, Dry, Rain, EarthQuake};

public class WeatherManager : MonoBehaviour
{
    public static Weather weather = Weather.None;

    public Weather initialWeather;
    public Camera camera;
    public GameObject rain;
    public GameObject dry;
	public MonoBehaviour bloom;
	public MonoBehaviour grayscale;

    private Weather oldWeather;

    void Start()
    {
        weather = initialWeather;
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
            dry.SetActive(true);
		}
        if (weather == Weather.Rain){
			grayscale.enabled = true;
            rain.SetActive(true);
		}
        if (weather == Weather.EarthQuake){
            camera.DOShakePosition(10, .4f, 5);
			#if UNITY_IPHONE || UNITY_ANDROID
			Handheld.Vibrate();
			#endif
		}
    }
}