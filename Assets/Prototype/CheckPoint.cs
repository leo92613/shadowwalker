using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Cube {
	private Transform[] children;
	private GameObject quad;
	private bool ishit;

    private Material pastMaterial;

	public bool IsHit {
		set {
			ishit = value;
		}
		get{
			return ishit;
		}
	}
    
	// Use this for initialization
	void Start () {
		base.Start ();
		quad = GameObject.Find ("Quad");
		children = transform.GetComponentsInChildren<Transform> ();
        pastMaterial = Resources.Load("Materials/OpenChest", typeof(Material)) as Material;
	}

	void Update()
	{
    }
    void LateUpdate(){
        if (GetComponent<Renderer>().material.name != "PassCheckpoint" && ishit)
        {
            GetComponent<Renderer>().material = pastMaterial;
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
