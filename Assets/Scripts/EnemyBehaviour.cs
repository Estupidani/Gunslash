using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float speed;
	public float jumpSpeed;
	public Boundary boundary;
	public float attackLength;

	private Rigidbody2D enemyRigidBody;
	private float moveHorizontal;
	private float moveVertical;
	private bool isAttacking;
	private float timeAttack;

	void Start () {
		isAttacking = false;
		enemyRigidBody = this.gameObject.GetComponent<Rigidbody2D> ();
		moveHorizontal = -1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyRigidBody.IsSleeping () && !isAttacking) {
			moveHorizontal = -moveHorizontal;
			moveVertical = jumpSpeed;
		}
		CheckBoundaries ();
		if (isAttacking && Time.time > timeAttack + attackLength)
			isAttacking = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("ChangeDirectionBoundary"))
			moveHorizontal = -moveHorizontal;
		if (other.CompareTag ("JumpBoundaryFromLeft") && enemyRigidBody.velocity.x > 0f)
			moveVertical = jumpSpeed;
		if(other.CompareTag("JumpBoundaryFromRight") && enemyRigidBody.velocity.x < 0f )
			moveVertical = jumpSpeed;
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag ("PlayerInteractHitbox"))
			Attack ();
	}
		
	void FixedUpdate(){
		Vector2 movement = new Vector2 (moveHorizontal * speed, enemyRigidBody.velocity.y+moveVertical);
		moveVertical = 0f;
		if(!isAttacking)
			enemyRigidBody.velocity = movement;
	}

	void Attack(){
		timeAttack = Time.time;
		Vector2 auxMovement = new Vector2 (moveHorizontal, moveVertical);
		isAttacking = true;
		enemyRigidBody.velocity = new  Vector2 (0f, 0f);
	}

	void CheckBoundaries(){ //Checks wether the character is out of bounds. If it is, prevents it from going further
		enemyRigidBody.position = new Vector2 (
			Mathf.Clamp (enemyRigidBody.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp (enemyRigidBody.position.y, boundary.yMin, boundary.yMax));
	}
}
