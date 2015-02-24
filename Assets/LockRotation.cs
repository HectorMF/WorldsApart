using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LockRotation : MonoBehaviour {
    private Quaternion rotation;

	void Start () {
        rotation = this.transform.rotation;
        //transform.parent.transform.DOShakePosition(100,new Vector3(0,.1f,0),1);
        transform.parent.transform.DOShakeScale(100, new Vector3(.1f, .1f, 0),1);
    }
	
	void Update () {
        this.transform.rotation = rotation;

	}
}
