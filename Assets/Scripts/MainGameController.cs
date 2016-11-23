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
        ok_text_arraylist.Add("Amazing");
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
            PlayEndGameScene();
        }

        double remainingTime = gameTime - currentGameTime;
        if (remainingTime >= 0)
        {
            timeRemainingText.GetComponent<TextMesh>().text = remainingTime.ToString("F1");
        }

        if (!indicatorsSpawned)
        {
            //indicator1 = IndicatorSpawner.SpawnIndicator(new Vector3(Random.Range(-6.0f, -2.0f), Random.Range(-8.0f, 8.0f), 0));
            //indicator2 = IndicatorSpawner.SpawnIndicator(new Vector3(Random.Range(2.0f, 6.0f), Random.Range(-8.0f, 8.0f), 0));

            indicator1 = IndicatorSpawner.SpawnIndicator(((Coordinates)coordList[coordCounter]).coordLeft, leftHand);
            indicator2 = IndicatorSpawner.SpawnIndicator(((Coordinates)coordList[coordCounter]).coordRight, rightHand);

            //indicator1.transform.Rotate(Vector3.up, 120f);


            //GameObject newObject1 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordLeft + new Vector3(0, 10), this.transform.rotation) as GameObject;
            //GameObject newObject2 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordLeft + new Vector3(0, 12.5f), this.transform.rotation) as GameObject;
            //GameObject newObject3 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordLeft + new Vector3(0, 15), this.transform.rotation) as GameObject;
            //GameObject newObject4 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordLeft + new Vector3(0, 17.5f), this.transform.rotation) as GameObject;

            //GameObject newObject5 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordRight + new Vector3(0, 10), this.transform.rotation) as GameObject;
            //GameObject newObject6 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordRight + new Vector3(0, 12.5f), this.transform.rotation) as GameObject;
            //GameObject newObject7 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordRight + new Vector3(0, 15), this.transform.rotation) as GameObject;
            //GameObject newObject8 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordRight + new Vector3(0, 17.5f), this.transform.rotation) as GameObject;

            for (float i = 10f; i <= 19f; i = i + 3f)
            {
                targets.Add(SpawnEnemy(targetPrefab, coordList, "LEFT", new Vector3(0, i), coordCounter));
                targets.Add(SpawnEnemy(targetPrefab, coordList, "RIGHT", new Vector3(0, i), coordCounter));
                amountOfTargets = amountOfTargets + 2;
            }

            //check if all targets are shot down
            //if not reset targets if timer reaches activeTimeSeconds
            //if yes set timer to activeTimeSeconds 

            coordCounter++;
            indicatorsSpawned = !indicatorsSpawned;

        }

        if (amountOfTargets == 0)
        {
            Destroy(indicator1, 0);
            Destroy(indicator2, 0);
            spawnTimer = 0;
            targets.Clear();
            //StartCoroutine(Wait(3));
            //checkPostion = !checkPostion;
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
            //StartCoroutine(Wait(3));
            //checkPostion = !checkPostion;
            StartCoroutine(Display_not_Ok_Text(1));
            indicatorsSpawned = !indicatorsSpawned;
            currentFigure.SetActive(false);
        }


        //if all figures has been shown then start from beginning
        if (coordCounter >= coordAmount)
        {
            coordCounter = 0;
        }

        //float distanceInd1LeftHand = Vector3.Distance(indicator1.transform.position,
        //    leftHand.transform.position);
        //float distanceInd2RightHand = Vector3.Distance(indicator2.transform.position,
        //    rightHand.transform.position);

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


        // left and right indicator position
        //list.Add(new Coordinates(new Vector3(-5, -3, 0), new Vector3(5, -3, 0)));
        //list.Add(new Coordinates(new Vector3(-5, -2, 0), new Vector3(5, -2, 0)));
        //list.Add(new Coordinates(new Vector3(-5, -1, 0), new Vector3(5, -1, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 0, 0), new Vector3(5, 0, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 1, 0), new Vector3(5, 1, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 2, 0), new Vector3(5, 2, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 3, 0), new Vector3(5, 3, 0)));
        //list.Add(new Coordinates(new Vector3(-5, 3, 0), new Vector3(5, 3, 0)));

        //list.Add(new Coordinates(new Vector3(-7.6f, -6.3f, 4.8f), new Vector3(9.5f, -5.8f, 2.1f)));
        //list.Add(new Coordinates(new Vector3(0.0f, -3.8f, 0.9f), new Vector3(6.6f, -11, 1.1f)));
        //list.Add(new Coordinates(new Vector3(-5.6f, 0.7f, -1.6f), new Vector3(6.0f, -7.5f, -1.6f)));
        //list.Add(new Coordinates(new Vector3(-7.1f, -10, 0.8f), new Vector3(4.9f, 0.4f, -1.1f)));
        //list.Add(new Coordinates(new Vector3(-7.4f, -8.2f, 0.6f), new Vector3(4.8f, -0.4f, -0.9f)));
        //list.Add(new Coordinates(new Vector3(-4, 2, -0.4f), new Vector3(5, 0.9f, -1.7f)));
        //list.Add(new Coordinates(new Vector3(-4.1f, -12.9f, 0.5f), new Vector3(4.9f, -12.4f, 0.2f)));
        //list.Add(new Coordinates(new Vector3(-8.5f, -4.6f, 0.4f), new Vector3(2.5f, -4.8f, 1.1f)));
        //list.Add(new Coordinates(new Vector3(-4.6f, -4.2f, 0.2f), new Vector3(4.7f, -4.4f, -0.5f)));
        //list.Add(new Coordinates(new Vector3(-3.0f, -3.7f, -8.1f), new Vector3(3.5f, -3.8f, -8.5f)));

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
        SceneManager.LoadScene(2);

    }
}


