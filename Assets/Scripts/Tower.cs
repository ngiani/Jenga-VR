using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public Block selected;
    GameObject cube;

    int i = 0;
    int frameCounter = 0;

    // Use this for initialization
    void Start () {

        cube = (GameObject)Resources.Load("Prefabs/Cube");
    }
	
	// Update is called once per frame
	void Update () {

        if (frameCounter % 20 == 0 && i < 8)
        {
            float sizeX = cube.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;
            float sizeY = cube.GetComponent<MeshFilter>().sharedMesh.bounds.size.y;
            float sizeZ = cube.GetComponent<MeshFilter>().sharedMesh.bounds.size.z;

            if (i % 2 == 0)
            {

                Instantiate(cube, new Vector3(0, transform.position.y + (sizeY * (i + 1)), 0), Quaternion.Euler(0, 90, 0), this.transform);
                Instantiate(cube, new Vector3(0, transform.position.y + (sizeY * (i + 1)), sizeZ/2), Quaternion.Euler(0, 90, 0), this.transform);
                Instantiate(cube, new Vector3(0, transform.position.y + (sizeY * (i + 1)), -sizeZ/2), Quaternion.Euler(0, 90, 0), this.transform);
            }

            else
            {

                Instantiate(cube, new Vector3(0, transform.position.y + (sizeY * (i + 1)), 0), Quaternion.Euler(0, 0, 0), this.transform);
                Instantiate(cube, new Vector3(sizeZ/2, transform.position.y + (sizeY * (i + 1)), 0), Quaternion.Euler(0, 0, 0), this.transform);
                Instantiate(cube, new Vector3(-sizeZ/2, transform.position.y + (sizeY * (i + 1)), 0), Quaternion.Euler(0, 0, 0), this.transform);
            }

            i++;
            
        }

        frameCounter++;

    }
}
