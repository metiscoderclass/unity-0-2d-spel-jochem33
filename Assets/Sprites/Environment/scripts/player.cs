﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
	public float moveSpeed = 10.0f;
	bool facingRight = true;

	Animator anim;
	Rigidbody2D rigid;
	float move;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		move = Input.GetAxis("Horizontal");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		anim.SetFloat ("speed", Mathf.Abs (move));
		rigid.velocity = new Vector2 (move * moveSpeed, rigid.velocity.y);
		if (move > 0 && !facingRight) {
			FlipFacing ();
		} else if (move < 0 && facingRight) {
			FlipFacing ();
		}
	}

	void FlipFacing() {
		facingRight = !facingRight;
		Vector3 charScale = transform.localScale;
		charScale.x = charScale.x * -1;
		transform.localScale = charScale;
	}
}
