using UnityEngine;
using System.Collections;

public class MainCharAnimation : MonoBehaviour {

    private Animator _animator;
    private Movement _movement;
	// Use this for initialization
	void Start () {
        _animator = this.GetComponent<Animator>();
        _movement = this.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_movement)
        {
            _animator.SetBool("Moving", _movement.moving);
        }
	}
}
