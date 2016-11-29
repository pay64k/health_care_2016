using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour
{

    public PositionIndicatorSpawner IndicatorSpawner;
    public ProjectileShooter ProjectileShooterLeft;
    public ProjectileShooter ProjectileShooterRight;
    public GameObject targetPrefab;
    public GameConfig Config;

    public GameObject leftHand;
    public GameObject rightHand;

    public float leftDistance;
    public float rightDistance;

    public GameObject text_Great;
    public GameObject text_not_ok;
    public GameObject timeRemainingText;

    public GameObject scoreText;
    private int score;

    private bool debugMode;

    private float shootThreshold;
    private float activeTimeSeconds;
    private float shootIntervalTime;

    private float leftShootTimer = 0f;
    private float rightShootTimer = 0f;

    private float spawnTimer = 0f;
    private bool indicatorsSpawned = false;

    private GameObject indicator1;
    private GameObject indicator2;
    private bool leftInPosition;
    private bool rightInPosition;

    private bool checkPostion;

    private ArrayList coordList;
    private Coordinates coord;
    private int coordCounter;
    private int coordAmount;

    public GameObject[] figures;
    private GameObject currentFigure;

    public ArrayList targets;
    private int amountOfTargets;

    private float gameTime;
    private float currentGameTime;
    private bool counting;

    private ArrayList ok_text_arraylist;

    //Variables for debugging
    private GameObject left_light;
    private GameObject right_light;

    void Start()
    {
        left_light = GameObject.Find("Debugging/left_light");
        right_light = GameObject.Find("Debugging/right_light");
        activeTimeSeconds = Config.IndicatorsActiveTimeSec;
        shootThreshold = Config.ShootThreshold;
        shootIntervalTime = Config.ShootIntervalTimer;

        debugMode = Config.handMouseControl;

        coordList = new ArrayList();
        coordList = fillCoordList(coordList);
        coordCounter = 0;
        coordAmount = coordList.Count;

        targets = new ArrayList();
        amountOfTargets = 0;
        score = 0;
        DontDestroyOnLoad(scoreText);

        gameTime = Config.gameTime;
        currentGameTime = 0;
        text_Great.SetActive(false);
        counting = true;

        foreach (GameObject obj in figures)
        {
            obj.SetActive(false);
        }

        ok_text_arraylist = new ArrayList();

        ok_text_arraylist.Add("Nice!");
        ok_text_arraylist.Add("Great!");
        ok_text_arraylist.Add("Yeah!");
        ok_text_arraylist.Add("Sweet!");
        ok_text_arraylist.Add("O'right!");
        ok_text_arraylist.Add("WOW!");
        ok_text_arraylist.Add("Amazing!");
        ok_text_arraylist.Add("Good shooting!");
        ok_text_arraylist.Add("OMG!");
        ok_text_arraylist.Add("Fantastic!");
        ok_text_arraylist.Add("Excellent!");


    }

    void Update()
    {
        currentGameTime = currentGameTime + Time.deltaTime;
        if (currentGameTime >= gameTime)
        {
            //score = 0;
            currentGameTime = 0;
            counting = false;
            PlayEndGameScene();
        }

        double remainingTime = gameTime - currentGameTime;

        if (remainingTime >= 0 && counting)
        {
            timeRemainingText.GetComponent<TextMesh>().text = remainingTime.ToString("F1");
        }

        if (!indicatorsSpawned)
        {
            indicator1 = IndicatorSpawner.SpawnIndicator(((Coordinates)coordList[coordCounter]).coordLeft, leftHand);
            indicator2 = IndicatorSpawner.SpawnIndicator(((Coordinates)coordList[coordCounter]).coordRight, rightHand);

            for (float i = 10f; i <= 19f; i = i + 3f)
            {
                targets.Add(SpawnEnemy(targetPrefab, coordList, "LEFT", new Vector3(0, i), coordCounter));
                targets.Add(SpawnEnemy(targetPrefab, coordList, "RIGHT", new Vector3(0, i), coordCounter));
                amountOfTargets = amountOfTargets + 2;
            }

            coordCounter++;
            indicatorsSpawned = !indicatorsSpawned;

        }

        if (amountOfTargets == 0)
        {
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(indicator1, 0);
            Destroy(indicator2, 0);
            spawnTimer = 0;
            targets.Clear();
            StartCoroutine(DisplayGreatText(1));
            indicatorsSpawned = !indicatorsSpawned;
            currentFigure.SetActive(false);
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= activeTimeSeconds)
        {
            Destroy(indicator1, 0);
            Destroy(indicator2, 0);
            spawnTimer = 0;
            foreach (GameObject obj in targets)
            {
                if (!obj.Equals(null))
                    obj.GetComponent<TargetBehaviour>().PrematureDestroy();
            }
            targets.Clear();
            amountOfTargets = 0;
            StartCoroutine(Display_not_Ok_Text(1));
            indicatorsSpawned = !indicatorsSpawned;
            currentFigure.SetActive(false);
        }


        //if all figures has been shown then start from beginning
        if (coordCounter >= coordAmount)
        {
            coordCounter = 0;
        }

        leftInPosition = checkDistance(indicator1, leftHand, shootThreshold);
        rightInPosition = checkDistance(indicator2, rightHand, shootThreshold);


        //if (leftInPosition & debugMode)
        //{
        //    left_light.SetActive(true);

        //} else {
        //    left_light.SetActive(false);
        //}
        //if (rightInPosition & debugMode)
        //{
        //    right_light.SetActive(true);

        //} else {
        //    right_light.SetActive(false);
        //}

        if (leftInPosition & rightInPosition)
        {
            ShootWithInterval(shootIntervalTime, leftHand);
            ShootWithInterval(shootIntervalTime, rightHand);
        }
    }

    void ShootWithInterval(float interval, GameObject shootSource)
    {
        if (shootSource.Equals(leftHand))
        {
            leftShootTimer += Time.deltaTime;
            if (leftShootTimer > shootIntervalTime)
            {
                ProjectileShooterLeft.CreateProjectile(shootSource);
                leftShootTimer = 0;
            }
        }
        else
        {
            rightShootTimer += Time.deltaTime;
            if (rightShootTimer > shootIntervalTime)
            {
                ProjectileShooterRight.CreateProjectile(shootSource);
                rightShootTimer = 0;
            }
        }

    }

    bool checkDistance(GameObject object1, GameObject object2, float threshold)
    {
        float distance = getDistance(object1, object2);
        saveDistanceToVariable(object2, distance);
        if (distance <= threshold)
        {
            return true;
        }
        return false;
    }

    float getDistance(GameObject object1, GameObject object2)
    {
        return Vector3.Distance(object1.transform.position, object2.transform.position); ;
    }

    ArrayList fillCoordList(ArrayList list)
    {
        //actual coordinates
        list.Add(new Coordinates(new Vector3(-13.7f, -4.0f, 0), new Vector3(13.3f, -4.4f, 0)));
        list.Add(new Coordinates(new Vector3(-2.5f, 8.9f, 0), new Vector3(6.9f, -6.9f, 0)));
        list.Add(new Coordinates(new Vector3(-5.9f, 6.6f, 0), new Vector3(5.3f, 5.9f, 0)));
        list.Add(new Coordinates(new Vector3(-12.7f, -6.2f, 0), new Vector3(1.1f, 4.5f, 0)));
        list.Add(new Coordinates(new Vector3(-4.7f, -4.8f, 0), new Vector3(4.6f, -4.7f, 0)));
        list.Add(new Coordinates(new Vector3(-8.8f, -6.5f, 0), new Vector3(1.8f, 8.2f, 0)));
        list.Add(new Coordinates(new Vector3(-12.3f, -13.0f, 0), new Vector3(12.2f, -12.8f, 0)));
        list.Add(new Coordinates(new Vector3(-4.4f, 7.1f, 0), new Vector3(13.0f, -9.2f, 0)));


        //debug coordinates
        //list.Add(new Coordinates(new Vector3(-5, -3, 0), new Vector3(5, -3, 0)));
        //list.Add(new Coordinates(new Vector3(-5, -2, 0), new Vector3(5, -2, 0)));
        //list.Add(new Coordinates(new Vector3(-5, -1, 0), new Vector3(5, -1, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 0, 0), new Vector3(5, 0, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 1, 0), new Vector3(5, 1, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 2, 0), new Vector3(5, 2, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 3, 0), new Vector3(5, 3, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 3, 0), new Vector3(5, 3, 0)));

        return list;
    }

    GameObject SpawnEnemy(GameObject targetPrefab, ArrayList coordList, string side, Vector3 offset, int positionCounter)
    {
        currentFigure = figures[positionCounter];
        currentFigure.SetActive(true);

        if (side.Equals("LEFT"))
        {
            GameObject target = Instantiate(targetPrefab, ((Coordinates)coordList[positionCounter]).coordLeft + offset, this.transform.rotation) as GameObject;
            return target;
        }
        else
        {
            GameObject target = Instantiate(targetPrefab, ((Coordinates)coordList[positionCounter]).coordRight + offset, this.transform.rotation) as GameObject;
            return target;
        }



    }

    public void DecrementEnemyCount()
    {
        amountOfTargets = amountOfTargets - 1;
    }

    public void IncrementScore(int amount)
    {
        score = score + amount;
        scoreText.GetComponent<TextMesh>().text = "Score  " + score.ToString();
    }

    void saveDistanceToVariable(GameObject obj, float dist)
    {
        if (obj.Equals(leftHand))
        {
            leftDistance = dist;
        }
        else
        {
            rightDistance = dist;
        }
    }

    IEnumerator DisplayGreatText(float time)
    {

        text_Great.SetActive(true);
        text_Great.GetComponent<TextMesh>().text = ok_text_arraylist[Random.Range(0, ok_text_arraylist.Count)].ToString();
        yield return new WaitForSeconds(time);
        text_Great.SetActive(false);

    }

    IEnumerator Display_not_Ok_Text(float time)
    {
        GameObject text = Instantiate(text_not_ok);
        yield return new WaitForSeconds(time);
        Destroy(text);

    }

    public void PlayEndGameScene()
    {
        //SceneManager.LoadScene(2);
        if (NiceSceneTransition.instance != null)
        {

            NiceSceneTransition.instance.LoadScene("Scene_EndGame");
        }
        else
        {

            SceneManager.LoadScene("Scene_EndGame", LoadSceneMode.Single);
        }
    }
}


