using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

    //setup direction vars
    public Transform nPoint;
    public Transform ePoint;
    public Transform sPoint;
    public Transform wPoint;

    //setup detected objects
    public GameObject nObject;
    public GameObject eObject;
    public GameObject sObject;
    public GameObject wObject;

    //setup player vars
    Transform playerTransform;
    Vector3 targetPos;
    bool moveNorth = false;
    bool moveEast = false;
    bool moveSouth = false;
    bool moveWest = false;

    // Use this for initialization
    void Start () {
        playerTransform = GetComponent<Transform>();
	}

    //This function is used to see if the player should move
    void PlayerMove () {
        //moving north
        if (nObject != null && moveNorth == false) {
            if (nObject.tag == "Tile") {
                if (Input.GetAxis("Vertical") > 0) {
                    //move player
                    moveNorth = true;
                    targetPos = nObject.transform.position;
                }
            }
        }

        //moving east
        if (eObject != null && moveEast == false) {
            if (eObject.tag == "Tile") {
                if (Input.GetAxis("Horizontal") > 0) {
                    //move player
                    moveEast = true;
                    targetPos = eObject.transform.position;
                }
            }
        }

        //moving south
        if (sObject != null && moveSouth == false) {
            if (sObject.tag == "Tile") {
                if (Input.GetAxis("Vertical") < 0) {
                    //move player
                    moveSouth = true;
                    targetPos = sObject.transform.position;
                }
            }
        }

        //moving west
        if (wObject != null && moveWest == false) {
            if (wObject.tag == "Tile") {
                if (Input.GetAxis("Horizontal") < 0) {
                    //move player
                    moveWest = true;
                    targetPos = wObject.transform.position;
                }
            }
        }
    }

    bool CommitMove (bool move) {
        if (move == true) {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPos + Vector3.up * 1.5f, 0.1f);

            //reset player move ability
            if (playerTransform.position.x == targetPos.x && playerTransform.position.z == targetPos.z) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        /*//Debug draw rays to confirm direction
        Debug.DrawRay(nPoint.position, -nPoint.up * 5f, Color.red, 0.1f);
        Debug.DrawRay(ePoint.position, -ePoint.up * 5f, Color.red, 0.1f);
        Debug.DrawRay(sPoint.position, -sPoint.up * 5f, Color.red, 0.1f);
        Debug.DrawRay(wPoint.position, -wPoint.up * 5f, Color.red, 0.1f);
        */

        RaycastHit hit;

        //setting the object variables to store the object that the rays are hitting
        if (Physics.Raycast(nPoint.position, -nPoint.up * 5f, out hit)) {
            nObject = hit.collider.gameObject;
        } else {
            nObject = null;
        }

        if (Physics.Raycast(ePoint.position, -ePoint.up * 5f, out hit)) {
            eObject = hit.collider.gameObject;
        } else {
            eObject = null;
        }

        if (Physics.Raycast(sPoint.position, -sPoint.up * 5f, out hit)) {
            sObject = hit.collider.gameObject;
        } else {
            sObject = null;
        }

        if (Physics.Raycast(wPoint.position, -wPoint.up * 5f, out hit)) {
            wObject = hit.collider.gameObject;
        } else {
            wObject = null;
        }

        //moving the player
        PlayerMove();
        moveNorth = CommitMove(moveNorth);
        moveEast = CommitMove(moveEast);
        moveSouth = CommitMove(moveSouth);
        moveWest = CommitMove(moveWest);
    }
}
