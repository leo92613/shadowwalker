using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorescript : MonoBehaviour {
    public bool DEBUG = true;

	// Use this for initialization
	void Start () {
        if (DEBUG) Debug.Log("score text start");
	}
	
	// Update is called once per frame
	void Update () {
		//int total;
		//if (GameObject.Find("HitAnchor") != null)
		 //total = GameObject.Find("HitAnchor").GetComponent<Anchar>().PointCount;
//        GetComponent<Text>().text = total + "";
  //      if (DEBUG) Debug.Log("current score " + total);
	}
}
