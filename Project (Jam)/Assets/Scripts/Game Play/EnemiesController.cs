using UnityEngine;
using System.Collections;

public class EnemiesController : MonoBehaviour {

	public static EnemiesController enemieC;

	[SerializeField] private float maxFlyerTime;
	[SerializeField] private float minFlyerTime;

	[SerializeField] private float maxCreepingTime;
	[SerializeField] private float minCreepingTime;

	[SerializeField] private float maxWalkerTime;
	[SerializeField] private float minWalkerTime;

	[SerializeField] private GameObject[] enemiesPrefab;
	
	void Awake(){
		enemieC = this;
		Invoke("SummonFlyer", Random.Range(minFlyerTime, maxFlyerTime));
		Invoke("SummonCreeping", Random.Range(minCreepingTime, maxCreepingTime));
		Invoke("SummonWalker", Random.Range(minWalkerTime, maxWalkerTime));
	}

	void SummonFlyer(){
		if (GameManager.gameM.inGame){
			GameObject _enemy = Instantiate(
				enemiesPrefab[1],
				Vector3.zero,
				Quaternion.Euler(Vector3.zero)
			) as GameObject;
				
			_enemy.GetComponent<EnemyUnit>().SetStart(GameManager.gameM.gameSpeed);
		}
		Invoke("SummonFlyer", Random.Range(minFlyerTime, maxFlyerTime));
	}

	void SummonCreeping(){
		if (GameManager.gameM.inGame){
			GameObject _enemy = Instantiate(
				enemiesPrefab[0],
				Vector3.zero,
				Quaternion.Euler(Vector3.zero)
				) as GameObject;
			
			_enemy.GetComponent<EnemyUnit>().SetStart(GameManager.gameM.gameSpeed);
		}
		Invoke("SummonCreeping", Random.Range(minCreepingTime, maxCreepingTime));
	}

	void SummonWalker(){
		if (GameManager.gameM.inGame){
			GameObject _enemy = Instantiate(
				enemiesPrefab[2],
				Vector3.zero,
				Quaternion.Euler(Vector3.zero)
				) as GameObject;
			
			_enemy.GetComponent<EnemyUnit>().SetStart(GameManager.gameM.gameSpeed);
		}
		Invoke("SummonWalker", Random.Range(minWalkerTime, maxWalkerTime));
	}
}