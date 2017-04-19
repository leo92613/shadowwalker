using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private bool DEBUG = false;

    public Transform ch, came;
    public float maxdis;
    private Vector2 cam, cha;

    private float max_zoom = 15;
    private float min_zoom = 5;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cam = new Vector2(came.position.x, came.position.y);
        cha = new Vector2(ch.position.x, ch.position.y);
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        // Debug.Log(zoom);
        if (zoom != 0)
        {
            changeZoom(zoom);
        }
    }

    void LateUpdate()
    {

        if (Vector2.Distance(cam, cha) > maxdis)
        {
            //Debug.Log (Vector2.Distance (cam, cha));
            Vector3 movement = new Vector3((cha - cam).x, (cha - cam).y, 0);
            came.position = came.position + movement / 4.0f;
        }
    }

    void changeZoom(float zoom)
    {
        float currentSize = Camera.main.orthographicSize;
        if (DEBUG) Debug.Log(currentSize);
        zoom = zoom * -1;
        if (zoom > 0f && currentSize < max_zoom) // zoom in
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + zoom * 5f;
            if (DEBUG) Debug.Log("out");
        }

        else if (zoom < 0f && currentSize > min_zoom) // zoom out
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + zoom * 5f;
            if (DEBUG) Debug.Log("in");
        }
    
    }
}
