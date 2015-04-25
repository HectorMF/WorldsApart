using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
public class WaypointTween : MonoBehaviour {
	public int index;
	public List<Vector3> positions;
	public List<float> windowWidth;
	public List<float> tweenDuration;

	public Ease easingFunction = Ease.OutElastic;

	public CameraFit cameraFit;

	private bool doneTweening;
	// Use this for initialization
	void Start () {
		doneTweening = true;
		cameraFit = GetComponent<CameraFit>();
		transform.position = positions[0];
		cameraFit.UnitsForWidth = windowWidth[0];
	}
	
	// Update is called once per frame
	void Update () {
		if(doneTweening)
		{
			if(index < positions.Count)
			{
				transform.DOMove(positions[index],tweenDuration[index]).SetEase(easingFunction).OnComplete(()=> doneTweening = true);
				DOTween.To(()=> cameraFit.UnitsForWidth, x=> cameraFit.UnitsForWidth = x, windowWidth[index], tweenDuration[index]);
				index++;
				doneTweening = false;
			}
		}

	}
}
