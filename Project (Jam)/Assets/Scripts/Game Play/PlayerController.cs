using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
		
	public enum Spirit{paca, tucano, mico};

	private Spirit mySpirit;

	/*Force*/
	[SerializeField] private float pacaForce;
	[SerializeField] private float tucanoForce;
	[SerializeField] private float micoForce;
	[SerializeField] private float jumpForce;

	/*Mesh*/
	[SerializeField] private GameObject[] spiritMesh;
	private GameObject currentMesh;

	/*Physics*/
	private Rigidbody2D rigidBody;
	private bool canJump = true;
	private bool canFly = true;
	[SerializeField] private float timeFly;
	[SerializeField] private float pacaDrag;
	[SerializeField] private float tucanoDrag;
	[SerializeField] private float micoDrag;

	/*Particles*/
	[SerializeField] private GameObject smokeParticle;
	[SerializeField] private float timeSmoke;

	void Awake(){
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void Start(){
		ChangeSpirit(Spirit.paca);
	}

	void Update(){
		if (Input.GetKeyDown("1") && mySpirit != Spirit.paca){

			ChangeSpirit(Spirit.paca);
		} else if (Input.GetKeyDown("2") && !canJump && mySpirit != Spirit.tucano){

			ChangeSpirit(Spirit.tucano);
		} else if (Input.GetKeyDown("3") && mySpirit != Spirit.mico){

			ChangeSpirit(Spirit.mico);
		}
	}

	void FixedUpdate(){

		if (Input.GetKey("space")){

			Jump();
		}
	}

	public void Jump(){

		if (canJump && mySpirit != Spirit.tucano){

			Animator anim = currentMesh.GetComponent<Animator>();
			if (anim){
				anim.SetBool("Jump", true);
			}

			rigidBody.AddForce(Vector3.up * (jumpForce * Time.deltaTime), ForceMode2D.Impulse);
			canJump = false;
		} else if (canFly &&  mySpirit == Spirit.tucano){

			Animator anim = currentMesh.GetComponent<Animator>();
			if (anim){
				anim.SetBool("Jump", true);
			}

			rigidBody.AddForce(Vector3.up * (jumpForce * Time.deltaTime), ForceMode2D.Impulse);
			canFly = false;
			canJump = false;
			Invoke("SetFly", timeFly);
		}
	}

	void SetParticle(){

		GameObject _particle = Instantiate(
			smokeParticle,
			Vector3.zero,
			Quaternion.Euler(Vector3.zero)
		) as GameObject;

		_particle.transform.SetParent(gameObject.transform);
		_particle.transform.localPosition = Vector3.zero;
		_particle.GetComponent<ParticleUnit>().SelfDestruct(timeSmoke);
	}

	public void ChangeSpirit(Spirit _spirit){

		mySpirit = _spirit;

		if (mySpirit == Spirit.paca){

			jumpForce = pacaForce;
			rigidBody.drag = pacaDrag;
			ChangeMesh(0);
			GameManager.gameM.ChangeSpiritAvatar(0);
		} else if (mySpirit == Spirit.tucano){

			jumpForce = tucanoForce;
			rigidBody.drag = tucanoDrag;
			ChangeMesh(1);
			GameManager.gameM.ChangeSpiritAvatar(1);
		} else if (mySpirit == Spirit.mico){

			jumpForce = micoForce;
			rigidBody.drag = micoDrag;
			ChangeMesh(2);
			GameManager.gameM.ChangeSpiritAvatar(2);
		}

		SetParticle();
	}

	private void ChangeMesh(int index){

		foreach (GameObject mesh in spiritMesh){
			mesh.SetActive(false);
		}

		currentMesh = spiritMesh[index];

		currentMesh.SetActive(true);
		Animator animator = currentMesh.GetComponent<Animator>();
		if (animator){
			if (mySpirit == Spirit.tucano)
				animator.SetBool("Jump", false);
			else if (canJump)
				animator.SetBool("Jump", false);
			else
				animator.SetBool("Jump", true);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
	if (other.gameObject.CompareTag("Ground")){
			Animator anim = currentMesh.GetComponent<Animator>();
			if (anim){
				anim.SetBool("Jump", false);
			}
			canJump = true;
			if (mySpirit == Spirit.tucano)
				ChangeSpirit(Spirit.paca);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("Enemy")){
			SetDeath();
		}
	}

	void SetFly(){
		Animator anim = currentMesh.GetComponent<Animator>();
		if (anim && mySpirit == Spirit.tucano)
			anim.SetBool("Jump", false);

		canFly = true;
	}

	void SetDeath(){
		foreach (GameObject mesh in spiritMesh){
			mesh.SetActive(false);
		}
		GameManager.gameM.readyToStart = false;
		GameManager.gameM.inGame = false;

		GameObject _particle = Instantiate(
			smokeParticle,
			Vector3.zero,
			Quaternion.Euler(Vector3.zero)
			) as GameObject;

		_particle.transform.localPosition = transform.localPosition;
		_particle.GetComponent<ParticleUnit>().SelfDestruct(timeSmoke);
	}
}