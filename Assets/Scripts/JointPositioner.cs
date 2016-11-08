using UnityEngine;
using System.Collections;

public class JointPositioner : MonoBehaviour {
	
	public KinectWrapper.Joints JointToTrack;
	public KUInterface Kinect;
    public GameConfig Config;
    
    public bool fixateX = true;
	public bool fixateY = true;
	public bool fixateZ = true;

    private bool debugMode;

    private Vector3 position;
    

    // Use this for initialization
    void Start () {
        debugMode = Config.handMouseControl;
    }
	
	// Update is called once per frame
	void Update () {


        if (!debugMode)
        {
            //Choose center
            Vector3 center = Kinect.GetJointPos(KinectWrapper.Joints.SHOULDER_CENTER);

            //Release any non-fixated directions
            if (!fixateX)
                center.x = 0;
            if (!fixateY)
                center.y = 0;
            if (!fixateZ)
                center.z = 0;
            //Position this object according to chosen joint, but do it relative to the ShoulderCenter position
            transform.localPosition = Kinect.GetJointPos(JointToTrack) - center;
        }
        else
        {
            position = Input.mousePosition;
            position = new Vector3(
                MapValue(position.x,0,1800,-20,20),
                MapValue(position.y, 0, 1000, -15, 15),
                0);
            transform.localPosition = position;
            
        }
	
	}

    public static float MapValue(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
