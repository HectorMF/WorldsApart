using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class WaterGameLogic {

    public float water;
    private float _maxWater;
    public float maxWater
    {
        get
        {
            return _maxWater;
        }
    }
    public float currentDistance = 0f;
    public float distance = 0f;

    private Quaternion targetRotation;

    public bool GameOver = false;
    public bool ReachedDestination = false;
    private float step;
    public float Step {
        get{
            return step;
        }
    }
    
	// Use this for initialization
    private static WaterGameLogic instance;
    private WaterGameLogic()
    {
       
       
        currentDistance = 0f;
        _maxWater = ThirdWorldManager.Instance.WaterCapacity;
        water = _maxWater;
        distance = 10f;
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
        if(maxWater< water)
        {
            water = maxWater;
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
            Debug.Log("LOST THE GAME!!!");
            GameOver = true;
			Fader.FadeOutIn(Fader.Gesture.None, 0, 2, "You lost all of your water", "Good luck next time!",()=>Application.LoadLevel("WorldsApart"));
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
            WaterGameResources.Instance.DistanceText.text = string.Format("{0:0.0} / {1} meter", currentDistance, distance);

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
        Fader.FadeOutIn(Fader.Gesture.None, 0, 2, 
            string.Format(@"You reached the village with 
{0: 0.0} liters of water 
out of {1: 0.0} capacity", water, maxWater),"", ()=>Application.LoadLevel("WorldsApart"),24);
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
    }
	
}
