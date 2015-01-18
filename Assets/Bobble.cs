using UnityEngine;
using System.Collections;


public class Bobble : MonoBehaviour {
    Vector3 position;
    float offset;
    bool positive;
    public float speed = 1;
    public float maxOffset = .3f;
    public float minOffset = -.3f;
    void Start()
    {
        position = transform.localPosition;
        offset = 0;
    }

    void Update()
    {
        if (positive)
        {
            offset += speed * Time.deltaTime;
            if (offset > maxOffset) positive = false;
        }
        else
        {
            offset -= speed * Time.deltaTime;
            if (offset < minOffset) positive = true;
        }

        transform.localPosition = new Vector3(position.x, position.y + offset, position.z);
    }
}
