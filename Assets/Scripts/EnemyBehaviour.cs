using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float speed;
	public float jumpSpeed;
	public Boundary boundary;
	public float attackLength;
	public int lifePoints;
	public CharacterControl character;
	public GameController controller;
	public GameObject attackHitBox;

	private Rigidbody2D enemyRigidBody;
	private float moveHorizontal;
	private float moveVertical;
	private bool isAttacking;
	private float timeAttack;
	private float nextAttack;
	private bool enemyType;
	private bool firstAttack;

	void Start () {
		isAttacking = false;
		enemyRigidBody = this.gameObject.GetComponent<Rigidbody2D> ();
		enemyType = GameController.EnemyType ();
		firstAttack = true;
		if (enemyType)
			moveHorizontal = -1f;
		else
			moveHorizontal = 1f;
	}

	void Update () {
		if (lifePoints <= 0)
			Destroy (this.gameObject);
		if (enemyRigidBody.IsSleeping () && !isAttacking) {//Changes direction and jumps in order to not get stuck
			moveHorizontal = -moveHorizontal;
			moveVertical = jumpSpeed;
		}
		CheckBoundaries ();
		if (isAttacking)
			Attack ();
		if (isAttacking && Time.time > timeAttack + attackLength) {
			firstAttack = true;
			isAttacking = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("ChangeDirectionBoundary"))
			moveHorizontal = -moveHorizontal;
		if (other.CompareTag ("JumpBoundaryFromLeftLower") && enemyRigidBody.velocity.x > 0f)
			moveVertical = jumpSpeed;
		else
			if(other.CompareTag("JumpBoundaryFromRightLower") && enemyRigidBody.velocity.x < 0f )
				moveVertical = jumpSpeed;
		if (other.CompareTag ("JumpBoundaryFromLeftUpper") && enemyRigidBody.velocity.x > 0f && !enemyType)
			moveVertical = jumpSpeed;
		else
			if (other.CompareTag ("JumpBoundaryFromRightUpper") && enemyRigidBody.velocity.x < 0f && enemyType)
				moveVertical = jumpSpeed;
		if (other.CompareTag ("Projectile")) {
			lifePoints--;
			Destroy (other.gameObject);
			controller.FreezeFrame ();
		}
		if (other.CompareTag ("Melee")) {
			lifePoints--;
			character.ammo++;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag ("PlayerInteractHitbox"))
			isAttacking = true;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("PlayerInteractHitbox"))
			isAttacking = false;
	}
		
	void FixedUpdate(){
		Vector2 movement = new Vector2 (moveHorizontal * speed, enemyRigidBody.velocity.y+moveVertical);
		moveVertical = 0f;
		if (!isAttacking)
			enemyRigidBody.velocity = movement;
		else
			enemyRigidBody.velocity = Vector2.zero;
	}

	void Attack(){
		timeAttack = Time.time;
		enemyRigidBody.velocity = Vector2.zero;
		if (Time.time > nextAttack) {
			nextAttack = Time.time + attackLength;
			if (!firstAttack) //delays the first attack to avoid instant hit*/
				attackHitBox.GetComponent<CircleCollider2D> ().radius = 6.5f;
			firstAttack = false;
		} else {
			attackHitBox.GetComponent<CircleCollider2D> ().radius = 1f;
		}
	}

	void CheckBoundaries(){ //Checks wether the character is out of bounds. If it is, prevents it from going further
		enemyRigidBody.position = new Vector2 (
			Mathf.Clamp (enemyRigidBody.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp (enemyRigidBody.position.y, boundary.yMin, boundary.yMax));
	}
}
