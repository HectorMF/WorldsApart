using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using System;
public class WaypointTween : MonoBehaviour {
	public int index;
	public List<Vector3> positions;
	public List<float> windowWidth;
	public List<float> tweenDuration;

	public Ease easingFunction = Ease.OutElastic;

    public WaypointTweeningMode mode = WaypointTweeningMode.Auto;

	public CameraFit cameraFit;
    public Button button;

	private bool doneTweening;
    private bool buttonPressed;
    private List<Action> actions;
	// Use this for initialization
	void Start () {
		doneTweening = true;
		cameraFit = GetComponent<CameraFit>();
		transform.position = positions[0];
		cameraFit.UnitsForWidth = windowWidth[0];
        buttonPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(doneTweening && (mode == WaypointTweeningMode.Auto || buttonPressed))
		{
            if (index < positions.Count)
            {
                transform.DOMove(positions[index], tweenDuration[index]).SetEase(easingFunction).OnComplete(() => doneTweening = true);
                DOTween.To(() => cameraFit.UnitsForWidth, x => cameraFit.UnitsForWidth = x, windowWidth[index], tweenDuration[index]);
                index++;
                doneTweening = false;
                if (index != positions.Count) buttonPressed = false;
            }
            else
            {
                if(actions != null) actions.ForEach(a => a());
                buttonPressed = false;
            }
		}
	}

    public void QueueReady()
    {
        buttonPressed = true;
    }

    public enum WaypointTweeningMode
    {
        Auto,
        Button
    }

    public void SubscribeToFinished(Action action)
    {
        if (actions == null) actions = new List<Action>();
        actions.Add(action);
    }
}
