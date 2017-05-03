using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : Cube {

	private CharMove moveControl;
	private AudioSource au;
	private bool rotationenable;
	private bool activate = true;
	private int rotateTimes = 0;
	private List<CheckPoint> cps = new List<CheckPoint>();
	private List<int> cpc = new List<int> ();
	private int cpcount = 0;
	private GameObject undoeffect;

	public Vector3 rot;
	public Material idlemateral;
	public bool isWorld = true;
	public bool isMenu = false;
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
		cpc.Add (0);
		base.Start ();
		//moveControl = GameObject.Find ("Landy").GetComponent<CharMove> ();
		moveControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<CharMove>();
		au = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource> ();
		undoeffect = GameObject.Find ("UndoEffect");
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
			yield return new WaitForSeconds (0.02f);
			if (isWorld)
				this.transform.Rotate (_rot, relativeTo: Space.World);
			else
				this.transform.Rotate (_rot);
		}
		moveControl.enabled = true;
		Anchar.Instance.isAbled = true;
		if (isMenu)
			this.gameObject.GetComponent<EndGame> ().NextLevel();
	}

	public void StartRotate(){
		if (!RotationEnable) {
			return;
		}
		if (!Activated)
			return;
		Debug.Log (au.pitch);
		au.pitch = 1.24f + Random.Range (-0.1f, 0.2f);
		au.Play ();
		moveControl.enabled = false;
		Anchar.Instance.isAbled = false;
		StartCoroutine (Rot());
		rotateTimes++;
	}

	public void UndoRotate(){
		if (rotateTimes > 0) {
			rotateTimes--;
			this.transform.Rotate (-rot, relativeTo: Space.World);
		}
		UndoCP ();
		GameObject.FindGameObjectWithTag ("GameController").transform.position = new Vector3 (transform.position.x, transform.position.y, -15f);
		undoeffect.transform.position = new Vector3 (transform.position.x, transform.position.y, -5f);
		undoeffect.GetComponent<ParticleSystem> ().Play ();
		undoeffect.GetComponent<AudioSource> ().Play ();
	}

	public void RemoteRotate(){
		au.Play ();
		moveControl.enabled = false;
		Anchar.Instance.isAbled = false;
		StartCoroutine (Rot());
	}

	public void AddCP(CheckPoint c){
		cps.Add (c);
		if (cpc.Count == 0)
			cpc.Add (1);
		else
			cpc [cpc.Count - 1]++;
		Debug.Log (cpc [cpc.Count - 1]);
	}

	public void AddCPC(){
		cpc.Add (0);
		//Debug.Log (cpc.Count);
	}

	private void UndoCP()
	{
		
		int count = 0;
		Debug.Log (" CPcount count: "+cpc.Count);
		if (cpc.Count > 0) {
			count = cpc [cpc.Count - 1];
			cpc.RemoveAt (cpc.Count - 1);
		}
		//Debug.Log (name);
		Debug.Log ("cp count: "+cps.Count);
		Debug.Log (" count: "+count);

		for (int i = 0; i < count; i++) {
			cps [cps.Count - 1].IsHit = false;
			Anchar.Instance.RemoveCheckpoint (cps [cps.Count - 1]);
			cps.RemoveAt (cps.Count - 1);
		}
	}


}
