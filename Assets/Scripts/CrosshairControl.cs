using UnityEngine;
using System.Collections;

public class CrosshairControl : MonoBehaviour {
	private Transform crosshairTransform;
	private SpriteRenderer crosshairRenderer;
	public Sprite availableCrosshair;
	public Sprite unavailableCrosshair;
	public CharacterControl character;

	void Start(){ //Starts the cursor confined to the player's view and attaches the available crosshair sprite
		Cursor.lockState = CursorLockMode.Confined;
		crosshairTransform = this.gameObject.transform;
		crosshairRenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		crosshairRenderer.sprite = availableCrosshair;
	}

	void Update(){
		if (character.ammo > 0) {
			crosshairRenderer.sprite = availableCrosshair;
		} else
			crosshairRenderer.sprite = unavailableCrosshair;
	}

	void OnGUI()
	{
		Vector3 p = new Vector3 ();
		Camera c = Camera.main;
		Event e = Event.current;
		Vector2 mousePos = new Vector2 ();

		// Get the mouse position from Event.
		// Note that the y position from Event is inverted.
		mousePos.x = e.mousePosition.x;
		mousePos.y = c.pixelHeight - e.mousePosition.y;

		p = c.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, c.nearClipPlane));
		crosshairTransform.position = p;
	}

}
