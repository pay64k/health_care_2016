using UnityEngine;
using System.Collections;

public class IndicatorBehaviour : MonoBehaviour {

    private MainGameController controller;
    // Use this for initialization
	void Start () {
        GameObject controllerObject = GameObject.Find("Main Game Controller");
        controller = controllerObject.GetComponent<MainGameController>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(controller.leftDistance);
	}
}
