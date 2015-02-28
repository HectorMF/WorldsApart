using UnityEngine;
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
		if (AmountDrank >= TotalRequiredWater)
			DaysWithoutWater = 0;
		else
			DaysWithoutWater += 1;

		if(DaysWithoutWater >= SurvivesFor)
		{
			MemberCount -= 1;
			ThirdWorldManager.Instance.DecrementMood();
			Debug.Log("You've lost one " + name);
			if (MemberCount <= 0)
			{
				if(this is FamilyThirst) GameObject.Find ("MainChar").SetActive(false);
				gameObject.SetActive(false);
			}
		}
		AmountDrank = 0;
		transform.Find("ThirstyBubble").gameObject.SetActive(true);
	}
	
	public void Drink()
	{
		ThirdWorldManager.Instance.UsedAction();
		if(NoWaterRequirementOnRainyDay())
		{
			DrinkSuccess();
		}
		else 
		{
			DrinkOnNormalday();
		}
	}

	private void DrinkOnNormalday()
	{
		AmountDrank += ThirdWorldManager.Instance.DecrementWater(TotalRequiredWater - AmountDrank);
		if (AmountDrank >= TotalRequiredWater)
		{
			DrinkSuccess();
		}
		Debug.Log (string.Format("Name: {2}, Tot Req H20: {1}, Drank: {3}", MemberCount, TotalRequiredWater, name, AmountDrank));
	}
	private bool NoWaterRequirementOnRainyDay()
	{
		return ThirdWorldManager.Instance.IsRaining() && !(this is FamilyThirst);
	}

	public virtual void DrinkSuccess()
	{
		transform.Find("ThirstyBubble").gameObject.SetActive(false);
		Debug.Log(string.Format("Your {0} has enough water", name));
	}

	public void InitializeWaterRequirements(ThirstInfo info)
	{
		SurvivesFor = info.requirements[info.survivesFor];
		WaterRequiredPerMember = info.requirements[info.waterRequiredPerMember];
		MemberCount = info.requirements[info.memberCount];
		DaysWithoutWater = info.requirements[info.daysWithoutWater];
	}
}