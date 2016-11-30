using UnityEngine;
using System.Collections;



public class MusicBehaviour : MonoBehaviour {

    public static MusicBehaviour instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            instance = this;
            //if (fadeIn)
            //{
            //    fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 1.0f);
            //}
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
