using UnityEngine;
using System.Collections;

public class MouseDragRotate : MonoBehaviour
{
    public float deltaRotation;
    public float deltaLimit;
    public float deltaReduce;
    float previousRotation;
    float currentRotation;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            deltaRotation = 0f;
            previousRotation = angleBetweenPoints(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButton(0))
        {
            currentRotation = angleBetweenPoints(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            deltaRotation = Mathf.DeltaAngle(currentRotation, previousRotation);
            if (Mathf.Abs(deltaRotation) > deltaLimit)
            {
                deltaRotation = deltaLimit * Mathf.Sign(deltaRotation);
            }
            previousRotation = currentRotation;
            transform.Rotate(Vector3.back * Time.deltaTime, deltaRotation);
        }
        else
        {
            transform.Rotate(Vector3.back * Time.deltaTime, deltaRotation);
            deltaRotation = Mathf.Lerp(deltaRotation, 0, deltaReduce * Time.deltaTime);
        }

    }

    float angleBetweenPoints(Vector2 position1, Vector2 position2)
    {
        Vector2 fromLine = position2 - position1;
        Vector2 toLine = new Vector2(1, 0);

        float angle = Vector2.Angle(fromLine, toLine);

        Vector3 cross = Vector3.Cross(fromLine, toLine);

        // did we wrap around?
        if (cross.z > 0)
        {
            angle = 360f - angle;
        }

        return angle;
    }


}