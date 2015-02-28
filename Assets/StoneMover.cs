using UnityEngine;
using System.Collections;
using System;

public class StoneMover : MonoBehaviour {

    public GameObject Land;
	// Use this for initialization
    private Vector3 _start;
    private Vector3 _end;
    private Vector3 _startingPosition;
	void Start () {
        try
        {
            var landManager = Land.GetComponent<LandManager>();
            _start = landManager.GetStart();
            _end = landManager.GetEnd();
           
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        _startingPosition = transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(_start!=null && _end != null)
        {
            if(transform.position.y < _end.y)
                transform.Translate(( _end - _start).normalized * Time.deltaTime );
            else
            {
                transform.position = new Vector3(_startingPosition.x, _start.y, _startingPosition.z);
            }
        }
	}
}
