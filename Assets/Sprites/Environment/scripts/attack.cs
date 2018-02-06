using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag("box"))
		{
			DestroyObject(col.gameObject);
		}
	}
	//otto
	// Update is called once per frame
	void Update () {
		
	}
}
