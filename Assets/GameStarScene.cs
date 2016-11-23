using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStarScene : MonoBehaviour {


    public GameObject start_text;
    public GameObject countdown_text;

    private float timer;
    private float limit;
	// Use this for initialization
	void Start () {
        //SceneManager.LoadScene(0);
        timer = 0;
        limit = 1.5f;
        countdown_text.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SceneManager.LoadScene(1);
            StartCoroutine(StartGame(1));
        }
	    
	}

    IEnumerator StartGame(float time)
    {
        var i = 5;
        countdown_text.SetActive(true);
        start_text.SetActive(false);
        countdown_text.GetComponent<TextMesh>().text = i.ToString();
        i = i - 1;
        yield return new WaitForSeconds(time);
        countdown_text.SetActive(true);
        start_text.SetActive(false);
        countdown_text.GetComponent<TextMesh>().text = i.ToString();
        i = i - 1;
        yield return new WaitForSeconds(time);
        countdown_text.SetActive(true);
        start_text.SetActive(false);
        countdown_text.GetComponent<TextMesh>().text = i.ToString();
        i = i - 1;
        yield return new WaitForSeconds(time);
        countdown_text.SetActive(true);
        start_text.SetActive(false);
        countdown_text.GetComponent<TextMesh>().text = i.ToString();
        i = i - 1;
        yield return new WaitForSeconds(time);

        start_text.SetActive(false);
        SceneManager.LoadScene(1);

    }

}
