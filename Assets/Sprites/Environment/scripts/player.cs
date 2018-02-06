using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
	public float moveSpeed = 10.0f;
	bool facingRight = true;

	public float jumpSpeed = 300.0f;

	public float smoothTimeY = 0.1f;
	public float smoothTimeX = 0.1f; 

	bool attacking;
	float attackTime;
	public float attackCD = 0.3f;
	public Collider2D batTrigger;

	Animator anim;
	Rigidbody2D rigid;
	float move;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		batTrigger.enabled = false;
		//GameObject Camera;
		//Camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("f") && !attacking)
		{
			attacking = true;
			attackTime = attackCD;
			anim.SetTrigger("attack");
		}

		if (attacking)
		{
			if (attackTime > 0)
			{
				attackTime -= Time.deltaTime;
				batTrigger.enabled = true;
			}
			else
			{
				batTrigger.enabled = false;
				attacking = false;
			}
		}
			

		//Camera.transform.z = transform.z;
		//float posX = Mathf.SmoothDamp (Camera.transform.position.x, transform.position.x, ref cameraVelocity.x, smoothTimeX);
		//float posY = Mathf.SmoothDamp (Camera.transform.position.y, transform.position.y, ref cameraVelocity.y, smoothTimeY);
		//Camera.transform.position = new Vector3(posX, posY, Camera.transform.position.z);
	}
		

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag("Box"))
		{
			DestroyObject(col.gameObject);
		}
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

		if (Input.GetButtonDown ("Jump") && rigid.velocity.y == 0) {
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
