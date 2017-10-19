using UnityEngine;
using System.Collections;

public class SwingRotator : MonoBehaviour {

	void Update () 
	{
		Vector2 swingPosition = Camera.main.WorldToViewportPoint (transform.position);
		Vector2 mousePosition = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
		float angle = AngleBetweenTwoPoints(swingPosition, mousePosition);
		transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}
}
