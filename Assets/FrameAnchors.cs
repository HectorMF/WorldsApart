using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class FrameAnchors : MonoBehaviour {
    public Ease easingFunction = Ease.InBack;
    public float duration;
    public int index;
    private bool moving;
    //public Ease easingFunction = Ease.OutElastic;
    public List<Vector3> anchors;

    private Camera camera;
	void Start () {
        camera = GetComponent<Camera>();
        camera.transform.position = anchors[index];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
            SetIndex((++index)%anchors.Count);
	}

    public void SetIndex(int i)
    {
        index = i;
        camera.transform.DOMove(anchors[i], duration).SetEase(easingFunction);
    }
}
