using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Anchar : MonoBehaviour
{
	private static Anchar instance = null;
	private List<CheckPoint> CheckPoints;
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

		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
		}
		instance = this;
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
		CheckPoints = new List<CheckPoint>();
	}

	void Update ()
	{
		isPressed = false;
		hitobject = null;
		ray = new Ray (transform.position, Vector3.forward);
		if (Physics.Raycast (ray, out hit,100f,LayerMask.GetMask("Default"))) {
			hitobject = hit.transform.gameObject;
		}
		if (Input.GetKeyDown (KeyCode.Space))
			isPressed = true;
		if (Input.GetKeyDown(KeyCode.P))
			CheckPoints[CheckPoints.Count - 1].ReturnToThisPoint();
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	// Update is called once per frame
	void LateUpdate ()
	{
		if (hitobject != null) {
			

			if (hitobject.GetComponent<Rotate> () && hitobject.GetComponent<Rotate>().Activated) {
				hitobject.GetComponent<Rotate> ().RotationEnable = true;
				hitobject.GetComponent<Rotate> ().Mat = anchorhit;
				if (isPressed) {
					isPressed = false;
					onRotate ();
				}
			}

			if (hitobject.GetComponent<TransProjection> () && hitobject.GetComponent<TransProjection>().enabled) {
				hitobject.GetComponent<TransProjection> ().RotationEnable = true;
				hitobject.GetComponent<TransProjection> ().Mat = anchorhit;
				if (isPressed) {
					isPressed = false;
					onRotate ();
				}
			}

			if (hitobject.GetComponent<TransProjectTionPartly> () && hitobject.GetComponent<TransProjectTionPartly>().enabled) {
				hitobject.GetComponent<TransProjectTionPartly> ().RotationEnable = true;
				hitobject.GetComponent<TransProjectTionPartly> ().Mat = anchorhit;
				if (isPressed) {
					isPressed = false;
					Debug.Log("TransProjectionPartly");
					onRotate ();

				}
			}

			if (hitobject.GetComponent<Remote> () && hitobject.GetComponent<Remote>().enabled) {
				hitobject.GetComponent<Remote> ().RotationEnable = true;
				hitobject.GetComponent<Remote> ().Mat = anchorhit;
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

	public void AddCheckpoint(CheckPoint c){
		if (!CheckPoints.Contains (c))
			CheckPoints.Add (c);
	}
}
