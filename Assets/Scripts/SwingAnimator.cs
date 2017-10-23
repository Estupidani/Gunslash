using UnityEngine;
using System.Collections;

public class SwingAnimator : MonoBehaviour {
	public Animator anim;
	public float animationLength;
	public bool isAttacking;

	private float nextAnimation;

	void Start(){
		isAttacking = false;
	}

	void Update () {
		if (Time.time > nextAnimation) {
			anim.SetBool ("isAttacking", isAttacking);
			nextAnimation = Time.time + animationLength;
		}
	}
}
