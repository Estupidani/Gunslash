using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {
	public float speed;
	public float jumpSpeed;
	private bool inTheAir;


	void Update(){
		Transform playerTransform = this.gameObject.transform;
		Rigidbody2D playerRigidBody=this.gameObject.GetComponent<Rigidbody2D>();
		playerTransform.rotation = new Quaternion (0, 0, 0, 0);
		if (playerRigidBody.velocity.y <= 0) 
			inTheAir = false;
		float moveVertical = 0;
		if (Input.GetKeyDown (KeyCode.Space) && !inTheAir) {
			moveVertical = jumpSpeed;
			inTheAir = true;
		}
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
