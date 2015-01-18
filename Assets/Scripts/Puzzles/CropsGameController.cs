using UnityEngine;
using System.Collections;
using WorldsApart;
using WorldsApart.Utility;

public class CropsGameController : MonoBehaviour {

	public GameObject cameraGO;
	public Transform CameraHook;
	Camera cameraComponent;
	Vector3 previousPosition;
	public GameObject miniGame;
	public float speed = 20.0f;
	float lerpTime;
	float cameraOriginalSize, gameCameraSize = 1.3f;

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
		cameraComponent = cameraGO.GetComponent<Camera>();
	}
	
	void Update () {
		switch(CurrentState)
		{
		case MiniGameState.Suspended:
			miniGame.GetComponent<ObjectGenerator>().enabled = false;
			break;
		case MiniGameState.Starting:
			miniGame.GetComponent<ObjectGenerator>().enabled = true;
			cameraGO.transform.position = Vector3.MoveTowards(cameraGO.transform.position, CameraHook.position, lerpTime += Time.deltaTime * speed);
			cameraComponent.orthographicSize = Mathf.Lerp(cameraComponent.orthographicSize, gameCameraSize, lerpTime);
			if(cameraGO.transform.position == CameraHook.transform.position) CurrentState = MiniGameState.Playing;
			break;
		case MiniGameState.Playing:
			if (CounterManager.Instance.GetCounter("GardenMiniGame").count >= scoreGoal)
			{
				CurrentState = MiniGameState.Ending;
				lerpTime = 0;
			}
			break;
		case MiniGameState.Ending:
			cameraGO.transform.position = Vector3.MoveTowards(cameraGO.transform.position, previousPosition, lerpTime += Time.deltaTime * speed);
			cameraComponent.orthographicSize = Mathf.Lerp(cameraComponent.orthographicSize, cameraOriginalSize, lerpTime);

			if(cameraGO.transform.position == previousPosition) {
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
			previousPosition = cameraGO.transform.position;
			CurrentState = MiniGameState.Starting;
			cameraOriginalSize = cameraComponent.orthographicSize;
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