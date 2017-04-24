using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public bool DEBUG = true;

    public GameObject panel;
    private bool menuActive = false;

    // Use this for initialization
    void Start()
    {
        panel.SetActive(menuActive);
    }

    // Update is called once per frame
    void Update()
    {
        // bring up menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuActive = !menuActive;
            if (DEBUG) Debug.Log("escape!");
            GameObject.Find("Landy").GetComponent<CharMove>().freezeScreen(menuActive);
            panel.SetActive(menuActive);
        }
    }
}
