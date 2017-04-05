using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : Cube {

	private CharMove moveControl;
	private AudioSource au;
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

	public bool Activated {
		set {
			activate = value;
		}
		get {
			return activate;
		}
	}
		

	void Start () {
		base.Start ();
		moveControl = GameObject.Find ("Landy").GetComponent<CharMove> ();
		au = GameObject.Find ("AudioEffect").GetComponent<AudioSource> ();
	}

	void OnEnable(){
		Anchar.TriggeredRotation += StartRotate;
	}

	void OnDisable(){
		Anchar.TriggeredRotation -= StartRotate;
	}

	// Update is called once per frame
	void Update () {
		RotationEnable = false;
		if(activate)
		Mat = idlemateral;

	}

	void LateUpdate(){
		if (RotationEnable) {
			Material tmp = this.GetComponent<Renderer> ().material;
			ShowMaterial[] kids = GetComponentsInChildren<ShowMaterial> ();
			for (int i = 0; i < kids.Length; i++)
				kids [i].Activate ();
			this.GetComponent<Renderer> ().material = tmp;
		}
	}

	private IEnumerator Rot(){
		Vector3 _rot = rot / 20f;
		for (int i = 0; i < 20; i++) {
			yield return new WaitForSeconds (0.06f);
			this.transform.Rotate (_rot);
		}
		moveControl.enabled = true;
		Anchar.Instance.isAbled = true;
	}

	public void StartRotate(){
		if (!RotationEnable) {
			return;
		}
		if (!Activated)
			return;
		au.Play ();
		moveControl.enabled = false;
		Anchar.Instance.isAbled = false;
		StartCoroutine (Rot());
	}

	public void RemoteRotate(){
		if (!Activated)
			return;
		au.Play ();
		moveControl.enabled = false;
		Anchar.Instance.isAbled = false;
		StartCoroutine (Rot());
	}

	public void ResetRotation()
	{
		transform.localRotation = base.originRot;
	}
}
