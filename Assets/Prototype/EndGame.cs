using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.gameObject.GetComponent<CameraFollow> ().enabled = false;
		Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -20.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
		Camera.main.orthographicSize = Camera.main.orthographicSize + (3.0f - Camera.main.orthographicSize) / 10.0f;
	}
}
