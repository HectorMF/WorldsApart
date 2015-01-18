using UnityEngine;
using System.Collections;
using WorldsApart;
using WorldsApart.Utility;

public class CropsGameController : MonoBehaviour {

	public GameObject camera;
	public Transform CameraHook;
	Vector3 previousPosition;
	public GameObject miniGame;
	public float speed = 20.0f;
	float lerpTime;

	int scoreGoal;

	public enum MiniGameState {
		Suspended,
		Starting,
		Playing,
		Ending
	}

	public MiniGameState CurrentState;

	void Start () {
		CurrentState = MiniGameState.Suspended;
		CameraHook = transform.Find("CameraHook");
		miniGame = transform.Find ("MiniGame").gameObject;
		miniGame.SetActive(false);
	}
	
	void Update () {
		switch(CurrentState)
		{
		case MiniGameState.Suspended:
			miniGame.GetComponent<ObjectGenerator>().enabled = false;
			break;
		case MiniGameState.Starting:
			miniGame.GetComponent<ObjectGenerator>().enabled = true;
			camera.transform.position = Vector3.MoveTowards(camera.transform.position, CameraHook.position, lerpTime += Time.deltaTime * speed);
			if(camera.transform.position == CameraHook.transform.position) CurrentState = MiniGameState.Playing;
			break;
		case MiniGameState.Playing:
			if (CounterManager.Instance.GetCounter("GardenMiniGame").count >= scoreGoal)
			{
				CurrentState = MiniGameState.Ending;
				lerpTime = 0;
			}
			break;
		case MiniGameState.Ending:
			camera.transform.position = Vector3.MoveTowards(camera.transform.position, previousPosition, lerpTime += Time.deltaTime * speed);
			if(camera.transform.position == previousPosition) {
				ThirdWorldManager.Instance.Report();
				CurrentState = MiniGameState.Suspended;
			}
			break;
		}
	}

	public void StartGame()
	{
		if (CanAndShouldWater())
		{
			previousPosition = camera.transform.position;
			CurrentState = MiniGameState.Starting;
			lerpTime = 0;
			miniGame.SetActive(true);
			scoreGoal =  gameObject.GetComponent<Thirst>().Drink();
		}
	}
	
	bool CanAndShouldWater()
	{
		return ThirdWorldManager.Instance.AnyWater 
			&& gameObject.GetComponent<Thirst>().AmountDrank < gameObject.GetComponent<Thirst>().TotalRequiredWater
				&& ThirdWorldManager.Instance.TryAction();
	}
}