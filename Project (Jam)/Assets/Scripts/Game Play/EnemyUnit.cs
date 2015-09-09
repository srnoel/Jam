using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour {

	public enum Enemy { Flyer, Creeping, Walker };

	[SerializeField] private Enemy enemie;
	[SerializeField] private float baseVelocity;
	[SerializeField] private float maxDistance;
	private float maxY = 7.5f;
	private float minY = -4f;
	private float floor = -9.2f;
	private float spawn = 30f;
	private float velocity;
	
	void Start(){

		velocity = baseVelocity;
	}
	
	void FixedUpdate () {
		if (transform.position.x <= maxDistance)
			Destroy(gameObject);
		
		transform.position += Vector3.right * -(velocity * Time.deltaTime);
	}
	
	public void SetStart(float gameTime){
		
		velocity *= gameTime;
		if (enemie == Enemy.Flyer){
			transform.position = new Vector3(spawn, Random.Range(minY, maxY),0);
		} else {
			transform.position = new Vector3(spawn, floor,0);
		}
	}
}