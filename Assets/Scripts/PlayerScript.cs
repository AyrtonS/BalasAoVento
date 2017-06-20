using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	private Rigidbody2D rb;
	public Transform verifyGround;
	public Transform verifyObstacle;

	private bool isInGround;
	private bool isInObstacle;

	private float axis;
	public float speed;
	private float vesticalSpeed;
	public float forceImpulse;
	public float rayCheckGround;
	public float rayCheckObstacle;

	public LayerMask ground;

	Animator anim;
	int speedHash = Animator.StringToHash("speed");
	int verticalSpeedHash = Animator.StringToHash("verticalSpeed");
	int isInGroundHash = Animator.StringToHash ("inGround");
	bool facingRight;

	public GameObject shot;
	public Transform originShot;

	public int life;

	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		facingRight = true;
		life = 3;
	}

	void Update () {
		//verifica se o personagem está no chão
		//checa se a esfera centrada no gameObject VerifyGround toca em algum objeto da camada ground
		isInGround = Physics2D.OverlapCircle (new Vector2 (verifyGround.position.x, verifyGround.position.y), 0.3f, ground.value);

		//verifica se o personagem está em um obstáculo
		//checa se a esfera centrada no gameObject VerifyObstacle toca em algum objeto da camada ground
		isInObstacle = Physics2D.OverlapCircle (new Vector2 (verifyObstacle.position.x, verifyObstacle.position.y), 0.3f, ground.value);

		//atribui o valor do atributo isInGround para o parâmetro inGround do animator do Player
		anim.SetBool (isInGroundHash, isInGround);

		//obtem a entrada das teclas A e D
		//axis receberá valor zero quando o jogador não pressionar A ou D
		//axis receberá um valor entre 0.1 e 1 de acordo com o tempo que o
		//jogador pressionar as teclas A ou D
		axis = Input.GetAxis ("Horizontal");

		//envia o valor da velocidade do personagem (speed) para o parametro speed do animator
		anim.SetFloat (speedHash, Mathf.Abs (axis));

		//envia o valor da velocidade vertical do personagem para o parametro verticalSpeed do animator
		anim.SetFloat (verticalSpeedHash, rb.velocity.y);

		//verifica se o jogador apertou a tecla de barra de espaço e se está no chão
		if (Input.GetButtonDown ("Jump") && isInGround) {

			//aplica uma força do tipo impulso no sentido do eixo Y positivo, ou seja, para cima
			rb.AddForce (new Vector2 (0, forceImpulse), ForceMode2D.Impulse);
		}

		//verifica se o usuário está pressionando o botão esquerdo do mouse
		if (Input.GetMouseButtonDown (0)) {

			//invoca (chama) a cada 0.1 segundos a função Shooting
			InvokeRepeating ("Shooting", 0f, 0.1f);
		}

		//Quando o usuário soltar (deixar de pressionar) o botão esquerdo do mouse
		if (Input.GetMouseButtonUp (0)) {

			//deixa de chamar a cada 0.1 segundos a função Shooting
			CancelInvoke ("Shooting");
		}

		//verifica se o usuário está apertando as teclas A ou D e se o personagem está virado para direita
		if (axis < 0 && facingRight)
			//vira o personagem
			Turn ();
		//verifica se o usuário está apertando as teclas A ou D e se o personagem está virado para esquerda (não direita)
		else if (axis > 0 && !facingRight)
			//vira o personagem
			Turn ();
	}

	void FixedUpdate(){

		//verifica se o usuário está apertando as teclas A ou D e se o personagem não está preso em algum obstáculo
		if (Mathf.Abs (axis) > 0.1f && !isInObstacle) {

			//movimenta o personagem de acordo com a direção do eixo (axis) horizontal
			rb.velocity = new Vector2 (axis * speed, rb.velocity.y);
		}
	}

	void Turn(){

		facingRight = !facingRight;
		//vira o personagem mudando o sinal da escala no eixo X
		//mudar o sinal (positivo para negativo e vice-versa) é um truque comum de computação gráfica
		//para virar um modelo ou imagem
		transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
	}

	void Shooting(){
		//se o jogador estiver voltado (virado) para a direita
		if (facingRight) 
			//cria um tiro na mesma direção que o personagem (sem rotacionar o tiro)
			Instantiate (shot, originShot.position, originShot.rotation);
		else
			//cria um tiro e o rotaciona 180 graus no eixo Y, para que ele vá para a esquerda
			//já que o personagem também estará virado para a esquerda
			//se o tiro está rotacionado 180 graus, a direção de movimentação do tiro também 
			//é virada para que o tiro se movimente para a esquerda (ver a função Start do 
			//script ShotScript.cs)
			Instantiate (shot, originShot.position, originShot.rotation).transform.Rotate( new Vector3(0, 180, 0));
	}

	void OnTriggerEnter2D(Collider2D other){

		//se o tiro colidir com um zumbi
		if(other.CompareTag("Zombie")){

			//tira 1 ponto de vida do jogador
			life--;
			//escreve a vida atual do jogador no console
			print (life.ToString ());
			//verifica se a vida do jogador acabou
			if(life == 0)
				//imprime no console o texto   Morreu :(
				print("Morreu :(");
		}
	}
}
