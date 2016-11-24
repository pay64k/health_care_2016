using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStarScene : MonoBehaviour {


    public GameObject start_text;
    public GameObject countdown_text;
    public GameObject info_text;
    public GameObject audio_song;

    private float timer;
    private float limit;
	// Use this for initialization
	void Start () {
        //SceneManager.LoadScene(0);
        timer = 0;
        limit = 1.5f;
        countdown_text.SetActive(false);
        DontDestroyOnLoad(audio_song);
	}

	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SceneManager.LoadScene(1);

            AudioSource audioObj = GameObject.Find("Music").GetComponent<AudioSource>();

            if (!audioObj.isPlaying)
            {
                audioObj.Play();
            }
             
            StartCoroutine(StartGame(1));
        }
	    
	}

    IEnumerator StartGame(float time)
    {
        start_text.SetActive(false);
        info_text.SetActive(false);

        var i = 5;
        string pre = "starting in ";

        countdown_text.SetActive(true);
        countdown_text.GetComponent<TextMesh>().text = pre + i.ToString();
        i = i - 1;

        yield return new WaitForSeconds(time);
        countdown_text.SetActive(true);
        countdown_text.GetComponent<TextMesh>().text = pre + i.ToString();
        i = i - 1;

        yield return new WaitForSeconds(time);
        countdown_text.SetActive(true);
        countdown_text.GetComponent<TextMesh>().text = pre + i.ToString();
        i = i - 1;

        yield return new WaitForSeconds(time);
        countdown_text.SetActive(true);
        countdown_text.GetComponent<TextMesh>().text = pre + i.ToString();
        i = i - 1;

        yield return new WaitForSeconds(time);
        countdown_text.SetActive(true);
        countdown_text.GetComponent<TextMesh>().text = pre + i.ToString();
        i = i - 1;

        yield return new WaitForSeconds(time);
        countdown_text.SetActive(true);
        countdown_text.GetComponent<TextMesh>().text = pre + i.ToString();

        //yield return new WaitForSeconds(time);

        //start_text.SetActive(false);
        //SceneManager.LoadScene(1);
        if (NiceSceneTransition.instance != null)
        {
            NiceSceneTransition.instance.LoadScene("MainScene");
        }
        else
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }

    }

}
