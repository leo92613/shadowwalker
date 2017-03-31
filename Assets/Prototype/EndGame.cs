using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : Cube {
	public Rotate StartPoint;


	private AudioSource Au;
	private bool end;

	public bool End {
		set {
			end = value;
		}
	}

	void Start(){
		base.Start ();
	}
	void play () {
		if (this.GetComponent<AudioSource> ())
			this.GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (end) {
			play ();
			end = false;
		}
	}
}
