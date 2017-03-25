using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSize : MonoBehaviour {
	public float size;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.orthographicSize = Camera.main.orthographicSize + (size - Camera.main.orthographicSize) / 10.0f;
		if (Mathf.Abs (Camera.main.orthographicSize - size) < 0.05f)
			this.GetComponent<ChangeCameraSize> ().enabled = false;
	}
}
