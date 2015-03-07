using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaterGameLogic {

    public float water = 0f;
    public float maxWater = 0f;
    public float currentDistance = 0f;
    public float distance = 0f;

    public bool GameOver = false;
    public bool ReachedDestination = false;
	// Use this for initialization
    private static WaterGameLogic instance;
    private WaterGameLogic()
    {
        water = 0f;
        maxWater = 1f;
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
    }
	
}
