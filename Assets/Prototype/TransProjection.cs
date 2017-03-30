﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransProjection : Cube {
	[SerializeField] private Vector3 Rot;
	// Use this for initialization
	private CharMove moveControl;
	private AudioSource au;
	private CameraShake shake;
	private bool rotationenable;
	private Cube[] AllCubes;

	public Material idlemateral;


	public bool RotationEnable {
		set {
			rotationenable = value;
		}
		get {
			return rotationenable;
		}
	}


	void Start () {
		moveControl = GameObject.Find ("Landy").GetComponent<CharMove> ();
		au = GameObject.Find ("AudioEffect").GetComponent<AudioSource> ();
		shake = GameObject.Find ("Main Camera").GetComponent<CameraShake> ();
	}

	void OnEnable(){
		AllCubes = GameObject.FindObjectsOfType<Cube> ();
		Anchar.TriggeredRotation += StartRotate;
	}

	void OnDisable(){
		Anchar.TriggeredRotation -= StartRotate;
	}

	// Update is called once per frame
	void Update () {
		RotationEnable = false;
		Mat = idlemateral;
	}

	private IEnumerator Trans(){
		Vector3 _rot = Rot / 20f;
		for (int i = 0; i < 20; i++) {
			yield return new WaitForSeconds (0.06f);
			this.transform.Rotate (_rot);
		}
		moveControl.enabled = true;
		Anchar.Instance.isAbled = true;
		ResetParent ();
	}

	private void SetParent(){
		foreach (Cube c in AllCubes)
			c.transform.parent = transform;
	}

	private void ResetParent(){
		foreach (Cube c in AllCubes)
			c.ResetParent ();
	}

	public void StartRotate(){
		if (!RotationEnable)
			return;
		SetParent ();
		au.Play ();
		shake.StartShake ();
		moveControl.enabled = false;
		Anchar.Instance.isAbled = false;

		StartCoroutine (Trans());

	}



	public void ResetRotation()
	{
		transform.localRotation = base.originRot;
	}

}