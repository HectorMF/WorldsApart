using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class BannerAd : MonoBehaviour {

	// Use this for initialization
	void Start () {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-1850231283632979/2985243049";
        #elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-1850231283632979/2845642242";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        bannerView.AdLoaded += HandleAdLoaded;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        bannerView.Show();
	}

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received.");
        // Handle the ad loaded event.
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
