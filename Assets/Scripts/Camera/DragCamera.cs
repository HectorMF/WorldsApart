using UnityEngine;
using System.Collections;

namespace WorldsApart.Cameras
{

    public class DragCamera : MonoBehaviour
    {

        public float dragSpeed = 1;
        private Vector3 dragOrigin;

        public bool cameraDragging = true;

        public float minX = 0;
        public float maxX = 2;
        public float minY = 0;
        public float maxY = 2;
        
         void Update()
          {
              if (!cameraDragging) return;
             
                  if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                  {
                      Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                      //transform.Translate(-touchDeltaPosition.x * dragSpeed, -touchDeltaPosition.y * dragSpeed, 0);
                      if (touchDeltaPosition.x < 0)
                      {
                          if (touchDeltaPosition.x * dragSpeed + ParallaxObject.xOffset >= minX)
                              ParallaxObject.xOffset += touchDeltaPosition.x * dragSpeed;

                      }
                      else
                      {
                          if (touchDeltaPosition.x * dragSpeed + ParallaxObject.xOffset < maxX)
                              ParallaxObject.xOffset += touchDeltaPosition.x * dragSpeed;
                      }

                      if (touchDeltaPosition.y < 0)
                      {
                          if (touchDeltaPosition.y * dragSpeed + ParallaxObject.yOffset >= minY)
                              ParallaxObject.yOffset += touchDeltaPosition.y * dragSpeed;

                      }
                      else
                      {
                          if (touchDeltaPosition.y * dragSpeed + ParallaxObject.yOffset < maxY)
                              ParallaxObject.yOffset += touchDeltaPosition.y * dragSpeed;
                      }
                  }
 
                  if (Input.GetMouseButton(0))
                  {
                      float leftMovement = Input.GetAxisRaw("Mouse X");
                      float upMovement = Input.GetAxisRaw("Mouse Y");

                      if (leftMovement < 0)
                      {
                          if (ParallaxObject.xOffset + leftMovement >= minX)
                              ParallaxObject.xOffset += leftMovement;
                      }
                      else
                      {
                          if (ParallaxObject.xOffset + leftMovement < maxX)
                              ParallaxObject.xOffset += leftMovement;
                      }

                      if (upMovement < 0)
                      {
                          if (ParallaxObject.yOffset + upMovement >= minY)
                              ParallaxObject.yOffset += upMovement;
                      }
                      else
                      {
                          if (ParallaxObject.yOffset + upMovement < maxY)
                              ParallaxObject.yOffset += upMovement;
                      
                  }
              }
          }
        /*
        void Update()
        {

            if (Input.GetMouseButton(0))
            {
                float leftMovement = Input.GetAxisRaw("Mouse X");
                float upMovement = Input.GetAxisRaw("Mouse Y");

                if (leftMovement < 0)
                {
                    if (ParallaxObject.xOffset + leftMovement >= minX)
                        ParallaxObject.xOffset += leftMovement;
                }
                else
                {
                    if (ParallaxObject.xOffset + leftMovement < maxX)
                        ParallaxObject.xOffset += leftMovement;
                }

                if (upMovement < 0)
                {
                    if (ParallaxObject.yOffset + upMovement >= minY)
                        ParallaxObject.yOffset += upMovement;
                }
                else
                {
                    if (ParallaxObject.yOffset + upMovement < maxY)
                        ParallaxObject.yOffset += upMovement;
                }
            }
        }*/
        /*
        void Update()
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            float left = Screen.width * 0.2f;
            float right = Screen.width - (Screen.width * 0.2f);
            float up = Screen.height * .2f;
            float down = Screen.height - (Screen.height * .2f);

            if (mousePosition.x < left)
            {
                cameraDragging = true;
            }
            else if (mousePosition.x > right)
            {
                cameraDragging = true;
            }

            if (mousePosition.y < up)
            {
                cameraDragging = true;
            }
            else if (mousePosition.y > down)
            {
                cameraDragging = true;
            }

            if (cameraDragging)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    dragOrigin = Input.mousePosition;
                    lastPosition = Vector3.zero;
                    return;
                }

                if (!Input.GetMouseButton(0)) return;

                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

                if (pos.x > 0f)
                {
                    if (ParallaxObject.xOffset < maxX)
                    {
                        ParallaxObject.xOffset += dragSpeed;//transform.Translate(move, Space.World);
                    }
                }
                else
                {
                    if (ParallaxObject.xOffset > minX)
                    {
                        ParallaxObject.xOffset -= dragSpeed; 
                        //transform.Translate(move, Space.World);
                    }
                }

                if (pos.y > 0f)
                {
                    if (ParallaxObject.yOffset < maxY)
                    {
                        ParallaxObject.yOffset += dragSpeed;//transform.Translate(move, Space.World);
                    }
                }
                else
                {
                    if (ParallaxObject.yOffset > minY)
                    {
                        ParallaxObject.yOffset -=  dragSpeed;
                        //transform.Translate(move, Space.World);
                    }
                }
            }
        
        }

        */
    }
}