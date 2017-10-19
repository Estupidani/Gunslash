using UnityEngine;
using System.Collections;

public class SwingAnimator : MonoBehaviour {
	public Animator anim;
	public bool isAttacking;

	void Start(){
		isAttacking = false;
	}

	void Update () {
		anim.SetBool("isAttacking",isAttacking);
	}
}
