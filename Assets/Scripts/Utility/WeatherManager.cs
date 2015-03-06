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
        dry.SetActive(false);
        rain.SetActive(false);
    }

    private void UpdateWeather()
    {
        TurnOffWeather();

        if (weather == Weather.Dry)
            dry.SetActive(true);
        if (weather == Weather.Rain)
            rain.SetActive(true);
        if (weather == Weather.EarthQuake){
            camera.DOShakePosition(10, .4f, 5);
			Handheld.Vibrate();
		}
    }
}