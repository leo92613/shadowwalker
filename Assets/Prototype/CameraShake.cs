using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	private Camera  cam; // set this via inspector
	private float shake  = 0f;
	private float shakeAmount = 0.1f;
	private float decreaseFactor = 1.0f;
	private Vector3 origin_Pos;
	private Vector2 shake_Trans;


	// Use this for initialization
	void Start () {
		cam = Camera.main;
		shake = 0.0f;
		origin_Pos = cam.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (shake > 0) {
			//camera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
			shake_Trans = Random.insideUnitCircle * shakeAmount;
			cam.transform.localPosition = new Vector3 (origin_Pos.x + shake_Trans.x, origin_Pos.y + shake_Trans.y, origin_Pos.z);
			shake -= Time.deltaTime * decreaseFactor;

		} else {
			shake = 0.0f;
			cam.transform.localPosition = origin_Pos;
		}
	}

	public void StartShake(){
		shake = 1.6f;
	}
}
