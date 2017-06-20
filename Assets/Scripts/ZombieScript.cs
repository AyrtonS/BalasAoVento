using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {

	private Rigidbody2D rb;
	public int speed;
	public int life;
	public AnimationClip clip;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		clip = GetComponent<AnimationClip>();
	}

	void FixedUpdate(){

		rb.velocity = new Vector2 (speed, rb.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.CompareTag ("Shot")) {

			life--;
			Destroy (other.gameObject);

			if (life == 0){
				//Destroy (gameObject, 0.5f);
				Animation cl = clip;
				cl.Play("ZombieDeath");
			}
		}
	}
}
