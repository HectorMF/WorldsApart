using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour {

	void OnEnable()
	{
		ThirdWorldManager.OnNewWeather += UpdateWeather;
	}
	
	void OnDisable()
	{
		ThirdWorldManager.OnNewWeather -= UpdateWeather;
	}

	void UpdateWeather(ThirdWorldManager.Weather newWeather)
	{
		if(newWeather == ThirdWorldManager.Weather.Rainy)
		{
			transform.Find("Pixel Rain").gameObject.SetActive(true);
		}
		else
		{
			transform.Find("Pixel Rain").gameObject.SetActive(false);
		}
	}
}
