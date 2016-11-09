using UnityEngine;
using System.Collections;

public class scrollscript : MonoBehaviour {
	public float scrollspeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float y = Mathf.Repeat (Time.time * scrollspeed, 1);
		Vector2 offset = new Vector2 (0, y);
		gameObject.GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_Maintex", offset);

	}
}
