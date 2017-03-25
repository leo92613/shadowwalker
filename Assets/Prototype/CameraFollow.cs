using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform ch,came;
	public float maxdis;
	private Vector2 cam, cha;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cam = new Vector2 (came.position.x, came.position.y);
		cha = new Vector2 (ch.position.x, ch.position.y);

	}

	void LateUpdate()
	{

		Debug.Log (cam);
		if (Vector2.Distance (cam, cha) > maxdis) {
			//Debug.Log (Vector2.Distance (cam, cha));
			Vector3 movement = new Vector3 ((cha - cam).x, (cha - cam).y, 0);
			came.position = came.position + movement/4.0f;
		}
	}
		
}
