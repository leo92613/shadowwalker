using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Anchar : MonoBehaviour
{
	private static Anchar instance = null;
	private List<CheckPoint> CheckPoints = new List<CheckPoint>();
	private List<Rotate> UndoList = new List<Rotate> ();
	private Transform Landy;
	//private Rotate lastRotate;
	public int CheckPointsCount;


	public int PointCount {
		get {
			return CheckPoints.Count;
		}
	}

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
		CheckPointsCount = GameObject.FindObjectsOfType<CheckPoint> ().Length;
	}


	public delegate void TriggerRotation();
	public static event TriggerRotation TriggeredRotation;
	public bool isAbled = true;
	public Material anchorhit;
	private int MenuScene = 10;

	private Ray ray;
	private RaycastHit hit;
	private bool isPressed;
	private GameObject hitobject;
	private Rotate lastRotate;
	// Use this for initialization
	void Start ()
	{
		CheckPoints = new List<CheckPoint>();
		Landy = GameObject.Find ("Landy").transform;

	}

	public void SetCheckPoint()
	{
		CheckPointsCount = GameObject.FindObjectsOfType<CheckPoint> ().Length;

	}

	void Update ()
	{
		// Debug.Log (CheckPoints.Count);
		if (CheckPoints.Count == CheckPointsCount)
			this.GetComponent<EndGame> ().NextLevel ();
		isPressed = false;
		hitobject = null;
		ray = new Ray (Landy.position, Vector3.forward);
		if (Physics.Raycast (ray, out hit,100f,LayerMask.GetMask("Default"))) {
			hitobject = hit.transform.gameObject;
		}
		if (Input.GetKeyDown (KeyCode.Space))
			isPressed = true;
		//if (Input.GetKeyDown(KeyCode.P))
		//	CheckPoints[CheckPoints.Count - 1].ReturnToThisPoint();
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		if (Input.GetKeyDown (KeyCode.U)) {
			if (UndoList.Count > 0) {
				UndoList [UndoList.Count - 1].UndoRotate ();
				UndoList.RemoveAt (UndoList.Count - 1);
			}
		}

		if (Input.GetKeyDown (KeyCode.M)) {
			SceneManager.LoadScene(MenuScene);
		}
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
					UndoList.Add (hitobject.GetComponent<Rotate> ());
					if (lastRotate != hitobject.GetComponent<Rotate> ()) {
						lastRotate = hitobject.GetComponent<Rotate> ();
						lastRotate.AddCPC ();
					}
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

	public void RecordCheckpoint(CheckPoint c){
		if (lastRotate != null)
			lastRotate.AddCP (c);
	}

	public void RemoveCheckpoint(CheckPoint c){
		CheckPoints.Remove (c);
	}
}
