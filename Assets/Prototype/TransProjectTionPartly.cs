using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransProjectTionPartly : Cube {
	public Transform Root;
	public Vector3 Rot;


	private CharMove moveControl;
	private AudioSource au;
	private bool rotationenable;
	private Transform[] AllCubes;
	public Material idlemateral;


	public bool RotationEnable {
		set {
			rotationenable = value;
		}
		get {
			return rotationenable;
		}
	}

	void Start ()
	{
		base.Start ();
		moveControl = GameObject.Find ("Landy").GetComponent<CharMove> ();
		au = GameObject.Find ("AudioEffect").GetComponent<AudioSource> ();
	}

	void OnEnable ()
	{
		AllCubes = Root.GetComponentsInChildren<Transform> ();
		Anchar.TriggeredRotation += StartRotate;
	}

	void OnDisable ()
	{
		Anchar.TriggeredRotation -= StartRotate;
	}
	
	void Update ()
	{
		RotationEnable = false;
		Mat = idlemateral;
	}

	private IEnumerator Trans ()
	{
		Vector3 _rot = Rot / 20f;
		for (int i = 0; i < 20; i++) {
			yield return new WaitForSeconds (0.06f);
			this.transform.Rotate (_rot);
		}
		moveControl.enabled = true;
		Anchar.Instance.isAbled = true;
		ResetParent ();
			Rot = -Rot;
	}

	private void SetParent ()
	{
		this.transform.parent = null;
		foreach (Transform c in AllCubes)
			c.parent = transform;
		Root.parent = Root.GetComponent<CheckPoint> ().Parent;
	}

	private void ResetParent ()
	{

		foreach (Transform c in AllCubes)
			c.GetComponent<Cube>().ResetParent ();
		this.transform.parent = parent;
	}

	public void StartRotate ()
	{
		if (!RotationEnable)
			return;
		SetParent ();
		au.Play ();
		moveControl.enabled = false;
		Anchar.Instance.isAbled = false;

		StartCoroutine (Trans ());

	}




}
