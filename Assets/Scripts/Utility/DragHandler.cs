using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GoofyGhost.Utility
{
    public class DragHandler : MonoBehaviour
    {
        private static DragHandler instance;

        static private DragHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = UnityEngine.Object.FindObjectOfType(typeof(DragHandler)) as DragHandler;

                    if (instance == null)
                    {
                        GameObject go = new GameObject("DragHandler");
                        DontDestroyOnLoad(go);
                        instance = go.AddComponent<DragHandler>();
                    }
                }
                return instance;
            }
        }

        private List<GameObject> objList;
        private List<IDraggable> dragList;
        private IDraggable draggedObject;
        // button values are 0=left,1=right,2=middle
        const int mouseButton = 0;
        bool isDragging;

        public DragHandler()
        {
            objList = new List<GameObject>();
            dragList = new List<IDraggable>();
        }

        public static void Subscribe<T>(T obj) where T : MonoBehaviour, IDraggable
        {
            Instance.objList.Add(obj.gameObject);
            Instance.dragList.Add(obj);
        }

        public void UnSubscribe(IDraggable obj)
        {
           // objList.Remove(obj);
        }

        public static void StopDragging()
        {
            if(Instance.isDragging){

                Instance.isDragging = false;
                Instance.draggedObject.EndDrag();
            }
        }

        public void Update()
        {
            
            if (isDragging) // Leading underscore denotes private member variables
            {
                if (Input.GetMouseButton(mouseButton))
                    draggedObject.UpdateDrag(); // Move your object according to Input.mousePosition
                if (Input.GetMouseButtonUp(mouseButton))
                {
                    isDragging = false;
                    draggedObject.EndDrag(); // Set the object's position to its final destination
                }
            }
            else if (Input.GetMouseButtonDown(mouseButton))
            {
                // The bottom-left of the screen or window is at x:0, y:0
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // This requires a Collider component on the draggable objects
                if (Physics.Raycast(ray, out hit))
                {
                    int pos = objList.IndexOf(hit.transform.gameObject);
                    if(pos > -1){
                        
                        draggedObject = dragList[pos];
                        if (draggedObject.CanDrag)
                        {
                            isDragging = true;
                            // Here you can capture the object being dragged,
                            //     or create a phantom representing the destination
                            //     until the drag is completed or cancelled.
                            draggedObject.BeginDrag();
                        }
                    }
                }
            }
        }
    }
}
