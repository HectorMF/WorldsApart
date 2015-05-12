using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaterGameResources: MonoBehaviour {

    public float travelDistance;
    public float bucketSize;
    public float currentWater;
    public float trippingChance;
	public Text DistanceText;

    public float TravelDistanceValue
    {
        get
        {
            return WaterGameLogic.Instance.currentDistance;
        }
    }

    public float BucketSizeValue
    {
        get
        {
            return WaterGameLogic.Instance.BucketSizeValue;
        }
    }
    
    public float CurrentWater
    {
        get
        {
            return WaterGameLogic.Instance.water;
        }
    }
    public float TrippingChance
    {
        get
        {
            return WaterGameLogic.Instance.TrippingChance;
        }
        set
        {
            WaterGameLogic.Instance.TrippingChance = value;
        }
    }

	public void Start(){
		//Fader.FadeToClear(Fader.Gesture.Balance,0, 2, "Carry Your Water", "Don't Spill");
        travelDistance= TravelDistanceValue;
        bucketSize = BucketSizeValue;
        currentWater = CurrentWater;
        trippingChance = TrippingChance;
        //giving the text boxes to the WaterGameLogic
        WaterGameLogic.Instance.GiveMeTheTextBox(DistanceText);
	}
}
