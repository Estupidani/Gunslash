using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
	public float speed;
	private Rigidbody2D projectileRigidBody;

	void Start () {
		projectileRigidBody = this.gameObject.GetComponent<Rigidbody2D> ();
	}
}
