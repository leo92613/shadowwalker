using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
	[SerializeField] protected Transform parent;
	[SerializeField] protected Quaternion originRot;
	protected Vector3 originPos;

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

	public void ResetParent ()
	{
		transform.parent = parent;
	}

	// Use this for initialization
	protected void Start ()
	{
		parent = transform.parent;
		transform.parent = null;
		originPos = transform.position;
		//originRot = transform.localRotation;
		originRot = transform.rotation;
		transform.parent = parent;
	}

	public void ResetRotation ()
	{
		transform.position = originPos;
		transform.rotation = originRot;
		print ("Reset");
	}
}
