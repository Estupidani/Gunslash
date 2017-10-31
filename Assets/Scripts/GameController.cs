using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public int framesFrozen;
	public CharacterControl character;
	public Text shots;
	public Text lifePoints;
	public float spawnWait;
	public int enemyCount;
	public Vector2 spawnValues;
	public GameObject enemy;

	private bool gameOver;
	private int freezeFrame;

	private static bool enemyType;

	void Start () {
		freezeFrame = framesFrozen;
		StartCoroutine( SpawnWaves ());
		shots.color = Color.green;
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
		if (character.lifePoints == 0)
			gameOver = true;
	}
		
	public void FreezeFrame(){
		freezeFrame = 0;
	}

	public static bool EnemyType(){ //Determines wether the enemy will start its movement going to the left or to the right
		enemyType = !enemyType;
		return enemyType;
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (spawnWait);
		while (!gameOver) {
			for (int i = 0; i < enemyCount; i++) {
				if (i > 10)
					lifePoints.text = "PUES SI ABER QUE PASA";
				Vector2 spawnPosition = new Vector2 (spawnValues.x, spawnValues.y);
				Instantiate (enemy, spawnPosition, Quaternion.identity);
			}
			yield return new WaitForSeconds (spawnWait);
		}
	}
}
