using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public int framesFrozen;
	public CharacterControl character;
	public Text shots;
	public Text lifePoints;

	private int freezeFrame;

	private static bool enemyType;

	void Start () {
		freezeFrame = framesFrozen;
		shots.text = "AMMO:" + character.ammo.ToString();
		shots.color = Color.green;
		//lifePoints.text = "LIFE:" + character.lifePoints.ToString();
		enemyType = false;
	}

	void Update () {
		shots.text = "AMMO:" + character.ammo.ToString();
		lifePoints.text = "LIFE:" + character.lifePoints.ToString();
		if (character.ammo == 0)
			shots.color = Color.red;
		else
			shots.color = Color.green;
		Time.timeScale = 1f;
		if (freezeFrame < framesFrozen) {
			Time.timeScale = 0.01f;
			freezeFrame++;
		} else
			Time.timeScale = 1f;
	}

	public void FreezeFrame(){
		freezeFrame = 0;
	}

	public static bool EnemyType(){ //Determines wether the enemy will start its movement going to the left or to the right
		enemyType = !enemyType;
		return enemyType;
	}
}
