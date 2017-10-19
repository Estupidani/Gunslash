using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
	public float speed;
	private Rigidbody2D projectileRigidBody;

	void Start () {
		projectileRigidBody = this.gameObject.GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D other){
		//if(!other.collider.gameObject.CompareTag("Player")){
			Time.timeScale = 0.1f;
			Destroy(gameObject);
		//}
	}
}
