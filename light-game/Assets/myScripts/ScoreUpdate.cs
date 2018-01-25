using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour {

    //Text boxes being used to display the scores
    public Text success;
    public Text failure;
    public Text length;

    Lights lights;

	// Use this for initialization
	void Start () {
        lights = GameObject.Find("signals").GetComponent<Lights>();
	}
	
	// Update is called once per frame
	void Update () {
        success.text = lights.sucesses.ToString();
        failure.text = lights.failures.ToString();
        length.text = lights.patternLength.ToString();
	}
}
