using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class FrameAnchors : MonoBehaviour {
    public Ease easingFunction = Ease.InBack;
    public float duration;
    public int index;
    private bool locked;
    //public Ease easingFunction = Ease.OutElastic;
    public List<Vector3> anchors;

    private Camera camera;
	void Start () {
        camera = GetComponent<Camera>();
        camera.transform.position = anchors[index];
	}
	
	// Update is called once per frame
	void Update () {
		if(locked) return;
        if (SwipeManager.swipeDirection == Swipe.Left)
            if(index + 1 < anchors.Count)
                SetIndex(++index);
        if (SwipeManager.swipeDirection == Swipe.Right)
            if (index - 1 >= 0)
                SetIndex(--index);
            //else
              //  camera.transform.DOShakePosition(1,new Vector3(1,0,0),3,0);
	}

    public void SetIndex(int i)
    {
        BannerAd.banner.Hide();
        index = i;
		locked = true;
        camera.transform.DOMove(anchors[i], duration).SetEase(easingFunction).OnComplete(() => { Unlock(); BannerAd.banner.Show(); });
    }

	private void Unlock()
	{
		locked= false;
	}
}
