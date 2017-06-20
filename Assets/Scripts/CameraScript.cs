using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	private Vector2 speed;

	public float delayX;
	public float delayY;

	public Transform player;
	public Vector3 posMinCamera;
	public Vector3 posMaxCamera;

	void Update () {

		//suaviza o movimento da camera no eixoX, com um determinado atraso (atrasoX)
		float posX = Mathf.SmoothDamp (transform.position.x, player.position.x, ref speed.x, delayX);
		//suaviza o movimento da camera no eixoY, com um determinado atraso (atrasoY)
		float posY = Mathf.SmoothDamp (transform.position.y, player.position.y, ref speed.y, delayY);

		//atualiza a posição da camera;
		transform.position = new Vector3 (posX, posY, transform.position.z);

		//limita a posição da camera nas medidas dos vetores posicaoMinCamera e posicaoMaxCamera
		//A função Mathf.Clamp limita a posição da camera usando os valores dos vetores posicaoMinCamera e posicaoMaxCamera
		transform.position = new Vector3 (
			Mathf.Clamp(transform.position.x, posMinCamera.x, posMaxCamera.x),
			Mathf.Clamp(transform.position.y, posMinCamera.y, posMaxCamera.y),
			transform.position.z
		);
	}
}
