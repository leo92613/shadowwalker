using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	public Vector3 rot;
	public bool RotationEnable;
	// Use this for initialization
	public Material idlemateral;
	private CharMove moveControl;
	//public Anchar an;
	private AudioSource au;
	private CameraShake shake;
	private Quaternion OriginRotation;
	void Start () {
		moveControl = GameObject.Find ("Landy").GetComponent<CharMove> ();
		au = GameObject.Find ("AudioEffect").GetComponent<AudioSource> ();
		shake = GameObject.Find ("Main Camera").GetComponent<CameraShake> ();
		OriginRotation = transform.localRotation;
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
		this.GetComponent<MeshRenderer> ().material = idlemateral;
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
		if (!RotationEnable)
			return;
		au.Play ();
		shake.StartShake ();
		moveControl.enabled = false;
		Anchar.Instance.isAbled = false;
		StartCoroutine (Rot());
	}

	public void ResetRotation()
	{
		transform.localRotation = OriginRotation;
	}
}
