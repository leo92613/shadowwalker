using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : Cube {

	public Rotate RotCube;

	private bool rotationenable;
	private bool activate = true;

	public Vector3 rot;
	public Material idlemateral;

	public bool RotationEnable {
		set {
			rotationenable = value;
		}
		get {
			return rotationenable;
		}
	}


	// Use this for initialization
	void Start () {
		base.Start ();
	}

	void OnEnable(){
		Anchar.TriggeredRotation += StartRotate;
	}

	void OnDisable(){
		Anchar.TriggeredRotation -= StartRotate;
	}

	public void StartRotate(){
		if(RotationEnable)
		RotCube.RemoteRotate ();
	}

	public void ResetRotation()
	{
		transform.localRotation = base.originRot;
	}

	void Update(){
		RotationEnable = false;
	}
	// Update is called once per frame
	void LateUpdate () {
		if (RotationEnable)
			RotCube.RotationEnable = true;
	}
}
