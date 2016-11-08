using UnityEngine;
using System.Collections;

public class MainGameController : MonoBehaviour
{

    public PositionIndicatorSpawner IndicatorSpawner;
    public ProjectileShooter ProjectileShooterLeft;
    public ProjectileShooter ProjectileShooterRight;
    public GameConfig Config;
        
    public GameObject leftHand;
    public GameObject rightHand;

    private float shootThreshold;
    private float activeTimeSeconds;
    private float shootIntervalTime;
    
    private float leftShootTimer = 0f;
    private float rightShootTimer = 0f;

    private float spawnTimer = 0f;
    private bool indicatorsSpawned = false;
    private bool leftProjectileSpawned = false;
    private bool rightProjectileSpawned = false;

    private GameObject indicator1;
    private GameObject indicator2;
    private bool leftInPosition;
    private bool rightInPosition;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!indicatorsSpawned)
        {
            indicator1 = IndicatorSpawner.SpawnIndicator(new Vector3(Random.Range(-6.0f, -2.0f), Random.Range(-8.0f, 8.0f), 0));
            indicator2 = IndicatorSpawner.SpawnIndicator(new Vector3(Random.Range(2.0f, 6.0f), Random.Range(-8.0f, 8.0f), 0));
            indicatorsSpawned = !indicatorsSpawned;
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

        if(leftInPosition & rightInPosition)
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
            if(leftShootTimer > shootIntervalTime)
            {
                ProjectileShooterLeft.CreateProjectile(shootSource);
                leftShootTimer = 0;
            }
        }else
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
        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);
        if(distance <= threshold)
        {
            return true;
        }
        return false;
    }
}
