using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaterGameLogic {

    public float water;
    private float _maxWater;

    public float BucketSizeValue
    {
        get
        {
            return _maxWater;
        }
    }
    public float currentDistance = 0f;
    public float distance = 10f;
    public float TrippingChance = 40f;
    private Quaternion targetRotation;

    public bool GameOver = false;
    public bool ReachedDestination = false;
    private float step;
    public float Step {
        get{
            return step;
        }
    }

    private Text distanceText;
	// Use this for initialization
    private static WaterGameLogic instance;
    private WaterGameLogic()
    {
       
       
        currentDistance = 0f;
        _maxWater = ThirdWorldManager.Instance.WaterCapacity;
        water = 0;
		ReadyToPlay = false;

    }
    public static WaterGameLogic Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new WaterGameLogic();
            }
            return instance;
        }
    }

    public bool ReadyToPlay
    {
        get;
        set;
    }
    /// <summary>
    /// Equals to Auto Fail. The playe loses all of his/her water
    /// </summary>
    public void DropWaterPack()
    {
        water = 0f;

    }
    /// <summary>
    /// Player loses some water
    /// </summary>
    /// <param name="amount"></param>
    public void LoseSomeWater(float amount)
    {
        if(BucketSizeValue< water)
        {
            water = BucketSizeValue;
        }
        water = water - amount;
        if (water <= 0)
        {
            LostTheGame();
        }
    }
    private void LostTheGame()
    {
        if (!ReachedDestination)
        {
            GameOver = true;
			Fader.Instance
				.SetTitle("You have lost all of your water.")
				.FadeOutOnComplete(()=>
					{
                        GameOver = false;
                        ReachedDestination = false;
                        instance = null;
						Application.LoadLevel("WorldsApart");
					})
				.FadeInOnComplete(()=>ThirdWorldManager.Instance.UsedAction())
				.FadeOutIn();
        }
        GameIsFinished(false);
    }

    /// <summary>
    /// Step size is how much the player is closer to his/her destination
    /// </summary>
    /// <param name="stepSize"></param>
    public void Walk(float stepSize)
    {
        if (!ReachedDestination && !GameOver)
        {
            distanceText.text = string.Format("{0:0.0} / {1} meter", currentDistance, distance);

            currentDistance += stepSize;
            if (currentDistance >= distance)
            {
                WinTheGame();
            }
        }
    }
    private void WinTheGame()
    {
        ReachedDestination = true;
		Fader.Instance
			.SetTitle(string.Format(@"You reached the village with {0: 0.0} liters of water out of {1: 0.0} capacity", water, BucketSizeValue))
			.SetTitleSize(35)
			.SetSubTitle("Women in water-stressed regions walk on average 3.5 miles everyday to get water.")
			.FadeOutOnComplete(()=>
		 		{
                    GameOver = false;
                    ReachedDestination = false;
                    //HACK Resetting the instance!
                    instance = null;
					Application.LoadLevel("WorldsApart");
				})
			.FadeInOnComplete(()=>ThirdWorldManager.Instance.UsedAction())
			.FadeOutIn();

        GameIsFinished(true);
    }

    /// <summary>
    /// CameraMovement Get called to make the player to adjust the device to match the camera.
    /// </summary>
    /// <param name="percentage"></param>
    public void CameraRandomMove(int angel, GameObject camera)
    {
        if (step < 10)
        {
            if (angel != 0)
            {
                camera.GetComponent<Rigidbody2D>().AddTorque(angel);
            }
            step +=0.05f;
        }
        else if(step >= 1)
        {
            step = 0;
 
        }
    }
    private void GameIsFinished(bool won)
    {
        //Re-Enabling the screen rotation after the game is over
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

       
        if(won)
        {
            ThirdWorldManager.Instance.IncrementWater((int)water);
        }
        //Resetting Variables
        ReadyToPlay = false;
        water = 0f;
        currentDistance = 0f;
    }

    public void GiveMeTheTextBox(Text textBox)
    {
        distanceText = textBox;
    }

    public void IncrementWater(float waterAmount)
    {
        if(waterAmount+water<_maxWater)
        {
            water += waterAmount;
        }
    }
    public void DecrementWater(float waterAmount)
    {
        if (water - waterAmount > 0)
        {
            water -= waterAmount;
        }
        else
            water = 0;
    }
	
}
