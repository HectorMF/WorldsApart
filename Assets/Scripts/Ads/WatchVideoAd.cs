using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

/// <summary>
/// ADS!:
/// You can set up an account with https://unityads.unity3d.com and the rest is piece of pie! 
/// gameID = Is when you create a game on the web interface it gives you an ID. gameId is that!
/// zoneID = when you go to the settings you can have different type of ads, such as reward ads or just normal ads. ZoneID is the id of the type of the ads
/// </summary>
public class WatchVideoAd : MonoBehaviour {

#if UNITY_EDITOR
    string gameId = @"131627042";
#elif UNITY_ANDROID
    string gameId = @"131627042";
#elif UNITY_IOS
    string gameId = @"IOS GAME ID";
#else 
    string gameId = @"131627042";
#endif
    bool startingAd = false;
    // Use this for initialization
	void Start () {
        Advertisement.Initialize(gameId);
        
	}
	
	// Update is called once per frame
	void Update () {
        //Uncommenting the following shows a ad on opening up the application
        //if(!startingAd)
        //{
        //    startingAd = true;
        //    if(Advertisement.isReady("defaultVideoAndPictureZone"))
        //    {
        //        Advertisement.Show("defaultVideoAndPictureZone");
        //    }
        //}
	    if(Advertisement.isReady("rewardedVideoZone"))
        {
            Advertisement.Show("rewardedVideoZone");
            //TODO: Hook it up to the game system so it rewards the player.
        }
	}
}
