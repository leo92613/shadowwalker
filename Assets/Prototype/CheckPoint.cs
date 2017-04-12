using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Cube {
	private Transform[] children;
	private GameObject quad;
	private bool ishit;

	public bool CloseMap;
	public bool IsHit {
		set {
			ishit = value;
		}
	}
	// Use this for initialization
	void Start () {
		base.Start ();
		quad = GameObject.Find ("Quad");
		children = transform.GetComponentsInChildren<Transform> ();
	}

	void Update()
	{
		if (CloseMap)
			quad.SetActive (false);
	}
	void LateUpdate(){
		
		if (CloseMap) {
			if (ishit)
				quad.SetActive (true);
			ishit = false;
		}
	}

	public void ReturnToThisPoint()
	{
		GameObject.Find("Landy").transform.position = new Vector3(transform.position.x,transform.position.y,GameObject.Find("Landy").transform.position.z);
		foreach (Transform r in children) {
			r.transform.parent = null;
		}
		foreach (Transform r in children) {
			r.GetComponent<Cube>().ResetRotation ();
		}
		foreach (Transform r in children) {
			r.parent = r.GetComponent<Cube>().Parent;
		}
	}


}
