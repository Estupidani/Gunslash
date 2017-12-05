using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float speed;
	public float jumpSpeed;
	public Boundary boundary;
	public float attackLength;
	public int lifePoints;
	public GameObject attackHitBox;
	public GameObject attackWindUp;
	public float hitRate;

	private Rigidbody2D enemyRigidBody;
	private float moveHorizontal;
	private float moveVertical;
	private bool isAttacking;
	private float nextAttack;
	private bool enemyType;
	private bool firstAttack;
	private float nextHit;
	private CharacterControl character;
	private GameController controller;
	private AudioSource enemyHit;
	private AudioSource enemyAttack;

	void Start () {
		AudioSource[] audios = this.GetComponents<AudioSource> ();
		enemyHit = audios [0];
		enemyAttack = audios [1];
		attackHitBox.SetActive (false);
		attackWindUp.SetActive (false);
		GameObject characterGameObject = GameObject.FindWithTag ("Player");
		GameObject controllerGameObject = GameObject.FindWithTag ("GameController");
		character = characterGameObject.GetComponent<CharacterControl> ();
		controller = controllerGameObject.GetComponent<GameController> ();
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
		if (other.CompareTag ("Projectile") && Time.time>nextHit) {
			nextHit = Time.time + hitRate;
			lifePoints--;
			Destroy (other.gameObject);
			if(this.lifePoints == 0)
				controller.FreezeFrame ();
		}
		if (other.CompareTag ("Melee") && Time.time > nextHit) {
			nextHit = Time.time + hitRate;
			lifePoints--;
			enemyHit.Play ();
			if(character.ammo <= 9)
				character.ammo++;
			if(this.lifePoints == 0)
				controller.FreezeFrame ();
		}
		if (other.CompareTag ("PlayerInteractHitbox")) {
			enemyAttack.Play ();
			firstAttack = true;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag ("PlayerInteractHitbox"))
			isAttacking = true;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("PlayerInteractHitbox"))
			isAttacking = false;
		attackWindUp.SetActive (false);
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
		if (Time.time > nextAttack) {
			nextAttack = Time.time + attackLength;
			if (!firstAttack) { //delays the first attack to avoid instant hit*/
				enemyAttack.PlayDelayed(0.03f);
				attackHitBox.SetActive (true);
				attackWindUp.SetActive (false);
			} else
				attackWindUp.SetActive (true);
			firstAttack = false;
		} else {
			attackHitBox.SetActive(false);
		}
	}

	void CheckBoundaries(){ //Checks wether the character is out of bounds. If it is, prevents it from going further
		enemyRigidBody.position = new Vector2 (
			Mathf.Clamp (enemyRigidBody.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp (enemyRigidBody.position.y, boundary.yMin, boundary.yMax));
	}
}
