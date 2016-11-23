using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {

    public MainGameController controller;

    public float health;
    public GameObject explosion;
    public float swingRange;

    public float max;
    public float min;

    private float swing;
    private Vector3 originalPosition;
    private float multi;

    void Start () {
        this.GetComponent<Rigidbody>().isKinematic = true;
        swing = 1f;
        originalPosition = transform.position;
        //print("Orignial Position" + originalPosition.ToString());
        multi = Random.Range(1, 4);
        GameObject controllerObject = GameObject.Find("Main Game Controller");
        controller = controllerObject.GetComponent<MainGameController>();
    }
	
	void Update () {
        ////var swing = Random.RandomRange(-swingRange, swingRange);
        //Vector3 oldPosition = transform.position;
        ////print(oldPosition);
        ////print(transform.position.x);
        //if (oldPosition.x - swingRange <= 2f)
        //{
        //    transform.position = new Vector3(swing * Time.deltaTime, 0, 0) + oldPosition;
        //    print("left");
        //}
        //else
        //{
        //    transform.position = new Vector3(-swing * Time.deltaTime, 0, 0) + oldPosition;
        //    print("right");
        //}
        //transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, originalPosition.y, originalPosition.z);

        var tempPosition = transform.position;
        var tempRotation = transform.rotation;
        //max = Random.Range(3, 10);
        //min = Random.Range(1, 2);
        //min = 1f;
        var newX = Mathf.PingPong(Time.time * multi, max - min) + min;// *Time.deltaTime;
        var newRot = Mathf.PingPong(Time.time * multi, max - min) + min;// *Time.deltaTime;
        //newX = newX * Time.deltaTime;
        transform.position = new Vector3(newX + originalPosition.x, originalPosition.y, originalPosition.z);
        //transform.rotation = 

        
        
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
            controller.DecrementEnemyCount();
        }


    }

    public void PrematureDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
