﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Thirst : MonoBehaviour {
	
	public int AmountDrank { get; set; }
	public int DaysWithoutWater { get; set; }
	public int SurvivesFor { get; protected set; }
	public int WaterRequiredPerMember { get; protected set;} 
	public int MemberCount { get; set; }
	public int TotalRequiredWater { get { return MemberCount * WaterRequiredPerMember; } set {} }

	void OnEnable()
	{
		ThirdWorldManager.OnDayEnd += DayEnd;
	}

	void OnDisable()
	{
		ThirdWorldManager.OnDayEnd -= DayEnd;
	}
	
	public virtual void DayEnd()
	{
		if (AmountDrank == TotalRequiredWater)
			DaysWithoutWater = 0;
		else
			DaysWithoutWater += 1;

		Debug.Log("Drank enough: " + (AmountDrank == TotalRequiredWater) + " " + DaysWithoutWater);

		if(DaysWithoutWater >= SurvivesFor)
		{
			MemberCount -= 1;
			ThirdWorldManager.Instance.DecrementMood();
			Debug.Log("You've lost one " + name);
			if (MemberCount <= 0)
			{
				gameObject.SetActive(false);
			}
		}
		AmountDrank = 0;
	}
	
	public void Drink()
	{
		AmountDrank += ThirdWorldManager.Instance.DecrementWater(TotalRequiredWater - AmountDrank);
		if (AmountDrank >= TotalRequiredWater)
		{
			DrinkSuccess();
		}
		else
		{
			Debug.Log("your " + name + " still needs " + (TotalRequiredWater - AmountDrank) + "L of water");
		}
		Debug.Log (string.Format("Name: {2}, Members: {0}, Tot Req H20: {1}, Drank: {3}", MemberCount, TotalRequiredWater, name, AmountDrank));
	}
	
	public virtual void DrinkSuccess()
	{
		
	}
}