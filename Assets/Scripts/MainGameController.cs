using UnityEngine;
using System.Collections;

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

    private ArrayList coordList;
    private Coordinates coord;
    private int coordCounter;
    private int coordAmount;

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

        coordList = new ArrayList();
        coordList = fillCoordList(coordList);
        coordCounter = 0;
        coordAmount = coordList.Count;

    }

    void Update()
    {
        if (!indicatorsSpawned)
        {
            //indicator1 = IndicatorSpawner.SpawnIndicator(new Vector3(Random.Range(-6.0f, -2.0f), Random.Range(-8.0f, 8.0f), 0));
            //indicator2 = IndicatorSpawner.SpawnIndicator(new Vector3(Random.Range(2.0f, 6.0f), Random.Range(-8.0f, 8.0f), 0));

            indicator1 = IndicatorSpawner.SpawnIndicator(((Coordinates)coordList[coordCounter]).coordLeft);
            indicator2 = IndicatorSpawner.SpawnIndicator(((Coordinates)coordList[coordCounter]).coordRight);

            //indicator1.transform.Rotate(Vector3.up, 120f);

            GameObject newObject1 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordLeft + new Vector3(0, 10), this.transform.rotation) as GameObject;
            GameObject newObject2 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordLeft + new Vector3(0, 12.5f), this.transform.rotation) as GameObject;
            GameObject newObject3 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordLeft + new Vector3(0, 15), this.transform.rotation) as GameObject;
            GameObject newObject4 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordLeft + new Vector3(0, 17.5f), this.transform.rotation) as GameObject;

            GameObject newObject5 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordRight + new Vector3(0, 10), this.transform.rotation) as GameObject;
            GameObject newObject6 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordRight + new Vector3(0, 12.5f), this.transform.rotation) as GameObject;
            GameObject newObject7 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordRight + new Vector3(0, 15), this.transform.rotation) as GameObject;
            GameObject newObject8 = Instantiate(targetPrefab, ((Coordinates)coordList[coordCounter]).coordRight + new Vector3(0, 17.5f), this.transform.rotation) as GameObject;

            //check if all targets are shot down
            //if not reset targets if timer reaches activeTimeSeconds
            //if yes set timer to activeTimeSeconds 

            coordCounter++;
            indicatorsSpawned = !indicatorsSpawned;

            //if all figures has been shown then start from beginning
            if (coordCounter >= coordAmount)
            {
                coordCounter = 0;
            }
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= activeTimeSeconds)
        {
            Destroy(indicator1, 0);
            Destroy(indicator2, 0);
            spawnTimer = 0;
            indicatorsSpawned = !indicatorsSpawned;
        }

        //float distanceInd1LeftHand = Vector3.Distance(indicator1.transform.position,
        //    leftHand.transform.position);
        //float distanceInd2RightHand = Vector3.Distance(indicator2.transform.position,
        //    rightHand.transform.position);

        leftInPosition = checkDistance(indicator1, leftHand, shootThreshold);
        rightInPosition = checkDistance(indicator2, rightHand, shootThreshold);


        if (leftInPosition)
        {
            left_light.SetActive(true);
            //ShootWithInterval(shootIntervalTime, LeftHand);

        } else {
            left_light.SetActive(false);
        }
        if (rightInPosition)
        {
            right_light.SetActive(true);
            //ShootWithInterval(shootIntervalTime, RightHand);

        } else {
            right_light.SetActive(false);
        }

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
        } else
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
        // left and right indicator position
        list.Add(new Coordinates(new Vector3(-5, -3, 0), new Vector3(5, -3, 0)));
        list.Add(new Coordinates(new Vector3(-5, -2, 0), new Vector3(5, -2, 0)));
        list.Add(new Coordinates(new Vector3(-5, -1, 0), new Vector3(5, -1, 0)));
        list.Add(new Coordinates(new Vector3(-5, 0, 0), new Vector3(5, 0, 0)));
        list.Add(new Coordinates(new Vector3(-5, 1, 0), new Vector3(5, 1, 0)));
        list.Add(new Coordinates(new Vector3(-5, 2, 0), new Vector3(5, 2, 0)));
        list.Add(new Coordinates(new Vector3(-5, 3, 0), new Vector3(5, 3, 0)));
        return list;
    }

    void saveDistanceToVariable(GameObject obj, float dist)
    {
        if (obj.Equals(leftHand))
        {
            leftDistance = dist;
        }else
        {
            rightDistance = dist;
        }
    }

    public float Bla()
    {
        return coordCounter;
    }
}
