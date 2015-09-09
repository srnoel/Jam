using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CenaryController : MonoBehaviour {

	public static CenaryController singleton;

	public float startPosition;

	public GameObject layer;
	public GameObject layerPool;

	public GameObject mask;
	public GameObject maskPool;

	public float maskTime;
	public float maskTimeGround;
	public float maskVelocity;
	public GameObject[] bushsMask;
	public GameObject[] treeMask;
	public int treeMaskPercentage;
	public Material maskMaterial;
	public Vector2 maskHeightSky;
	public Vector2 maskHeightGround;
	public int maskHeightTree;

	public GameObject[] trees;
	public Vector2 timeLayer2;
	public float velLayer2;
	public int layer2;
	public Material matLayer2;

	public Vector2 timeLayer3;
	public float velLayer3;
	public int layer3;
	public Material matLayer3;

	void Awake(){
		singleton = this;
	}

	void Start(){

		Invoke ("GameMaskSky", maskTime);
		Invoke ("GameMaskGround", maskTimeGround);
		Invoke ("InstantiateLayerTwo", Random.Range(timeLayer2.x, timeLayer2.y));
		Invoke ("InstantiateLayerThree", Random.Range(timeLayer3.x, timeLayer3.y));
	}

	void GameMaskSky(){

		List<Transform> pool = new List<Transform>();
		
		foreach(Transform _temp in maskPool.transform){
			pool.Add(_temp);
		}
		
		if (pool.Count < 5){

			GameObject _prefab = bushsMask[Random.Range(0, bushsMask.Length)];

			GameObject _bush = Instantiate(
				_prefab, 
				Vector3.zero, 
				Quaternion.Euler(Vector3.zero)
			) as GameObject;

			_bush.GetComponent<CenaryUnit>().SetStart(1.5f, maskVelocity, maskMaterial, 15);
			_bush.transform.position = new Vector3(startPosition, Random.Range(maskHeightSky.x, maskHeightSky.y), 0);
		
		} else {

			GameObject _prefab = pool[Random.Range(0, pool.Count)].gameObject;
			
			_prefab.GetComponent<CenaryUnit>().SetStart(1.5f, maskVelocity, maskMaterial, 15);
			_prefab.transform.position = new Vector3(startPosition, Random.Range(maskHeightSky.x, maskHeightSky.y), 0);
			_prefab.SetActive(true);
			_prefab.transform.SetParent(mask.transform);
		}

		int treeChance = Random.Range(0,10);
		if (treeChance < treeMaskPercentage)
			SummonTree();

		Invoke ("GameMaskSky", maskTime);
	}

	void GameMaskGround(){

		List<Transform> pool = new List<Transform>();
		
		foreach(Transform _temp in maskPool.transform){
			pool.Add(_temp);
		}
		
		if (pool.Count < 5){

			GameObject _prefab = bushsMask[Random.Range(0, bushsMask.Length)];
			
			GameObject _bush = Instantiate(
				_prefab, 
				Vector3.zero, 
				Quaternion.Euler(Vector3.zero)
				) as GameObject;
			
			_bush.GetComponent<CenaryUnit>().SetStart(-1f, maskVelocity, maskMaterial, 15);
			_bush.transform.position = new Vector3(startPosition, Random.Range(maskHeightGround.x, maskHeightGround.y), 0);
		} else {

			GameObject _prefab = pool[Random.Range(0, pool.Count)].gameObject;
			
			_prefab.GetComponent<CenaryUnit>().SetStart(-1f, maskVelocity, maskMaterial, 15);
			_prefab.transform.position = new Vector3(startPosition, Random.Range(maskHeightGround.x, maskHeightGround.y), 0);
			_prefab.SetActive(true);
			_prefab.transform.SetParent(mask.transform);
		}

		Invoke ("GameMaskGround", maskTimeGround);
	}

	void SummonTree(){
		GameObject _prefab = treeMask[Random.Range(0, treeMask.Length)];
		
		GameObject _bush = Instantiate(
			_prefab, 
			Vector3.zero, 
			Quaternion.Euler(Vector3.zero)
			) as GameObject;
		
		_bush.GetComponent<CenaryUnit>().SetStart(1.35f, maskVelocity, maskMaterial, 15);
		_bush.transform.position = new Vector3(startPosition, maskHeightTree, 0);
	}

	void InstantiateLayerTwo(){

		List<Transform> pool = new List<Transform>();
		
		foreach(Transform _temp in layerPool.transform){
			pool.Add(_temp);
		}
		
		if (pool.Count < 5){
			
			GameObject _prefab = trees[Random.Range(0, trees.Length)];
			
			GameObject _bush = Instantiate(
				_prefab, 
				Vector3.zero, 
				Quaternion.Euler(Vector3.zero)
				) as GameObject;
			
			_bush.GetComponent<CenaryUnit>().SetStart(1.5f, velLayer2, matLayer2, layer2);
			_bush.transform.position = new Vector3(startPosition, maskHeightTree, 0);
			
			_bush.transform.SetParent(layer.transform);
		} else {
			
			GameObject _prefab = pool[Random.Range(0, pool.Count)].gameObject;

			_prefab.GetComponent<CenaryUnit>().SetStart(1.5f, velLayer2, matLayer2, layer2);
			_prefab.transform.position = new Vector3(startPosition, maskHeightTree, 0);
			_prefab.SetActive(true);
			_prefab.transform.SetParent(layer.transform);
		}

		Invoke ("InstantiateLayerTwo", Random.Range(timeLayer2.x, timeLayer2.y));
	}

	void InstantiateLayerThree(){

		List<Transform> pool = new List<Transform>();

		foreach(Transform _temp in layerPool.transform){
			pool.Add(_temp);
		}

		if (pool.Count < 5){

			GameObject _prefab = trees[Random.Range(0, trees.Length)];
			
			GameObject _bush = Instantiate(
				_prefab, 
				Vector3.zero, 
				Quaternion.Euler(Vector3.zero)
				) as GameObject;
			
			_bush.GetComponent<CenaryUnit>().SetStart(1.2f, velLayer3, matLayer3, layer3);
			_bush.transform.position = new Vector3(startPosition, maskHeightTree, 0);
			
			_bush.transform.SetParent(layer.transform);
		} else {

			GameObject _prefab = pool[Random.Range(0, pool.Count)].gameObject;

			_prefab.GetComponent<CenaryUnit>().SetStart(1.2f, velLayer3, matLayer3, layer3);
			_prefab.transform.position = new Vector3(startPosition, maskHeightTree, 0);
			_prefab.SetActive(true);
			_prefab.transform.SetParent(layer.transform);
		}

		Invoke ("InstantiateLayerThree", Random.Range(timeLayer3.x, timeLayer3.y));
	}

	public void AddToLayerPool(Transform _transform){

		_transform.SetParent(layerPool.transform);
		_transform.gameObject.SetActive(false);
	}

	public void AddToMaskPool(Transform _transform){
		
		_transform.SetParent(maskPool.transform);
		_transform.gameObject.SetActive(false);
	}
}