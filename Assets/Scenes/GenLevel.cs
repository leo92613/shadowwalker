 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenLevel : MonoBehaviour {
	public GameObject L;
	public GameObject Check;
	public GameObject Cube;
	public GameObject RotCube;
	// Use this for initialization
	void Start () {
		GameObject TmpCheck;
		GameObject TmpCube;
		GameObject TmpRot;
		Instantiate(L, new Vector3(0, 0, -15), Quaternion.identity);
		int lastDirection = 0;
		int x = 0;
		int y = 0;
		int z = 0;
		int dir = 0;
		for (int i = 0; i<7;++i) {
			int num = Random.Range (2, 6);
			while (lastDirection == dir%3) {
				dir = Random.Range (1, 7);
			}
			TmpCheck = Instantiate (Check, new Vector3 (x, y, z), Quaternion.identity);
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
				TmpCube = Instantiate (Cube, new Vector3 (x, y, z), Quaternion.identity);
				TmpCube.transform.parent = TmpCheck.transform;
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
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
