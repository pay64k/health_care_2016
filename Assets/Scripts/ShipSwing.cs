using UnityEngine;
using System.Collections;

public class ShipSwing : MonoBehaviour {

    public float tilt = 100f;
    private Rigidbody myRigidbody;
    private Vector3 curPos;
    private Vector3 lastPos;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        curPos = transform.position;
        var direction = curPos - lastPos;
        if (curPos == lastPos)
        {
            //print("Not moving");
            //transform.Rotate(Vector3.back, 0f);
            return;
        }
        var lastRotation = transform.localEulerAngles;
        if(direction.x <= 0)
        {
            //print("left");
            transform.Rotate(Vector3.back , -tilt * Time.deltaTime);
        }
        else
        {
            //print("right");
            transform.Rotate(Vector3.back, tilt * Time.deltaTime);
        }
        lastPos = curPos;


    }
}
