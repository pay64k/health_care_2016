using UnityEngine;
using System.Collections;

public class IndicatorBehaviour : MonoBehaviour {

    private MainGameController controller;
    private string hand;
    private float distance;
    public GameObject side;

    // Use this for initialization
	void Start () {
        GameObject controllerObject = GameObject.Find("Main Game Controller");
        controller = controllerObject.GetComponent<MainGameController>();
        hand = side.GetComponent<JointPositioner>().JointToTrack.ToString();
	}
	
	void Update () {
        //Debug.Log(controller.leftDistance);
        //if side == left get leftDistance etc
        //JointPositioner bla = side.GetComponent<JointPositioner>();
        //Debug.Log(bla.JointToTrack.ToString());
        if (hand == "HAND_LEFT")
        {
            distance = controller.leftDistance;
        }
        else
        {
            distance = controller.rightDistance;
        }

        //Debug.Log(gameObject.GetComponent<Rigidbody>().isKinematic);
        //Debug.Log(hand + " " + distance.ToString());

        //do the color changing here using gameObject.GetComponent<>()
        
	}

    private static float MapValue(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
