using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour {
    public float speed;
    void Update()
    {
        //Offset += Scroll * Time.deltaTime;
        renderer.material.mainTextureOffset = new Vector2((Time.time * speed) % 1, 0f);
        //renderer.material.SetTextureOffset("_MainTex", Offset);
    }
}
