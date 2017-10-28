using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	public CharacterControl character;

	private float meleeRate;
	private float nextMelee;
	private Vector3 meleeScale;

	void Start(){
		this.gameObject.transform.localScale = Vector3.zero;
		meleeRate = character.meleeRate;
	}

	void Update(){
		if (character.getIsAttacking () && Time.time > nextMelee) {
			nextMelee = Time.time + meleeRate;
			this.gameObject.transform.localScale = new Vector3 (1f, 0.32f, 1f);
			this.gameObject.transform.localPosition = new Vector3 (-4.2f, 0.1f, 0f);
		} else if (Time.time >= nextMelee) {
			this.gameObject.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
			this.gameObject.transform.localPosition = Vector3.zero;
		}
	}
}
