using DG.Tweening;
using GoofyGhost.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace GoofyGhost.WorldsApart
{
    public class CowNipple : MonoBehaviour, IDraggable
    {
		public GameObject milk;
		public Text text;
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
		private BoxCollider collider;
		private bool canMilk = true;

        public void Start()
        {
            GameObject scoreObject = GameObject.Find("ScoreController");
            if(scoreObject != null)
			    scoreController = scoreObject.GetComponent<ScoreController>();
            DragHandler.Subscribe(this);
            currentFillValue = UnityEngine.Random.Range(minFillValue, maxFillValue);
            CanDrag = true;
			this.collider = this.GetComponent<BoxCollider>();
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
			canMilk = true;
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

            if (yPosition == yOffsetMax + position.y)
            {
                DragHandler.StopDragging();
            }
            if (canMilk && yPosition == yOffsetMin + position.y)
            {
				canMilk = false;
				ReportMilk();
				Instantiate(milk, this.transform.position + new Vector3(0,-this.collider.size.y/2,4), Quaternion.identity);
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
			text.text = "+" + currentFillValue;
			text.transform.DOShakePosition(1,16);
			text.DOFade(0,.5f).SetDelay(.5f).OnComplete(clearText);
	
            if(scoreController != null)
			    scoreController.Food += currentFillValue;
		}

		private void clearText()
		{
			text.text = "";
			text.DOFade(1,0);
		}


    }
}
