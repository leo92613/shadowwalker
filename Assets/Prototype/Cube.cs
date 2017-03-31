using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	[SerializeField] protected Transform parent;
	[SerializeField] protected Quaternion originRot;

	public Transform Parent {
		set {
			parent = value;
		}
		get {
			return parent;
		}
	}

	public Material Mat {
		set {
			this.GetComponent<Renderer> ().material = value;
		}
		get {
			return this.GetComponent<Renderer> ().material;
		}
	}

	public void ResetParent(){
		transform.parent = parent;
	}

	// Use this for initialization
	protected void Start () {
		parent = transform.parent;
		originRot = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
