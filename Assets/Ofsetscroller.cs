 using UnityEngine;
using System.Collections;

public class Ofsetscroller : MonoBehaviour {

	public float scrollspeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float y = Mathf.Repeat (Time.time * scrollspeed, 1);
		Vector2 offset = new Vector2 (0, y);
		GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_Maintex", offset);


	}
}
