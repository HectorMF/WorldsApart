using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class StoneMover : MonoBehaviour {
	public bool scale = true;
	public float duration = 4;
    public GameObject Land;
	// Use this for initialization
    private Vector3 _start;
    private Vector3 _end;
    private Vector3 _startingPosition;
	private bool canMove;
	void Start () {
		canMove = true;
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
		if (WaterGameLogic.Instance.ReadyToPlay == false) return;

		if(!canMove) return;
	    if(_start!=null && _end != null)
        {
            if(transform.position.y < _end.y)
                transform.Translate(( _end - _start).normalized * Time.deltaTime );
            else
            {
				canMove = false;
				Vector3 oldScale = transform.localScale;
				Vector3 tempScale = oldScale;
				Vector3 pos = transform.position;
				if(scale)
					transform.DOScale(new Vector3(0,0,0),duration).OnComplete(()=>{transform.localScale = oldScale; 	transform.position = new Vector3(_startingPosition.x, _start.y, _startingPosition.z); canMove = true;  });
                else{
					transform.localScale = oldScale; 	
					transform.position = new Vector3(_startingPosition.x, _start.y, _startingPosition.z); 
					canMove = true;  
				}
            }
        }
	}
}
