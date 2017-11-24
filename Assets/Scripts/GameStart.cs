using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	private GameController controller;

	// Use this for initialization
	void Start () {
		GameObject controllerGameObject = GameObject.FindWithTag ("GameController");
		controller = controllerGameObject.GetComponent<GameController>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Projectile")){
			controller.startGame();
			Destroy (other.gameObject);
			Destroy(this.gameObject);
		}
	}
}
