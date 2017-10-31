using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, yMin, yMax;
}

public class CharacterControl : MonoBehaviour {
	public float speed;
	public float projectileSpeed;
	public float jumpSpeed;
	public GameObject projectile;
	public float fireRate;
	public float meleeRate;
	public Boundary boundary;
	public int ammo;
	public int lifePoints;
	public float hitRate;

	private float nextHit;
	private bool inTheAir;
	private float nextFire;
	private float nextMelee;
	private Rigidbody2D playerRigidBody;
	private bool isAttacking;

	void Start(){
		playerRigidBody=this.gameObject.GetComponent<Rigidbody2D>();
		isAttacking = false;
	}

	void Update(){
		if (lifePoints <= 0)
			Destroy (this.gameObject);
		if (playerRigidBody.velocity.y == 0)
			inTheAir = false;
		else
			inTheAir = true;
		float moveVertical = CheckJump();
		Vector2 movement = new Vector2 (playerRigidBody.velocity.x, playerRigidBody.velocity.y+moveVertical);
		playerRigidBody.velocity = movement;
		CheckBoundaries ();
		FireProjectile ();
		MeleeAttack ();
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2 (moveHorizontal * speed, playerRigidBody.velocity.y);
		playerRigidBody.velocity = movement;
		CheckBoundaries ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("EnemyAttack") && Time.time > nextHit) { //Prevents the player from taking a lot of hits at the same time
			nextHit = Time.time + hitRate;
			lifePoints--;
		}
	}
	void CheckBoundaries(){ //Checks wether the character is out of bounds. If it is, prevents it from going further
		playerRigidBody.position = new Vector2 (
			Mathf.Clamp (playerRigidBody.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp (playerRigidBody.position.y, boundary.yMin, boundary.yMax));
	}

	float CheckJump(){ //Checks wether the character has to jump
		if (Input.GetButton("Jump") && !inTheAir) {
			inTheAir = true;
			return jumpSpeed;
		} else
			return 0f;
	}

	void FireProjectile(){ //Fires a projectile if the player has pressed the fire button. If they have, the player shoots in the direction of the crosshair
		if (Input.GetButton ("Fire1") && ammo > 0 && Time.time > nextFire) {//If the button is pressed and rateOfFire time has passed since the last shot
			nextFire = Time.time + fireRate;
			Vector2 target = Camera.main.ScreenToWorldPoint( new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)); //Translates mouse position to world coordinates
			Vector2 myPos = new Vector2(transform.position.x,transform.position.y);
			Vector2 direction = target - myPos;
			direction.Normalize();
			GameObject energyBall = (GameObject)Instantiate( projectile, myPos, Quaternion.identity); //Fires the energyBall with no rotation from the player position
			energyBall.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
			ammo--;
		}
	}

	void MeleeAttack(){
		if (Input.GetButtonDown ("Fire2") && Time.time > nextMelee) {
			nextMelee = Time.time + meleeRate;
			isAttacking = true;
		} else
			isAttacking = false;
	}

	public bool getIsAttacking(){
		return isAttacking;
	}
}
