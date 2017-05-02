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
		GameObject TmpRot;
		GameObject TmpCube;
		//Instantiate(L, new Vector3(0, 0, -15), Quaternion.identity);
		int lastDirection = 0;
		int x = 0;
		int y = 0;
		int z = 0;
		int dir = 0;

		int checknum = Random.Range (4, 7);
		for (int i = 0; i < checknum; ++i) {
			TmpCheck = Instantiate (Check, new Vector3 (x, y, z), Quaternion.identity);
			TmpCube = TmpCheck;
			int vertices = Random.Range (2, 5);
			for (int j = 0; j < vertices; ++j) {
				while (lastDirection == dir % 3) {
					dir = Random.Range (1, 6);
				}
				int numblocks = Random.Range (3, 5);
				for (int k = 0; k < numblocks; ++k) {
					if (dir == 1) {
						++x;
					} else if (dir == 2) {
						++y;
					} else if (dir == 3) {
						++z;
					} else if (dir == 4) {
						--x;
					} else if (dir == 5) {
						--y;
					} else if (dir == 6) {
						--z;
					}
					TmpRot = Instantiate (RotCube, new Vector3 (x, y, z), Quaternion.identity);
					TmpRot.transform.parent = TmpCube.transform;
					float xRot = 0;
					float yRot = 0;
					float zRot = 0;
					int rotdir = Random.Range (1, 4);
					int negrot = Random.Range (1, 2);
					if ((TmpRot.transform.parent.GetComponent<CheckPoint>())) {
						Vector3 rotaxis = TmpRot.transform.parent.position;
						Vector3 rotaxis2 = TmpRot.transform.position;
						if (rotaxis[1] == rotaxis2[1]) {
							if (negrot == 1) {
								xRot = 90f;
							} else {
								xRot = -90f;
							}
						} else if (rotaxis[0] == rotaxis2[0]) {
							if (negrot == 1) {
								yRot = 90f;
							} else {
								yRot = -90f;
							}
						}
					} else if (rotdir == 1) {
						if (negrot == 1) {
							xRot = 90f;
						} else {
							xRot = -90f;
						}
					} else if (rotdir == 2) {
						if (negrot == 1) {
							yRot = 90f;
						} else {
							yRot = -90f;
						}
					} else {
						if (negrot == 1) {
							zRot = 90f;
						} else {
							zRot = -90f;
						}
					}
					Vector3 rotvar = new Vector3 (xRot, yRot, zRot);
					TmpRot.GetComponent<Rotate> ().rot = rotvar;
					cubes.Add (TmpRot);
					TmpCube = TmpRot;
				}
				lastDirection = dir % 3;
			}
			if (dir == 1) {
				++x;
			} else if (dir == 2) {
				++y;
			} else if (dir == 3) {
				++y;
			} else if (dir == 4) {
				--x;
			} else if (dir == 5) {
				--y;
			} else if (dir == 6) {
				--x;
			}
			dir = 0;
			lastDirection = 0;
		}

		Instantiate (Check, new Vector3 (x, y, z), Quaternion.identity);

		// +yRot = left
		// -yRot = right
		// +xRot = up
		// -xRot = down

		for (int idx = 0; idx < cubes.Count; ++idx) {
			int rnd = Random.Range (0,100);
			if (true) {
				int rnd2 = Random.Range (1, 2);
				int rnd3 = Random.Range (1, 2);
				while (rnd2 > 0) {
					Vector3 rotaxis = cubes[idx].GetComponent<Rotate> ().rot;
					Vector3 child = cubes[idx].transform.position;
					Vector3 parent = cubes[idx].transform.parent.position;
					if (rotaxis.x > 0f) {
						if (parent.y > child.y) {
							cubes [idx].transform.Rotate (-cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World);
						} else {
							cubes [idx].transform.Rotate (cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World);  // breaks consistency
						}
					} else if (rotaxis.x < 0f) {
						if (parent.y < child.y) {
							cubes [idx].transform.Rotate (-cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World);
						} else {
							cubes [idx].transform.Rotate (cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World); // breaks consistency
						}
					} else if (rotaxis.y > 0f) {
						if (parent.x < child.x) {
							cubes [idx].transform.Rotate (-cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World);
						} else {
							cubes [idx].transform.Rotate (cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World); // breaks consistency
						}
					} else if (rotaxis.y < 0f) {
						if (parent.x > child.x) {
							cubes [idx].transform.Rotate (-cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World);
						} else {
							cubes [idx].transform.Rotate (cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World); // breaks consistency
						}
					} else {
						if (rnd3 == 1) {
							cubes [idx].transform.Rotate (cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World);
						} else {
							cubes [idx].transform.Rotate (-cubes [idx].GetComponent<Rotate> ().rot, relativeTo: Space.World);
						}
					}
					--rnd2;
				}
			}
		}
		Anchar.Instance.SetCheckPoint ();
	}

	// Update is called once per frame
	void Update () {

	}
}