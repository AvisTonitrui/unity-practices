using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class PlayerObjectControl : MonoBehaviour {

    public GameObject player;

    public CarController cCont;
    public CarUserControl cUserCont;
    public CarAudio cAudio;

    // Use this for initialization
    void Start () {
        //disable car
        cCont.enabled = false;
        cUserCont.enabled = false;
        cAudio.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Interact")) {
            if (player.GetComponent<Interact>().canEnter) {
                player.SetActive(false);
                cCont.enabled = true;
                cUserCont.enabled = true;
                cAudio.enabled = true;
                player.GetComponent<Interact>().canEnter = false;
            } else {
                player.SetActive(true);
                cCont.enabled = false;
                cUserCont.enabled = false;
                cAudio.enabled = false;
            }
        }
	}
}
