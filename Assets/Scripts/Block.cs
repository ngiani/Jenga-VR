using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HandleSelection()
    {
        //Change material with a light emitting one
        GetComponent<MeshRenderer>().material = Resources.Load("Materials/IlluminatedWood") as Material;

        if (GetComponent<Rigidbody>().isKinematic == false)
            GetComponent<Rigidbody>().isKinematic = true;
    }

    public void HandleUnSelection()
    {
        //Change material with default one
        GetComponent<MeshRenderer>().material = Resources.Load("Materials/Wood") as Material;

        if (GetComponent<Rigidbody>().isKinematic == true)
            GetComponent<Rigidbody>().isKinematic = false;
    }
}
