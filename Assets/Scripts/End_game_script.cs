using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class End_game_script : MonoBehaviour {

    private string yourScore;
    public GameObject End_score;

	// Use this for initialization
	void Start () {
        yourScore = GameObject.Find("Score").GetComponent<TextMesh>().text;
        End_score.GetComponent<TextMesh>().text = yourScore;
        StartCoroutine(Blink_text(0.7f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    IEnumerator Blink_text(float time)
    {
        yield return new WaitForSeconds(time);
        End_score.SetActive(false);
        yield return new WaitForSeconds(time);
        End_score.SetActive(true);
        yield return new WaitForSeconds(time);
        End_score.SetActive(false);
        yield return new WaitForSeconds(time);
        End_score.SetActive(true);
        yield return new WaitForSeconds(time);
        End_score.SetActive(false);
        yield return new WaitForSeconds(time);
        End_score.SetActive(true);
        yield return new WaitForSeconds(time);
        End_score.SetActive(false);
        yield return new WaitForSeconds(time);
        End_score.SetActive(true);
        yield return new WaitForSeconds(time);
        End_score.SetActive(false);

        Destroy(GameObject.Find("Score"));
        //Destroy(GameObject.Find("Music"));
        nextScene();


    }

    void nextScene()
    {
        if (NiceSceneTransition.instance != null)
        {
            NiceSceneTransition.instance.LoadScene("Scene_StartScene");
        }
        else
        {

            SceneManager.LoadScene("Scene_StartScene", LoadSceneMode.Single);
        }
    }

}
