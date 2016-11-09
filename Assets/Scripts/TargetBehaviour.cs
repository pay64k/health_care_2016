using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {


    public float health;
    public GameObject explosion;


    void Start () {
        this.GetComponent<Rigidbody>().isKinematic = true;
    }
	
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        health -= 15;
        Instantiate(explosion, transform.position - new Vector3(0,1.5f,0), transform.rotation);
        if (health <= 0)
        {
            //this.GetComponent<Collider>().enabled = false;
            //this.GetComponent<Rigidbody>().isKinematic = false;
            //if (Random.Range(0, 10000) == 0)
            //{
            //    this.GetComponent<Rigidbody>().AddForce(Vector3.left * 500);
            //}
            //else
            //{
            //    this.GetComponent<Rigidbody>().AddForce(Vector3.right * 500);

            //}
            //this.GetComponent<Rigidbody>().AddTorque(transform.forward * 400);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }


    }


}
