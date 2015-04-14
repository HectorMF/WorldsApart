using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThirstInfo {

	public string survivesFor, waterRequiredPerMember, memberCount, daysWithoutWater;
	public Dictionary<string, int> requirements;

	public ThirstInfo () 
	{
		survivesFor = "SurvivesFor";
		waterRequiredPerMember = "WaterRequiredPerMember";
		memberCount = "MemberCount";
		daysWithoutWater = "DaysWithoutWater";
	}

	public ThirstInfo Chicken()
	{
		requirements = WaterRequirements(1, 1, 1);
		return this;
	}

	public ThirstInfo Goat()
	{
		requirements = WaterRequirements(2, 2, 1);
		return this;
	}

	public ThirstInfo Cow()
	{
		requirements = WaterRequirements (3, 3, 1);
		return this;
	}

	public ThirstInfo Crops()
	{
		requirements = WaterRequirements(1, 1, 10);
		return this;
	}

	public ThirstInfo Family()
	{
		requirements = WaterRequirements(3, 3, 4);
		return this;
	}

	public ThirstInfo FirePit()
	{
		requirements = WaterRequirements (99999, 2, 1);
		return this;
	}

	private Dictionary<string, int> WaterRequirements(int survives, int waterRequired, int members)
	{
		Dictionary<string, int> d = new Dictionary<string, int>();
		d.Add (survivesFor, survives);
		d.Add (waterRequiredPerMember, waterRequired);
		d.Add (memberCount, members);
		d.Add (daysWithoutWater, 0);
		return d;
	}

}
