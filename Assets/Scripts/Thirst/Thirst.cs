using UnityEngine;
using System.Collections;

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
	
	public void DayEnd()
	{
		DaysWithoutWater += AmountDrank == TotalRequiredWater ? 0 : 1;
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
	}
	
	public void Drink()
	{
		AmountDrank += ThirdWorldManager.Instance.DecrementWater(TotalRequiredWater - AmountDrank);
		if (AmountDrank == TotalRequiredWater)
		{
			DrinkSuccess();
		}
		else
		{
			Debug.Log("your " + name + " still needs " + (TotalRequiredWater - AmountDrank) + "L of water");
		}
	}
	
	public virtual void DrinkSuccess()
	{
		
	}
}