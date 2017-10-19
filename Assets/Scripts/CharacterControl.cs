using UnityEngine;
using System.Collections;

public class Boundary{
	public float xMin, xMax, yMin, yMax;
}

public class CharacterControl : MonoBehaviour {
	public float speed;
	public float projectileSpeed;
	public float jumpSpeed;
	public GameObject projectile;
	public float fireRate;
	public Boundary boundary;

	private bool inTheAir;
	private float nextFire;
	private Rigidbody2D playerRigidBody;

	void Start(){
		playerRigidBody=this.gameObject.GetComponent<Rigidbody2D>();
	}
	void Update(){
		Transform playerTransform = this.gameObject.transform;
		if (playerRigidBody.velocity.y == 0) 
			inTheAir = false;
		float moveVertical = 0;
		if (Input.GetKeyDown (KeyCode.Space) && !inTheAir) {
			moveVertical = jumpSpeed;
			inTheAir = true;
		}
		Vector2 movement = new Vector2 (playerRigidBody.velocity.x, playerRigidBody.velocity.y+moveVertical);
		playerRigidBody.velocity = movement;
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Vector2 target = Camera.main.ScreenToWorldPoint( new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
			Vector2 myPos = new Vector2(transform.position.x,transform.position.y);
			Vector2 direction = target - myPos;
			direction.Normalize();
			GameObject bullet = (GameObject)Instantiate( projectile, myPos, Quaternion.identity);
			bullet.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
		}
	}
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2 (moveHorizontal * speed, playerRigidBody.velocity.y);
		playerRigidBody.velocity = movement;
	}

	void CheckBoundaries(){
		playerRigidBody.position = new Vector2 (
			Mathf.Clamp (playerRigidBody.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp (playerRigidBody.position.y, boundary.yMin, boundary.yMax));
	}
}
