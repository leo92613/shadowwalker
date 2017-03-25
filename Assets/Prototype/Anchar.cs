using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Anchar : MonoBehaviour
{
	private static Anchar instance = null;

	// Game Instance Singleton
	public static Anchar Instance
	{
		get
		{ 
			return instance; 
		}
	}

	private void Awake()
	{
		// if the singleton hasn't been initialized yet
		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
		}
		instance = this;
		DontDestroyOnLoad( this.gameObject );
	}




	public delegate void TriggerRotation();
	public static event TriggerRotation TriggeredRotation;
	public bool isAbled = true;
	public Material anchorhit;
	private Ray ray;
	private RaycastHit hit;
	private bool isPressed;
	private GameObject hitobject;
	// Use this for initialization
	void Start ()
	{
		
	}

	void Update ()
	{
		isPressed = false;
		hitobject = null;
		ray = new Ray (transform.position, Vector3.forward);
		if (Physics.Raycast (ray, out hit)) {
			hitobject = hit.transform.gameObject;
		}
		if (Input.GetKeyDown (KeyCode.Space))
			isPressed = true;
	}
	// Update is called once per frame
	void LateUpdate ()
	{
		if (hitobject != null) {
			if (hitobject.GetComponent<ChangeCameraSize> ())
				hitobject.GetComponent<ChangeCameraSize> ().enabled = true;
			if (hitobject.GetComponent<EndGame> ())
				hitobject.GetComponent<EndGame> ().enabled = true;
			if (hitobject.GetComponent<Rotate> ()) {
				hitobject.GetComponent<Rotate> ().RotationEnable = true;
				hitobject.GetComponent<MeshRenderer> ().material = anchorhit;
				if (isPressed) {
					isPressed = false;
					onRotate ();
				}
			}
		}
	}

	void onRotate(){
		if (isAbled)
		TriggeredRotation ();
	}
}
