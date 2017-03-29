using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
	private Ray ray;
	private RaycastHit hit;
	private SpriteRenderer sprite;
	private Animator ani;
	private bool isPressed;


	public Transform anchor;
	public Sprite idleSprite;
	// Use this for initialization
	void Start ()
	{
		sprite = GetComponent<Renderer> () as SpriteRenderer;
		ani = this.GetComponent<Animator> ();
		isPressed = false;
	    ani.enabled = true;
	}

    // Update is called once per frame
    void Update()
    {
        ani.enabled = true;
        isPressed = false;
        anchor.localPosition = new Vector3();
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s")) {
            isPressed = true;
            anchor.localPosition = new Vector3(0, -0.05f, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w")) {
			isPressed = true;
			anchor.localPosition = new Vector3 (0, 0.05f, 0);
		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey("a")) {
			anchor.localPosition = new Vector3 (-0.05f, 0, 0);
			isPressed = true;
			sprite.flipX = true;
		}
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey("d")) {
			anchor.localPosition = new Vector3 (0.05f, 0, 0);
			isPressed = true;
			sprite.flipX = false;
		}
		ray = new Ray (anchor.position, Vector3.forward);
		if (Physics.Raycast (ray, out hit)) {
			this.transform.position += anchor.localPosition ;
		}
		sprite.sprite = idleSprite;
		if (!isPressed)
			ani.enabled = false;

	}
}
