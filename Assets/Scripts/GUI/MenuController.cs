using UnityEngine;
using System.Collections;
using System.Linq;
using WorldsApart.Utility;
using UnityStandardAssets.ImageEffects;
using DG.Tweening;

public class MenuController : MonoBehaviour {
    public Grayscale cameraGreyscale;
    public RectTransform button;
	public RectTransform header;
    public float delay = 1f;
    public float duration = 3f;
    public GameObject Scene;

    private bool buttonReady;
    private float step;
    private Vector3 origScale;

	// Use this for initialization
	void Start () {
        Wander.pause = true;
        cameraGreyscale.effectAmount = 1f;
        step = 1f / (duration / Time.deltaTime);
        origScale = button.localScale;
        button.localScale = new Vector3(0f, 0f, 0f);
		header.localScale = Vector3.zero;
        buttonReady = false;
        if (Scene != null)
        {
            foreach (var a in Scene.GetComponentsInChildren<Animator>()) a.enabled = false;
            foreach (var a in Scene.GetComponentsInChildren<ScrollTexture>()) a.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        while (delay > 0f)
        {
            delay -= Time.deltaTime;
            return;
        }
        if (cameraGreyscale.effectAmount == 0) return;
        cameraGreyscale.effectAmount -= step;
        if (cameraGreyscale.effectAmount < .2 && !buttonReady)
        {
          	button.DOScale(origScale, 1f);
			header.DOScale(Vector3.one, 1f);
			//button.DOPunchScale(new Vector3(0,0,0),2f).SetDelay(2f);//.DOShakeScale(2f, 1f,10).SetDelay(.506f);
            Wander.pause = false;
            cameraGreyscale.effectAmount = 0f;
            if (Scene != null)
            {
                foreach (var a in Scene.GetComponentsInChildren<Animator>()) a.enabled = true;
                foreach (var a in Scene.GetComponentsInChildren<ScrollTexture>()) a.enabled = true;
            }
        }
	}
}
