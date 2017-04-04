using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : Cube {
	public int SceneIndex;


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
			this.GetComponent<ShowMaterial> ().Activate ();
			play ();
			if(Input.GetKeyDown(KeyCode.Space)) 
				SceneManager.LoadScene(SceneIndex);
			end = false;
		}
			
	}
}
