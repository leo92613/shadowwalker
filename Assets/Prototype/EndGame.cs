using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {
	public Rotate StartPoint;
	private AudioSource Au;
	// Use this for initialization
	void OnEnable () {
		if (this.GetComponent<AudioSource> ())
			this.GetComponent<AudioSource> ().Play ();
		//Camera.main.gameObject.GetComponent<CameraFollow> ().enabled = false;
		//Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -20.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
		//Camera.main.orthographicSize = Camera.main.orthographicSize + (3.0f - Camera.main.orthographicSize) / 10.0f;
	}
}
