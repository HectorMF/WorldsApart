﻿using UnityEngine;
using System.Collections;
using WorldsApart.Utility;
using UnityStandardAssets.ImageEffects;
using DG.Tweening;

public class MenuController : MonoBehaviour {
    public Grayscale cameraGreyscale;
    public RectTransform button;

    public float delay = 1f;
    public float duration = 3f;

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
        buttonReady = false;
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
            button.DOScale(origScale, .505f);
            button.DOShakeScale(2f, 11f, 6, 120f).SetDelay(.03f);
        }
        if (cameraGreyscale.effectAmount < 0f)
        {
            Wander.pause = false;
            cameraGreyscale.effectAmount = 0f;
        }
	}
}