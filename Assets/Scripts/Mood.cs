using UnityEngine;
using System.Collections;

public class Mood {

	public enum MoodNames
	{
		Depressed = 5,
		Sad,
		Neutral,
		Happy,
		Ecstatic,
	}

	public MoodNames CurrentMood { get; set; }

	public Mood()
	{
		CurrentMood = MoodNames.Neutral;
	}

	public void IncrementMood()
	{
		switch(CurrentMood)
		{
		case MoodNames.Depressed:
			CurrentMood = MoodNames.Sad;
			break;
		case MoodNames.Sad:
			CurrentMood = MoodNames.Neutral;
			break;
		case MoodNames.Neutral:
			CurrentMood = MoodNames.Happy;
			break;
		case MoodNames.Happy:
			CurrentMood = MoodNames.Ecstatic;
			break;
		case MoodNames.Ecstatic:
			CurrentMood = MoodNames.Ecstatic;
			break;
		}
	}
	
	public void DecrementMood()
	{
		switch(CurrentMood)
		{
		case MoodNames.Depressed:
			CurrentMood = MoodNames.Depressed;
			break;
		case MoodNames.Sad:
			CurrentMood = MoodNames.Depressed;
			break;
		case MoodNames.Neutral:
			CurrentMood = MoodNames.Sad;
			break;
		case MoodNames.Happy:
			CurrentMood = MoodNames.Neutral;
			break;
		case MoodNames.Ecstatic:
			CurrentMood = MoodNames.Happy;
			break;
		}
	}
}