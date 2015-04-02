using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaterGameLogic {

    public float water = 0f;
    public float maxWater = 0f;
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
        water = 0f;
        maxWater = WaterGameResources.Instance.BucketSizeValue;
        currentDistance = 0f;
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
            Fader.FadeToBlack(0, 1, "You lost all of your water", "Good luck next time!");
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
        Fader.FadeToBlack(0, 1, 
            string.Format(@"You reached the village with 
{0: 0.0} liters of water 
out of {1: 0.0} capacity", water, maxWater));
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
           // Debug.Log("Angel:" + angel);
            if (angel != 0)
            {
             //   targetRotation = Quaternion.Euler(new Vector3(0, 0, angel));
                camera.GetComponent<Rigidbody2D>().AddTorque(angel);
            }
            
            //camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, targetRotation, step);
           // Debug.Log("STEP:" + step);
            //amera.transform.rotation = targetRotation;
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

        //TODO: Logic to send the result to the core game mechanic!
    }
	
}
