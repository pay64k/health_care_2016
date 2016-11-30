using UnityEngine;
using System.Collections;

public class PositionIndicatorSpawner : MonoBehaviour {

    public GameObject ObjectToSpawn;

    void Start () {

    }
	
	public GameObject SpawnIndicator (Vector3 position, GameObject side) {

        GameObject indicatorCircle = Instantiate(ObjectToSpawn, position, this.transform.rotation) as GameObject;

        indicatorCircle.transform.parent = transform;

        indicatorCircle.GetComponent<IndicatorBehaviour>().side = side;

        return indicatorCircle;
    }
}
