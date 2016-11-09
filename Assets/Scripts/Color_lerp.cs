using UnityEngine;
using System.Collections;

public class Color_lerp : MonoBehaviour {

	public GameObject hand;
	public float distance;


	// Use this for initialization
	void Start () {
	
		}
	
	// Update is called once per frame
	void Update () {
		
		var startcolor = Color.red;
		var endcolor = Color.green;
		float maxdistance = 15;


		distance = Vector3.Distance (transform.position,hand.transform.position);

		transform.GetComponent<Renderer> ().material.color = Color.Lerp (Color.green, Color.red, distance/maxdistance);

		//transform.GetComponent<Renderer> ().material.color = Color.Lerp (Color.red, Color.green, Mathf.PingPong (Time.time, 1));
	}
}
