using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {


    public float health;

    void Start () {
        this.GetComponent<Rigidbody>().isKinematic = true;
    }
	
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        health -= 15;
        if(health <= 0)
        {
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Rigidbody>().isKinematic = false;
            if (Random.Range(0, 10000) == 0)
            {
                this.GetComponent<Rigidbody>().AddForce(Vector3.left * 500);
            }
            else
            {
                this.GetComponent<Rigidbody>().AddForce(Vector3.right * 500);

            }
            this.GetComponent<Rigidbody>().AddTorque(transform.forward * 400);
            Destroy(gameObject, 2f);
        }


    }


}
