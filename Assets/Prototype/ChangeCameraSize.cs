using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSize : Cube {
	public float size;
	public Rotate StartPoint;
	private bool changeCamera;

	public bool ChangeCamera {
		set {
			changeCamera = value;
		}
	}
	// Use this for initialization
	void Start(){
		base.Start ();
		changeCamera = false;
		StartPoint.Activated = false;
	}
		
	
	// Update is called once per frame
	void Update () {
		if (changeCamera) {
			StartPoint.Activated = true;
			Camera.main.orthographicSize = Camera.main.orthographicSize + (size - Camera.main.orthographicSize) / 10.0f;
		}
		if (Mathf.Abs (Camera.main.orthographicSize - size) < 0.05f)
			changeCamera = false;
	}
}
