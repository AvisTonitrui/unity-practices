using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour {

    //public ints for UI to use
    public int sucesses = -1;
    public int failures = 0;
    public int patternLength;

    //Light is a unity built-in object
    public Light lightRed;
    public Light lightBlue;
    public Light lightGreen;
    public Light lightYellow;

    //boolean is used to check if pattern has finished showing
    bool finished = false;
    bool running = false;

    //This array is used to determine the pattern given
    List<float> pattern = new List<float>();

    //This array is used for keeping track of what the player has input, with an accompanying int for keeping track of how many inputs the player has given
    int input = 0;
    List<float> given = new List<float>();

    //this boolean is used to check whether or not the input was correct
    bool correct = true;

    //this function either turns all the lights on or off depending on what's passed to it
    void toggleAll (bool boolean) {
        if (boolean) {
            lightRed.enabled = true;
            lightBlue.enabled = true;
            lightGreen.enabled = true;
            lightYellow.enabled = true;

        } else {
            lightRed.enabled = false;
            lightBlue.enabled = false;
            lightGreen.enabled = false;
            lightYellow.enabled = false;
        }
    }

    //this function toggles a corresponding light
    void toggleLight (float light) {
        //Debug.Log("Checking to see which light to turn on.");
        if (light == 0.0) {
            lightRed.enabled = true;
        }
        else if (light == 1.0) {
            lightBlue.enabled = true;
        }
        else if (light == 2.0) {
            lightGreen.enabled = true;
        }
        else if (light == 3.0) {
            lightYellow.enabled = true;
        }
    }

    //This function is used to turn lights on and off in the pattern.  It starts by turning everything off, then checking what should be turned on
    IEnumerator show () {
        Debug.Log(patternLength);

        //we first add another element to the pattern list to add to the difficulty if the player got the pattern correct
        if (correct) {
            //incrementing the successes variable
            sucesses++;

            //adding to the list
            pattern.Add(Random.Range(0, 4));
            patternLength++;

            //flashing the lights three times
            yield return new WaitForSeconds(0.25f);
            toggleAll(true);
            yield return new WaitForSeconds(0.25f);
            toggleAll(false);
            yield return new WaitForSeconds(0.25f);
            toggleAll(true);
            yield return new WaitForSeconds(0.25f);
            toggleAll(false);
            yield return new WaitForSeconds(0.25f);
            toggleAll(true);
            yield return new WaitForSeconds(0.25f);
            toggleAll(false);

        } else if(pattern.Count != 1) {
            //incrementing the failures varialbe
            failures++;

            //taking off and making things simpler to help reinforce the smaller pattern
            pattern.RemoveAt(pattern.Count - 1);
            patternLength--;

            //flashing the lights three times
            yield return new WaitForSeconds(0.25f);
            toggleAll(true);
            yield return new WaitForSeconds(0.25f);
            toggleAll(false);
            yield return new WaitForSeconds(0.25f);
            toggleAll(true);
            yield return new WaitForSeconds(0.25f);
            toggleAll(false);
            yield return new WaitForSeconds(0.25f);
            toggleAll(true);
            yield return new WaitForSeconds(0.25f);
            toggleAll(false);
        } else {
            //increments failures
            failures++;

            //flashing the lights three times
            yield return new WaitForSeconds(0.25f);
            toggleAll(true);
            yield return new WaitForSeconds(0.25f);
            toggleAll(false);
            yield return new WaitForSeconds(0.25f);
            toggleAll(true);
            yield return new WaitForSeconds(0.25f);
            toggleAll(false);
            yield return new WaitForSeconds(0.25f);
            toggleAll(true);
            yield return new WaitForSeconds(0.25f);
            toggleAll(false);
        }

        for (int i = 0; i < pattern.Count; i++) {
            //turning the lights off first
            //Debug.Log("Turning the lights off.");
            toggleAll(false);

            //now checking which light to turn on
            toggleLight(pattern[i]);

            //waits so the light can be seen, then turns all the lights off
            //Debug.Log("Waiting");
            yield return new WaitForSeconds(0.5f);

            toggleAll(false);
        }

        //finishes by saying it is finished
        Debug.Log("I'm finished, player turn");

        //resetting all variables for the iteration
        finished = true;
        running = false;
        given.Clear();
        input = 0;
        correct = true;
    }

    // Use this for initialization
    void Start () {
        patternLength = 0;

        //this will set the lights to begin as off
        toggleAll(false);
    }
	
	// Update is called once per frame
	void Update () {
        
        //initialy checking whether or not the pattern has finished, determining whose turn it is
        if (finished) {
            //Checks to see if any valid button was pressed down and enables the corresponding light if it was.  It also adds the input to the given array for checking the input
            if (Input.GetButtonDown("Red")) {
                //enbles redLight
                lightRed.enabled = true;

                //add input
                given.Add(0.0f);
                input ++;
                //Debug.Log(given[input - 1]);

            } else if (Input.GetButtonDown("Blue")) {
                //enable blueLight
                lightBlue.enabled = true;

                //add input
                given.Add(1.0f);
                input ++;
                //Debug.Log(given[input - 1]);

            } else if (Input.GetButtonDown("Green")) {
                //enable greenLight
                lightGreen.enabled = true;

                //add input
                given.Add(2.0f);
                input ++;
                //Debug.Log(given[input - 1]);

            } else if (Input.GetButtonDown("Yellow")) {
                //enable greenLight
                lightYellow.enabled = true;

                //add input
                given.Add(3.0f);
                input ++;
                //Debug.Log(given[input - 1]);
            }

            //checks to see if a button was released, turning off the light that it corresponds to. Each if statement is separate in case multiple buttons are released on the same frame.
            if (Input.GetButtonUp("Red")) {
                //disables red light
                lightRed.enabled = false;
            }

            if (Input.GetButtonUp("Blue")) {
                //disables blue light
                lightBlue.enabled = false;
            }

            if (Input.GetButtonUp("Green")) {
                //disables green light
                lightGreen.enabled = false;
            }

            if (Input.GetButtonUp("Yellow")) {
                //disables yellow light
                lightYellow.enabled = false;
            }

            //Now checks to see if input is the length of given, in which case the player's turn will end and will check to see if the pattern inputted was correct.
            if (given.Count >= pattern.Count) {
                for (int i = 0; i < given.Count; i++) {

                    //checking if input is correct and outputs the result. also sets finished to false to reshow pattern
                    if (!(given[i] == pattern[i])) {
                        Debug.Log("That was incorrect");
                        correct = false;

                        //breaking from false answer to save processing power
                        break;
                    }
                }

                //printing correct if the answer was correct and set finished to false to show pattern again
                if (correct) {
                    Debug.Log("Correct!");
                }

                //disable all lights
                toggleAll(false);

                //set finished to false to show either the same or an extended pattern
                finished = false;
            }
            
        } else if (!running) {
            Debug.Log("Evoking the show function");
            StartCoroutine("show");
            running = true;
        }
	}
}
