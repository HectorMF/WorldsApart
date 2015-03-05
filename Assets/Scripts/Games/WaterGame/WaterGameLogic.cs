using UnityEngine;
using System.Collections;

public class WaterGameLogic {

    public float water;
    public float maxWater;
    public float currentDistance;
    public float distance;

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
        water = water - amount;
        if (water <= 0)
        {
            LostTheGame();
        }
    }
    private void LostTheGame()
    {
        Debug.Log("LOST THE GAME!!!");
        GameOver = true;
    }

    /// <summary>
    /// Step size is how much the player is closer to his/her destination
    /// </summary>
    /// <param name="stepSize"></param>
    public void Walk(float stepSize)
    {
        currentDistance += stepSize;
        if(currentDistance>=distance)
        {
            WinTheGame();
        }
    }
    private void WinTheGame()
    {
        ReachedDestination = true;
    }
	
}
