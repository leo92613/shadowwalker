using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : Cube {
	public int SceneIndex;


	private AudioSource Au;

	private CharMove moveControl;
	private bool end;

	public bool End {
		set {
			end = value;
		}
	}

	void Start(){
		base.Start ();
		moveControl = GameObject.Find ("Landy").GetComponent<CharMove> ();
	}
	void play () {
		if (this.GetComponent<AudioSource> ())
			this.GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
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
		moveControl.enabled = false;
		StartCoroutine (EndG ());
	}

	private IEnumerator EndG()
	{
		for (int i = 0; i < 10; i++)
		{
			Camera.main.orthographicSize = Camera.main.orthographicSize + -0.08f * 5f;
			yield return new WaitForSeconds(0.02f);
		}
		moveControl.enabled = true;
		SceneManager.LoadScene(SceneIndex);
	}
}
