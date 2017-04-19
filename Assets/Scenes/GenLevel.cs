using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenLevel : MonoBehaviour {
	public GameObject L;
	public GameObject Check;
	public GameObject Cube;
	public GameObject RotCube;

	private List<GameObject> cubes;
	// Use this for initialization
	void Start () {
		cubes = new List<GameObject> ();
		GameObject TmpCheck;
		GameObject TmpCube;
		GameObject TmpRot;
		//Instantiate(L, new Vector3(0, 0, -15), Quaternion.identity);
		int lastDirection = 0;
		int x = 0;
		int y = 0;
		int z = 0;
		int dir = 0;
		int chck = 1;

		TmpRot = Instantiate (Check, new Vector3 (x, y, z), Quaternion.identity);
		for (int i = 0; i<10; ++i) {
			int num = Random.Range (2, 6);
			while (lastDirection == dir%3) {
				dir = Random.Range (1, 7);
			}
			if (chck % 2 == 0) {
				TmpCheck = Instantiate (Check, new Vector3 (x, y, z), Quaternion.identity);
			} else {
				TmpCheck = Instantiate (RotCube, new Vector3 (x, y, z), Quaternion.identity);
				TmpCheck.transform.parent = TmpRot.transform;
				float w = 0;
				float e = 0;
				float q = 0;
				int rnd2 = Random.Range (1,4);
				if (rnd2 == 1) {
					w = 90f;
				} else if (rnd2 == 2) {
					e = 90f;
				} else {
					q = 90f;
				}
				Vector3 rr = new Vector3 (w, e, q);
				TmpCheck.GetComponent<Rotate> ().rot = rr;
				cubes.Add (TmpCheck);
			}
			TmpRot = TmpCheck;
			for (int k = 1; k < num; k++) {
				if (dir == 1) {
					x++;
				} else if (dir == 2) {
					y++;
				} else if (dir == 3) {
					z++;
				} else if (dir == 4) {
					x--;
				} else if (dir == 5) {
					y--;
				} else if (dir == 6) {
					z--;
				}
				TmpCube = Instantiate (RotCube, new Vector3 (x, y, z), Quaternion.identity);
				TmpCube.transform.parent = TmpRot.transform;
				float w = 0;
				float e = 0;
				float q = 0;
				int rnd2 = Random.Range (1,4);
				if (rnd2 == 1) {
					w = 90f;
				} else if (rnd2 == 2) {
					e = 90f;
				} else {
					q = 90f;
				}
				Vector3 rr = new Vector3 (w, e, q);
				TmpCube.GetComponent<Rotate> ().rot = rr;
				cubes.Add(TmpCube);
				TmpRot = TmpCube;
			}
			if (dir == 1) {
				x++;
			} else if (dir == 2) {
				y++;
			} else if (dir == 3) {
				z++;
			} else if (dir == 4) {
				x--;
			} else if (dir == 5) {
				y--;
			} else if (dir == 6) {
				z--;
			}
			lastDirection = dir%3;
			chck = Random.Range (1, 3);
		}

		for (int r = 0; r < cubes.Count; ++r) {
			int rnd = Random.Range (1,30);
			if (rnd % 10 <= 4) {
				float w = 0;
				float e = 0;
				float q = 0;
				int rnd2 = Random.Range (1,4);
				if (rnd2 == 1) {
					w = 90f;
				} else if (rnd2 == 2) {
					e = 90f;
				} else {
					q = 90f;
				}
				Vector3 rr = new Vector3 (w, e, q);
				Debug.Log(rr);
				cubes [r].transform.Rotate (rr,relativeTo:Space.World);
			}
		}
		Anchar.Instance.SetCheckPoint ();
	}

	// Update is called once per frame
	void Update () {

	}
}