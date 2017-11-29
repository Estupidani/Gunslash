using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public int framesFrozen;
	public CharacterControl character;
	public Text shots;
	public Text lifePoints;
	public Text restartText;
	public Text roundText;
	public float spawnWait;
	public int enemyCount;
	public Vector2 spawnValues;
	public GameObject enemy;
	public AudioClip introClip;
	public AudioClip combatClip;
	public bool isPlaying;

	private bool gameOver;
	private int roundNumber;
	private int freezeFrame;
	private float waveWait;
	private AudioSource music;
	private GameObject[] enemiesOnScreen;
	private bool restart;

	private static bool enemyType;

	void Start () {
		music = this.GetComponent<AudioSource> ();
		music.clip = introClip;
		music.loop = true;
		music.Play ();
		freezeFrame = framesFrozen;
		waveWait = 0.5f;
		roundNumber = 0;
		shots.color = Color.green;
		enemyType = false;
		isPlaying = false;
		restart = false;
		restartText.text = "";
		roundText.text = "ROUND";
		roundNumber = 0;
	}

	void Update () {
		if (gameOver) {
			restartText.text = "Press R to restart or ESC to exit";
			restart = true;
		}
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R))
				Application.LoadLevel (Application.loadedLevel);
			else if (Input.GetKeyDown (KeyCode.Escape))
				Application.Quit ();
		}
			
		enemiesOnScreen = GameObject.FindGameObjectsWithTag ("Enemy");
		if (isPlaying)
			shots.text = "AMMO:" + character.ammo.ToString ();
		else
			shots.text = "AMMO:-";
		lifePoints.text = "LIFE:" + character.lifePoints.ToString();
		if (character.ammo == 0)
			shots.color = Color.red;
		else
			shots.color = Color.green;
		Time.timeScale = 1f;
		if (freezeFrame < framesFrozen) {
			Time.timeScale = 0.3f;
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

	public void startGame(){
		music.clip = combatClip;
		music.Play ();
		StartCoroutine( SpawnWaves ());		
	}

	IEnumerator SpawnWaves(){
		isPlaying = true;
		character.ammo = 0;
		while (!gameOver) {
			roundNumber += 1;
			roundText.text = roundNumber.ToString ();
			for (int i = 0; i < enemyCount; i++) {
				Vector2 spawnPosition = new Vector2 (spawnValues.x, spawnValues.y);
				Instantiate (enemy, spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (waveWait);
			}
			yield return new WaitUntil( ()=>enemiesOnScreen.Length==0);
			yield return new WaitForSeconds (spawnWait);
			enemyCount += (int) Mathf.Ceil (enemyCount * 0.2f);
		}
	}
}
