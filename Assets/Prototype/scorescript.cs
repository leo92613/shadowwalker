using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scorescript : MonoBehaviour
{
    public bool DEBUG = true;

    // Use this for initialization
    void Start()
    {
        if (DEBUG) Debug.Log("score text start");
    }

    // Update is called once per frame
    void Update()
    {
        int total = GameObject.Find("HitAnchor").GetComponent<Anchar>().CheckPointsCount;
		int numCheckpoints = GameObject.Find("HitAnchor").GetComponent<Anchar>().PointCount;
        if (GameObject.Find("HitAnchor") != null)
        {
            if (DEBUG) Debug.Log("hello");
			GetComponent<TMP_Text>().text = "LIGHTS: " + (numCheckpoints ) + "/" + (total );
            if (DEBUG) Debug.Log("current score " + total);
        }
    }
}