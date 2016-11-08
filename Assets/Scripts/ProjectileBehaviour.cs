using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
