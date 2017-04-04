using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMaterial : MonoBehaviour {
	private Material idle;

	public Material Active;

	// Use this for initialization
	void Awake () {
		idle = this.GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Renderer> ().material = idle;
	}

	public void Activate(){
		this.GetComponent<Renderer> ().material = Active;
	}
}
