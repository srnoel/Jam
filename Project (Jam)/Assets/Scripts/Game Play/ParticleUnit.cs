using UnityEngine;
using System.Collections;

public class ParticleUnit : MonoBehaviour {

	[SerializeField] private float timeDestruct;

	public void SelfDestruct(float time){
		Invoke("ClearParticles", time);
	}

	void ClearParticles(){
		GetComponent<ParticleSystem>().enableEmission = false;
		Invoke("Destruct", timeDestruct);
	}

	void Destruct(){

		Destroy(gameObject);
	}
}