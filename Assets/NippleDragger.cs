using DG.Tweening;
using GoofyGhost.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GoofyGhost.WorldsApart
{
    public class NippleDragger : MonoBehaviour, IDraggable
    {
        public bool canDrag = true;
        public Ease easingFunction = Ease.OutElastic;
        public float tweenDuration = 1;

        public float yOffsetMin;
        public float yOffsetMax;

        private Vector3 position;
        private Vector3 screenPoint;
        private Vector3 offset;

        public void Start()
        {
            DragHandler.Subscribe(this);
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
            if (yPosition == yOffsetMax + position.y)
            {
                DragHandler.StopDragging();
            }
            //if (yPosition == yOffsetMin + position.y)
            //    DragHandler.StopDragging();
            
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
    }
}
