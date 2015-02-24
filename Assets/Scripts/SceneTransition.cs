using System;
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
        private GameObject water;
        private GameObject food;


        void Start()
        {
            fadeImage.enabled = true;
            phase = TransitionPhase.MovingCamera;
            transitionActive = true;
            dragController = gameObject.GetComponent<DragCamera>();
            
            water = GameObject.Find("Water");
            food = GameObject.Find("Food");
        }

        void OnEnable()
        {
            ThirdWorldManager.OnDayEnd += Transition;
        }
                    
        void OnDisable()
        {
            ThirdWorldManager.OnDayEnd -= Transition;
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
                        //ParallaxLayer.xOffset = 180;
                       // ParallaxLayer.yOffset = 15;
                        if(water != null)
                        water.SetActive(false);
                        if(food != null)
                        food.SetActive(false);
                    }
                    else
                    {
                        cityHierarchy.gameObject.SetActive(false);
                        jungleHierarchy.gameObject.SetActive(true);
                        transform.position = transform.position = jungleCameraAnchor.position;
                        if (dragController != null) dragController.maxY = 0;
                        //ParallaxLayer.xOffset = 400;
                    }
                    phase = TransitionPhase.FadingIn;
                }
                if (phase == TransitionPhase.FadingIn)
                    FadeIn(() => transitionActive = false);
            }
        }

        public void FadeIn(Action onFinished)
        {
            fadeImage.color = Color.clear;// Color.Lerp(fadeImage.color, Color.clear, fadeSpeed * Time.deltaTime);
            if (onFinished != null && fadeImage.color == Color.clear) onFinished();
        }

        public void FadeOut(Action onFinished)
        {
            fadeImage.color = Color.black;// Color.Lerp(fadeImage.color, Color.black, fadeSpeed * Time.deltaTime);
            
            if (onFinished != null && fadeImage.color == Color.black) onFinished();
        }

        public void Transition()
        {
            transitionActive = true;
            phase = TransitionPhase.FadingOut;
            if (currentScene == TargetScene.City)
            {
                
                currentScene = TargetScene.Jungle;
                //ParallaxLayer.xOffset = 400;
                water.SetActive(true);
                food.SetActive(true);
            }
            else {
                currentScene = TargetScene.City;
                water.SetActive(false);
                food.SetActive(false);
            }
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
