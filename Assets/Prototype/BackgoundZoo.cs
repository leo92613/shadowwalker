using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgoundZoo : MonoBehaviour {
	Vector3 OriginSize;
	float OriginCameraSize;

	// Use this for initialization
	void Start () {
		OriginSize = transform.localScale;
		OriginCameraSize = Camera.main.orthographicSize;
		
	}
	
	// Update is called once per frame
	void Update () {
		float ratio = Camera.main.orthographicSize / OriginCameraSize;
		transform.localScale = OriginSize * ratio;
	}
}
