using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : Cube {
	public int SceneIndex;
	public bool isRandom = false;

	private AudioSource Au;

	private CharMove moveControl;
	private bool end;
	public bool menu = false;

	public bool End {
		set {
			end = value;
		}
	}

	void Start(){
		base.Start ();
		moveControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<CharMove>();
	}
	void play () {
		if (this.GetComponent<AudioSource> ())
			this.GetComponent<AudioSource> ().Play ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N))
			NextLevel ();
//		if (end) {
//			this.GetComponent<ShowMaterial> ().Activate ();
//			play ();
//			if(Input.GetKeyDown(KeyCode.Space)) 
//				SceneManager.LoadScene(SceneIndex);
//			end = false;
//		}			
	}

	public void NextLevel()
	{
		if (isRandom)
			return;
		moveControl.enabled = false;
		if (menu)
			SceneManager.LoadScene(SceneIndex);
		StartCoroutine (EndG ());
	}

	private IEnumerator EndG()
	{
		for (int i = 0; i < 30; i++)
		{
			//Camera.main.orthographicSize = Camera.main.orthographicSize + -0.08f * 5f;
			Camera.main.transform.Rotate(0,0,2f,relativeTo:Space.World);
			yield return new WaitForSeconds(0.02f);
		}
		moveControl.enabled = true;
		SceneManager.LoadScene(SceneIndex);
	}
}
