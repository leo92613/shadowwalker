using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Cube {
	Rotate[] children;
	// Use this for initialization
	void Start () {
		base.Start ();
		children = transform.GetComponentsInChildren<Rotate> ();
	}
	
	public void ReturnToThisPoint()
	{
		GameObject.Find("Landy").transform.position = new Vector3(transform.position.x,transform.position.y,GameObject.Find("Landy").transform.position.z);
		foreach (Rotate r in children)
			r.ResetRotation ();
	}
}
