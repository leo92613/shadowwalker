using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public bool DEBUG = true;

    public GameObject panel;
    private bool menuActive = false;

    // public GameObject soundPanel;

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
            bringUpMenu();
            if (DEBUG) Debug.Log("escape!");
        }
    }

    // toggle menu visible or not
    public void bringUpMenu()
    {
        if (DEBUG) Debug.Log("menu toggle");
        menuActive = !menuActive;
        // freeze background things
        GameObject.Find("Landy").GetComponent<CharMove>().freezeScreen(menuActive);
        Camera.main.GetComponent<CameraFollow>().enableZoom(!menuActive);

        // bring up main menu panel
        panel.SetActive(menuActive);
    }

    public void testButton()
    {
        Debug.Log("button clicked");
    }

    // toggle sound menu on and main menu off
    public void showSubPanel(GameObject o)
    {
        panel.SetActive(false);
        o.SetActive(true);
    }

    // return to main menu panel
    public void goBack(GameObject o)
    {
        o.SetActive(false);
        panel.SetActive(true);
    }

    public void onValueChanged(float value)
    {
        if (DEBUG) Debug.Log("slider " + value);
    }

    public void soundSlider(float value)
    {
        if (DEBUG) Debug.Log("sound " + value);
        GameObject.Find("AudioEffect").GetComponent<AudioSource>().volume = value;
    }
}
