using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

	public float speed;
	private Rigidbody2D rb;

	void Start(){

		rb = GetComponent<Rigidbody2D> ();

		if (transform.rotation.eulerAngles.y == 180) {

			speed *= -1;
		}
	}

	void Update () {

		rb.velocity = new Vector2 (speed, rb.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.CompareTag ("Ground")) {

			Destroy (gameObject);
		}
	}

	
}
