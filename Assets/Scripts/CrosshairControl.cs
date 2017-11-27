using UnityEngine;
using System.Collections;

public class CrosshairControl : MonoBehaviour {
	private Transform crosshairTransform;
	private SpriteRenderer crosshairRenderer;
	public CharacterControl character;
	public Texture2D crossHairAvailable;
	public Texture2D crossHairUnavailable;

	void Start(){ //Starts the cursor confined to the player's view and attaches the available crosshair sprite
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.SetCursor (crossHairAvailable, Vector2.zero, CursorMode.ForceSoftware);
	}

	void Update(){
		if (character.ammo > 0) {
			Cursor.SetCursor(crossHairAvailable,Vector2.zero,CursorMode.Auto);
		} else
			Cursor.SetCursor(crossHairUnavailable,Vector2.zero,CursorMode.ForceSoftware);
	}
		
}
