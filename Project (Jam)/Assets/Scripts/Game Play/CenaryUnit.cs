using UnityEngine;
using System.Collections;

public class CenaryUnit : MonoBehaviour {

	[SerializeField] private float velocity;
	[SerializeField] private float maxDistance;
	[SerializeField] private Renderer render;

	void FixedUpdate () {
		if (transform.position.x <= maxDistance){
			if (gameObject.CompareTag("LayerTree"))
				CenaryController.singleton.AddToLayerPool(gameObject.transform);
			else if (gameObject.CompareTag("MaskBush")) 
				CenaryController.singleton.AddToMaskPool(gameObject.transform);
			else
				Destroy(gameObject);
		}
		
		transform.position += Vector3.right * -(velocity * Time.deltaTime);
	}
	
	public void SetStart(float scale, float _velocity, Material _material, int layer){
		
		velocity = _velocity;
		render.material = _material;
		render.sortingOrder = layer;
		render.gameObject.transform.localScale = new Vector3(scale, scale, scale);
	}
}