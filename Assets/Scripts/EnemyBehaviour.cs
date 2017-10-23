using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float speed;
	public float jumpSpeed;

	private Rigidbody2D enemyRigidBody;
	private float moveHorizontal;
	private float moveVertical;

	void Start () {
		enemyRigidBody = this.gameObject.GetComponent<Rigidbody2D> ();
		moveHorizontal = -1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyRigidBody.IsSleeping ()) {
			moveHorizontal = -moveHorizontal;
			moveVertical = jumpSpeed;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("ChangeDirectionBoundary"))
			moveHorizontal = -moveHorizontal;
		if (other.CompareTag ("JumpBoundaryFromLeft") && enemyRigidBody.velocity.x > 0f)
			moveVertical = jumpSpeed;
		if(other.CompareTag("JumpBoundaryFromRight") && enemyRigidBody.velocity.x < 0f )
			moveVertical = jumpSpeed;
	}
		
	void FixedUpdate(){
		Vector2 movement = new Vector2 (moveHorizontal * speed, enemyRigidBody.velocity.y+moveVertical);
		moveVertical = 0f;
		enemyRigidBody.velocity = movement;
	}
}
