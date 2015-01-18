﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using WorldsApart.Cameras;

namespace WorldsApart
{
    public class SceneTransition : MonoBehaviour
    {
        public Transform jungleHierarchy;
        public Transform cityHierarchy;
        public Transform jungleCameraAnchor;
        public Transform cityCameraAnchor;
        public TargetScene currentScene;
        public float fadeSpeed = 1.5f;
        public Image fadeImage;

        private bool transitionActive;
        private TransitionPhase phase;
        private DragCamera dragController;

        void Start()
        {
            fadeImage.enabled = true;
            phase = TransitionPhase.MovingCamera;
            transitionActive = true;
            dragController = gameObject.GetComponent<DragCamera>();
        }

        void Update()
        {
            if (transitionActive)
            {
                if (phase == TransitionPhase.FadingOut)
                    FadeOut(() => phase = TransitionPhase.MovingCamera);
                if (phase == TransitionPhase.MovingCamera)
                {
                    if (currentScene == TargetScene.City)
                    {
                        jungleHierarchy.gameObject.SetActive(false);
                        cityHierarchy.gameObject.SetActive(true);
                        transform.position = cityCameraAnchor.position;
                        if (dragController != null) dragController.maxY = 100;
                    }
                    else
                    {
                        cityHierarchy.gameObject.SetActive(false);
                        jungleHierarchy.gameObject.SetActive(true);
                        transform.position = transform.position = jungleCameraAnchor.position;
                        if (dragController != null) dragController.maxY = 0;
                    }
                    phase = TransitionPhase.FadingIn;
                }
                if (phase == TransitionPhase.FadingIn)
                    FadeIn(() => transitionActive = false);
            }
        }

        public void FadeIn(Action onFinished)
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Color.clear, fadeSpeed * Time.deltaTime);
            if (onFinished != null && fadeImage.color == Color.clear) onFinished();
        }

        public void FadeOut(Action onFinished)
        {
            fadeImage.color = Color.Lerp(fadeImage.color, Color.black, fadeSpeed * Time.deltaTime);
            if (onFinished != null && fadeImage.color == Color.black) onFinished();
        }

        public void Transition()
        {
            transitionActive = true;
            if (currentScene == TargetScene.City) currentScene = TargetScene.Jungle;
            else currentScene = TargetScene.City;
        }
    }

    public enum TargetScene
    {
        Jungle,
        City
    }

    public enum TransitionPhase
    {
        FadingOut,
        FadingIn,
        MovingCamera
    }
}