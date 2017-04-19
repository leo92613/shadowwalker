using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * format
checkpoint:
ch,parent ID,relative coordinates
ex: ch,3,1-0-0

rotate
rt, parent ID, relative, axis of rotation, degree of rotation
ex: rt,4,2-2-0,x,90

remote rotate
rr, parent ID, relative coordinates, axis of rotation, degree of rotation, ID of cube to be rotated
ex: rr,1,0-0-1,x,90,4

*/


public class levelparserscript : MonoBehaviour
{
    public bool DEBUG = true;

    public TextAsset level_file;

    private List<GameObject> cubeList;
    private int numCubes = 0;
    private int maxCubes = 100;

    public GameObject L;
    public GameObject Check;
    public GameObject Cube;
    public GameObject RotCube;

    void Start()
    {
        if (DEBUG) Debug.Log("level parser start");
        cubeList = new List<GameObject>();
        // parseLevel(level_file);

        //GameObject testcube = Instantiate(Resources.Load("NormalCube")) as GameObject;
        //if (DEBUG) Debug.Log("test instantiate " + testcube.GetComponent<Transform>());
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            
        }
        //Debug.Log(level_file.text);
    }

    public void loadLevel()
    {
        resetCubeList();
        parseLevel(level_file);
    }

    private void parseLevel(TextAsset t)
    {
        string level = t.text;
        string[] blocklist = Regex.Split(level, "\n");
        if (DEBUG) Debug.Log("list length " + blocklist.Length);

        // first instantiate all the cubes so that we don't get a null reference exception when we try to set parents
        for (int i=0; i<blocklist.Length; i++)
        {
            string[] blockdata = Regex.Split(blocklist[i], ",");
            instantiateCube(blockdata[0], i);
        }

        // i = id of the current cube we're working on
        // this contains all the cubes we need to instantiate
        for (int i = 0; i < blocklist.Length; i++)
        {
            string[] blockdata = Regex.Split(blocklist[i], ",");
            for (int j=0; j<blockdata.Length; j++)
            {
                // if (DEBUG) Debug.Log(blockdata[j]);
                // find type
                string type = blockdata[0];

                // get parent id
                int parent = Convert.ToInt16(blockdata[1]);

                // get parent coordinates
                string[] coord = Regex.Split(blockdata[2], ":");
                int[] nodeCoord = new int[3];
                for (int k=0; k<coord.Length; k++)
                {
                    nodeCoord[k] = Convert.ToInt16(coord[k]);
                }

                // remaining blockdata starts at element 3
                switch (type)
                {
                    case "ch":
                        createCheckpoint(i, type, parent, nodeCoord, blockdata);
                        break;
                    case "rt":
                        createRotate(i, type, parent, nodeCoord, blockdata);
                        break;
                    case "rr":
                        createRemote(i, type, parent, nodeCoord, blockdata);
                        break;
                    case "nm":
                        createNormal(i, type, parent, nodeCoord, blockdata);
                        break;
                    default:
                        Debug.Log("something has gone terribly awry with the autoinstantiate, check formatting of level data");
                        break;
                }
            }
        }
    }

    private void instantiateCube(string type, int id)
    {
        UnityEngine.Object cube = new UnityEngine.Object();
        switch (type)
        {
            case "ch":
                cube = Resources.Load("CheckPoint");
                break;
            case "rt":
                cube = Resources.Load("RotatableCube");
                break;
            case "rr":
                cube = Resources.Load("RemoteCube");
                break;
            case "nm":
                cube = Resources.Load("NormalCube");
                break;
            default:
                Debug.Log("something has gone terribly awry with the autoinstantiate, check formatting of level data");
                break;
        }
        cubeList.Add(Instantiate(cube) as GameObject);
    }

    // checkpoints (ch) created with parent, relative location, and order of collection
    private void createCheckpoint(int i, string type, int parent, int[] coord, string[] data)
    {
        // parent node - if this is the first one, it has no parent
        setParentandPosition(i, parent, coord);
    }

    // rotate cubes (rt) created with parent, relative location, rotation axis, and rotation angle
    private void createRotate(int i, string type, int parent, int[] coord, string[] data)
    {
        setParentandPosition(i, parent, coord);

        string rotateAxis = data[3];
        int degreeRotation = Convert.ToInt16(data[4]);
        Vector3 rotate = new Vector3();
        if (rotateAxis=="x")
        {
            rotate.x = degreeRotation;
        }
        else if (rotateAxis=="y")
        {
            rotate.y = degreeRotation;
        }
        else if (rotateAxis == "z")
        {
            rotate.z = degreeRotation;
        }

        cubeList[i].GetComponent<Rotate>().rot = rotate;
    }

    // remote rotatae (rr) created with parent, relative location, and name of remote cube to be rotated
    private void createRemote(int i, string type, int parent, int[] coord, string[] data)
    {
        setParentandPosition(i, parent, coord);

        // id of cube to be moved
        int remoteSubject = Convert.ToInt16(data[5]);


        // axis of rotation of remote cube
        string rotateAxis = data[3];
        // rotation vector of remote cube
        int degreeRotation = Convert.ToInt16(data[4]);
        Vector3 rotate = new Vector3();
        if (rotateAxis == "x")
        {
            rotate.x = degreeRotation;
        }
        else if (rotateAxis == "y")
        {
            rotate.y = degreeRotation;
        }
        else if (rotateAxis == "z")
        {
            rotate.z = degreeRotation;
        }

        cubeList[i].GetComponent<Remote>().rot = rotate;
        // int remote = cubeList[remoteSubject].GetInstanceID();
        cubeList[i].GetComponent<Remote>().RotCube = cubeList[remoteSubject].GetComponent<Rotate>();
    }

    // normal cube (nm) created with parent, relative location
    // apparently we'll never use this
    private void createNormal(int i, string type, int parent, int[] coord, string[] data)
    {
        setParentandPosition(i, parent, coord);
    }
    

    private void setParentandPosition(int cube, int parent, int[] coord)
    {
        // if we have a parent, set it, otherwise no parent (-1)
        if (parent != -1)
        {
            cubeList[cube].transform.parent = cubeList[parent].transform;
        }
        // then set the position vector
        cubeList[cube].transform.position = new Vector3(coord[0], coord[1], coord[2]);
    }

    // Brian's generation script here
    public void generate()
    {
        // reset everything
        resetCubeList();

        // now generate
        GameObject TmpCheck;
        GameObject TmpCube;
        GameObject TmpRot;
        // Instantiate(L, new Vector3(0, 0, -15), Quaternion.identity);
        int lastDirection = 0;
        int x = 0;
        int y = 0;
        int z = 0;
        int dir = 0;
        for (int i = 0; i < 7; ++i)
        {
            int num = UnityEngine.Random.Range(2, 6);
            while (lastDirection == dir % 3)
            {
                dir = UnityEngine.Random.Range(1, 7);
            }
            TmpCheck = Instantiate(Check, new Vector3(x, y, z), Quaternion.identity);

            cubeList.Add(TmpCheck);

            for (int k = 1; k < num; k++)
            {
                if (dir == 1)
                {
                    x++;
                }
                else if (dir == 2)
                {
                    y++;
                }
                else if (dir == 3)
                {
                    z++;
                }
                else if (dir == 4)
                {
                    x--;
                }
                else if (dir == 5)
                {
                    y--;
                }
                else if (dir == 6)
                {
                    z--;
                }
                TmpCube = Instantiate(Cube, new Vector3(x, y, z), Quaternion.identity);
                TmpCube.transform.parent = TmpCheck.transform;
                cubeList.Add(TmpCube);
            }
            if (dir == 1)
            {
                x++;
            }
            else if (dir == 2)
            {
                y++;
            }
            else if (dir == 3)
            {
                z++;
            }
            else if (dir == 4)
            {
                x--;
            }
            else if (dir == 5)
            {
                y--;
            }
            else if (dir == 6)
            {
                z--;
            }
            lastDirection = dir % 3;
        }
    }

    // save current level to a text file
    public void saveLevel()
    {
        int count = cubeList.Count;
        if (DEBUG) Debug.Log("total count " + count);

        string[] output = new string[count];
        // add every element of cubelist to the text file
        for (int i = 0; i<count; i++)
        {
            // Debug.Log(cubeList[i].name);
            string cubeType = "";
            // extra data for rotation and remote
            string misc = "";
            switch(cubeList[i].name)
            {
                case ("CheckPoint(Clone)"):
                    cubeType = "ch";
                    break;
                case ("NormalCube(Clone)"):
                    cubeType = "nm";
                    break;
                case ("RotatableCube(Clone)"):
                    cubeType = "rt";
                    misc = "," + getRotationAxis(cubeList[i].GetComponent<Rotate>().rot);
                    break;
                case ("RemoteCube(Clone)"):
                    // TODO don't forget to add remotely rotated block to this data
                    cubeType = "rr";
                    misc = "," + getRotationAxis(cubeList[i].GetComponent<Remote>().rot);
                    break;
                default:
                    break;
            }

            int parent = -1;
            // try to find parent
            try
            {
                GameObject parentCube = cubeList[i].transform.parent.gameObject;
                parent = cubeList.IndexOf(parentCube); // also returns -1 if not in list...which shouldn't happen
            }
            catch (Exception e)
            {
                // no parent, do nothing
            }

            // get relative coordinates
            float[] coordinates = { 0, 0, 0 };
            coordinates[0] = cubeList[i].transform.position.x;
            coordinates[1] = cubeList[i].transform.position.y;
            coordinates[2] = cubeList[i].transform.position.z;
            string coord = coordinates[0] + ":" + coordinates[1] + ":" + coordinates[2];
            // Debug.Log(coord);

            string line = cubeType + "," + parent + "," + coord + misc;

            // if (DEBUG) Debug.Log(line);
            output[i] = line;
        }
        writeToFile(output);
    }

    private void writeToFile(string[] output)
    {
        string write = "";
        // save as string but make sure there's no return at eof
            for (int i=0; i<output.Length-1; i++)
            {
                if (DEBUG) Debug.Log(output[i]);
                write = write + output[i] + "\n";
            }
        write = write + output[output.Length - 1];
        File.WriteAllText(Application.dataPath + "/gen_level.txt", write);
		#if UNITY_EDITOR
        AssetDatabase.Refresh();
		#endif
    }

    public void resetCubeList()
    {
        for (int i = 0; i<cubeList.Count; i++)
        {
            Destroy(cubeList[i]);
        }
        cubeList = new List<GameObject>();
    }

    private string getRotationAxis(Vector3 vector)
    {
        if (vector.x != 0)
        {
            return "x" + "," + vector.x;
        }
        else if (vector.y != 0)
        {
            return "y" + "," + vector.y;
        }
        else if (vector.z != 0) {
            return "z" + "," + vector.z;
        }
        return "";
    }
}
