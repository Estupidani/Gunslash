using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {
	public float speed;
	public float jumpSpeed;


	void Update(){
		Transform playerTransform = this.gameObject.transform;
		playerTransform.rotation = new Quaternion (0, 0, 0, 0);
		float moveVertical = 0;
		if( Input.GetKeyDown(KeyCode.Space))
			moveVertical=jumpSpeed;
		Rigidbody2D playerRigidBody=this.gameObject.GetComponent<Rigidbody2D>();
		Vector2 movement = new Vector2 (playerRigidBody.velocity.x, playerRigidBody.velocity.y+moveVertical);
		playerRigidBody.velocity = movement;
	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		Rigidbody2D playerRigidBody=this.gameObject.GetComponent<Rigidbody2D>();
		Vector2 movement = new Vector2 (moveHorizontal * speed, playerRigidBody.velocity.y);
		playerRigidBody.velocity = movement;
	}
}
