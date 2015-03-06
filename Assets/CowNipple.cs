using DG.Tweening;
using GoofyGhost.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GoofyGhost.WorldsApart
{
    public class CowNipple : MonoBehaviour, IDraggable
    {
        public bool canDrag = true;
        public Ease easingFunction = Ease.OutElastic;
        public float tweenDuration = 1;

        public float yOffsetMin;
        public float yOffsetMax;

        public static float minFillValue = 0;
        public static float maxFillValue = 1;

        public float currentFillValue;
        public float fillRate = .1f;
        public Vector3 minScale;
        public Vector3 maxScale;

        private Vector3 position;
        private Vector3 screenPoint;
        private Vector3 offset;

		private ScoreController scoreController;

        public void Start()
        {
			scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
            DragHandler.Subscribe(this);
            currentFillValue = UnityEngine.Random.Range(minFillValue, maxFillValue);
            CanDrag = true;
        }

        public void BeginDrag()
        {
            
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            position = transform.position;
        }

        public void EndDrag()
        {
            transform.DOMove(position, tweenDuration).SetEase(easingFunction).OnComplete(SetCanDrag);

            CanDrag = false;
        }

        private void SetCanDrag()
        {
            this.CanDrag = true;
        }

        public void UpdateDrag()
        {
            //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, 0));
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            float yPosition = Clamp(curPosition.y, yOffsetMin + position.y, yOffsetMax + position.y);
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
            transform.localScale = Vector3.Lerp(minScale, maxScale, (yPosition - (yOffsetMin + position.y)) / ((position.y) - (yOffsetMin + position.y)));

            //Debug.og

            if (yPosition == yOffsetMax + position.y)
            {
                DragHandler.StopDragging();
            }
            if (yPosition == yOffsetMin + position.y)
            {
				ReportMilk();
                currentFillValue = 0;
            }

        }

        void Update()
        {
            if (currentFillValue == maxFillValue) return;

            currentFillValue += fillRate * Time.deltaTime;
            currentFillValue = Clamp(currentFillValue, minFillValue, maxFillValue);
            transform.localScale = Vector3.Lerp(minScale, maxScale, (currentFillValue - minFillValue) / (maxFillValue - minFillValue));
            if (currentFillValue == maxFillValue)
                transform.DOShakeScale(.5f, new Vector3(.15f, 0, 0), 16);
        }

        public float Clamp(float val, float min, float max)
        {
            if (val < min)
                return min;
            if (val > max)
                return max;
            return val;
        }

        public bool CanDrag
        {
            get
            {
                return canDrag;
            }
            set
            {
                canDrag = value;
            }
        }

		void ReportMilk() {
			scoreController.Food += currentFillValue;
			Debug.Log(scoreController.Food);
		}
    }
}
