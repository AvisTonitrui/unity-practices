using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

    //setup plaver vars
    public bool canEnter = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.name == "EntryPoint") {
            canEnter = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.name == "EntryPoint") {
            canEnter = false;
        }
    }
}
