using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;

public class WeatherManager : MonoBehaviour
{
	public static ThirdWorldManager.Weather weather = ThirdWorldManager.Weather.Nice;

    public Camera camera;
    public GameObject rain;
    public GameObject dry;
	private MonoBehaviour bloom;
	private MonoBehaviour grayscale;

	private ThirdWorldManager.Weather oldWeather;

    void Start()
    {
        UpdateWeather();
    }

	void Awake()
	{
		bloom = camera.GetComponent<BloomOptimized> ();
		grayscale = camera.GetComponent<Grayscale> ();
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

		if (weather == ThirdWorldManager.Weather.Dry){
			bloom.enabled = true;
			grayscale.enabled = true;
            dry.SetActive(true);
			((Grayscale)grayscale).effectAmount = -.3f;
		}
		if (weather == ThirdWorldManager.Weather.Rainy){
			grayscale.enabled = true;
            rain.SetActive(true);
			((Grayscale)grayscale).effectAmount = .3f;
		}
        /*if (weather == Weather.EarthQuake){
            camera.DOShakePosition(10, .4f, 5);
			#if UNITY_IPHONE || UNITY_ANDROID
			Handheld.Vibrate();
			#endif
		}*/
    }
}