using UnityEngine;
using System.Collections;

namespace WorldsApart.Cameras
{

    public class DragCamera : MonoBehaviour
    {
        public bool phoneCamera = false;

        public float dragSpeed = 1;
        private Vector3 dragOrigin;

        public bool cameraDragging = true;

        public float minX = 0;
        public float maxX = 2;
        public float minY = 0;
        public float maxY = 2;

         void Update()
          {
              if (phoneCamera)
              {
                  if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                  {
                      Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                      //transform.Translate(-touchDeltaPosition.x * dragSpeed, -touchDeltaPosition.y * dragSpeed, 0);
                      if (touchDeltaPosition.x < 0)
                      {
                          if (touchDeltaPosition.x * dragSpeed + ParallaxLayer.xOffset >= minX)
                              ParallaxLayer.xOffset += touchDeltaPosition.x * dragSpeed;

                      }
                      else
                      {
                          if (touchDeltaPosition.x * dragSpeed + ParallaxLayer.xOffset < maxX)
                              ParallaxLayer.xOffset += touchDeltaPosition.x * dragSpeed;
                      }

                      if (-touchDeltaPosition.y < 0)
                      {
                          if (touchDeltaPosition.y * dragSpeed + ParallaxLayer.yOffset >= minY)
                              ParallaxLayer.yOffset += touchDeltaPosition.y * dragSpeed;

                      }
                      else
                      {
                          if (touchDeltaPosition.y * dragSpeed + ParallaxLayer.yOffset < maxY)
                              ParallaxLayer.yOffset += touchDeltaPosition.y * dragSpeed;
                      }
                  }
              }
              else
              {
                  if (Input.GetMouseButton(0))
                  {
                      float leftMovement = Input.GetAxisRaw("Mouse X");
                      float upMovement = Input.GetAxisRaw("Mouse Y");

                      if (leftMovement < 0)
                      {
                          if (ParallaxLayer.xOffset + leftMovement >= minX)
                              ParallaxLayer.xOffset += leftMovement;
                      }
                      else
                      {
                          if (ParallaxLayer.xOffset + leftMovement < maxX)
                              ParallaxLayer.xOffset += leftMovement;
                      }

                      if (upMovement < 0)
                      {
                          if (ParallaxLayer.yOffset + upMovement >= minY)
                              ParallaxLayer.yOffset += upMovement;
                      }
                      else
                      {
                          if (ParallaxLayer.yOffset + upMovement < maxY)
                              ParallaxLayer.yOffset += upMovement;
                      }
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
                    if (ParallaxLayer.xOffset + leftMovement >= minX)
                        ParallaxLayer.xOffset += leftMovement;
                }
                else
                {
                    if (ParallaxLayer.xOffset + leftMovement < maxX)
                        ParallaxLayer.xOffset += leftMovement;
                }

                if (upMovement < 0)
                {
                    if (ParallaxLayer.yOffset + upMovement >= minY)
                        ParallaxLayer.yOffset += upMovement;
                }
                else
                {
                    if (ParallaxLayer.yOffset + upMovement < maxY)
                        ParallaxLayer.yOffset += upMovement;
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
                    if (ParallaxLayer.xOffset < maxX)
                    {
                        ParallaxLayer.xOffset += dragSpeed;//transform.Translate(move, Space.World);
                    }
                }
                else
                {
                    if (ParallaxLayer.xOffset > minX)
                    {
                        ParallaxLayer.xOffset -= dragSpeed; 
                        //transform.Translate(move, Space.World);
                    }
                }

                if (pos.y > 0f)
                {
                    if (ParallaxLayer.yOffset < maxY)
                    {
                        ParallaxLayer.yOffset += dragSpeed;//transform.Translate(move, Space.World);
                    }
                }
                else
                {
                    if (ParallaxLayer.yOffset > minY)
                    {
                        ParallaxLayer.yOffset -=  dragSpeed;
                        //transform.Translate(move, Space.World);
                    }
                }
            }
        
        }

        */
    }
}