using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : Cube {
	public Rotate StartPoint;
	private AudioSource Au;
	// Use this for initialization
	void OnEnable () {
		if (this.GetComponent<AudioSource> ())
			this.GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
