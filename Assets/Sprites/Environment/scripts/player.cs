using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {
	public float moveSpeed = 10.0f;
	bool facingRight = true;

	public float jumpSpeed = 300.0f;

	public float smoothTimeY = 0.1f;
	public float smoothTimeX = 0.1f; 

	Vector2 spawnPoint;

	public int playerLives = 5;
	public int coinPickups = 0;
	public Text lives;
	public Text coincounter;

	bool attacking;
	float attackTime;
	public float attackCD = 0.3f;
	public Collider2D batTrigger;

	Animator anim;
	Rigidbody2D rigid;
	float move;
	// Use this for initialization

	void Start () {
		spawnPoint = transform.position;
		UpdateCounter();
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

		//Camera.transform.x = Player.transform.x;
			

		//Camera.transform.z = transform.z;
		//float posX = Mathf.SmoothDamp (Camera.transform.position.x, transform.position.x, ref cameraVelocity.x, smoothTimeX);
		//float posY = Mathf.SmoothDamp (Camera.transform.position.y, transform.position.y, ref cameraVelocity.y, smoothTimeY);
		//Camera.transform.position = new Vector3(posX, posY, Camera.transform.position.z);
	}

	void UpdateCounter()
	{
		lives.text = playerLives.ToString();
		coincounter.text = coinPickups.ToString();
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


	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "coin")
		{
			DestroyObject(col.gameObject);
			coinPickups = coinPickups + 1;
			UpdateCounter();
		}
		if (col.gameObject.tag == "deathzone")
		{
			if(playerLives > 0)
			{
				playerLives = playerLives - 1;
				transform.position = spawnPoint;
			}
			else
			{
				Debug.Log (spawnPoint);
				lives.text = "5";
				coincounter.text = "0";
				transform.position = spawnPoint;
			}
		}
		UpdateCounter();
	}
}

