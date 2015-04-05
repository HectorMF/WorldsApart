using UnityEngine;
using System.Collections;
using System;

public class WaterBucketAnimationHandler : MonoBehaviour {

    public Sprite Stable;
    public Sprite Dripping1;
    public Sprite Dripping2;
    public Sprite Dripping3;
    public Sprite Dripping4;
    public Sprite Dripping5;

    private WaterBasket wb;
    private float step = 0f;
    private bool flipped = false;
	// Use this for initialization
	void Start () {
        try { wb = gameObject.GetComponent<WaterBasket>(); }
        catch (Exception e) { Debug.LogException(e); }
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = wb.Camera.transform.rotation;
        Vector3 scale = transform.localScale;
        if(!flipped)
        {
            if (!(wb.Camera.transform.rotation.eulerAngles.z > 180 && wb.Camera.transform.rotation.eulerAngles.z < 360))
            {
                scale.x *= -1;
                transform.localScale = scale;
                flipped = true;
            }
        }
        else
        {
            if (wb.Camera.transform.rotation.eulerAngles.z > 180 && wb.Camera.transform.rotation.eulerAngles.z < 360)
            {
                scale.x *= -1;
                transform.localScale = scale;
                flipped = false;
            }
        }
        

        
	}
    public void DripWater(GameObject gameObject, int strength)
    {
        if(step==0)
        {
            step += strength;
        }

       
        try
        {
            SpriteRenderer spRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (strength == 0 && step <2)
            {
                spRenderer.sprite = Stable;
               // step = 0;
            }
            else
            {
                switch (Convert.ToInt32(step))
                {
                    case 0:
                        spRenderer.sprite = Stable; break;
                    case 1:
                        step += 0.05f;
                        spRenderer.sprite = Dripping1; break;
                    case 2:
                        step += 0.05f;
                        spRenderer.sprite = Dripping2; break;
                    case 3:
                        step += 0.05f;
                        spRenderer.sprite = Dripping3; break;
                    case 4:
                        step += 0.05f;
                        spRenderer.sprite = Dripping4; break;
                    case 5:
                        step += 0.05f;
                        spRenderer.sprite = Dripping5; break;
                    case 6:
                        step = 0;break;

                }
            }

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

    }
}
