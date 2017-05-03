using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {
	public AudioClip[] ac;

	// Use this for initialization
	void OnEnable () {
		int index = Random.Range (0, ac.Length);
		this.GetComponent<AudioSource> ().clip = ac [index];
		this.GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
