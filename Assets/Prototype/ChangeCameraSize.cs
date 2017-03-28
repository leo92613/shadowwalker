using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSize : MonoBehaviour {
	public float size;

	public Rotate StartPoint;
	// Use this for initialization
	void Start () {
		StartPoint.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.orthographicSize = Camera.main.orthographicSize + (size - Camera.main.orthographicSize) / 10.0f;
		if (Mathf.Abs (Camera.main.orthographicSize - size) < 0.05f)
			this.GetComponent<ChangeCameraSize> ().enabled = false;
	}
}
