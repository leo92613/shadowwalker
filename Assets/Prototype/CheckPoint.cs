using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Cube {
	Transform[] children;
	// Use this for initialization
	void Start () {
		base.Start ();
		children = transform.GetComponentsInChildren<Transform> ();
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
