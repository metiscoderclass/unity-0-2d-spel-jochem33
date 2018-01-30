using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
	public float moveSpeed = 10.0f;
	bool facingRight = true;

	public float jumpSpeed = 300.0f;


	Animator anim;
	Rigidbody2D rigid;
	float move;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		move = Input.GetAxis("Horizontal");
		anim.SetFloat ("speed", Mathf.Abs (move));
		rigid.velocity = new Vector2 (move * moveSpeed, rigid.velocity.y);

		if (move > 0 && !facingRight) {
			FlipFacing ();
		} else if (move < 0 && facingRight) {
			FlipFacing ();
		}

		if (Input.GetButtonDown ("Jump")) {
			rigid.AddForce (Vector2.up * jumpSpeed);
		}
	}

	void FlipFacing() {
		facingRight = !facingRight;
		Vector3 charScale = transform.localScale;
		charScale.x = charScale.x * -1;
		transform.localScale = charScale;
	}
}
