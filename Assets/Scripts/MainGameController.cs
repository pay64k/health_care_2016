using UnityEngine;
using System.Collections;

public class MainGameController : MonoBehaviour
{

    public PositionIndicatorSpawner IndicatorSpawner;
    public ProjectileShooter ProjectileShooterLeft;
    public ProjectileShooter ProjectileShooterRight;
    public GameConfig Config;
        
    public GameObject LeftHand;
    public GameObject RightHand;

    private float shootThreshold;
    private float activeTimeSeconds;
    private float shootIntervalTime;
    
    private float spawnTimer = 0f;
    private float shootTimer = 0f;
    private bool indicatorsSpawned = false;
    private bool leftProjectileSpawned = false;
    private bool rightProjectileSpawned = false;
    private GameObject indicator1;
    private GameObject indicator2;

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

        float distanceInd1LeftHand = Vector3.Distance(indicator1.transform.position,
            LeftHand.transform.position);
        float distanceInd2RightHand = Vector3.Distance(indicator2.transform.position,
            RightHand.transform.position);

        if (distanceInd1LeftHand <= shootThreshold )
        {
            left_light.SetActive(true);
            ShootWithInterval(shootIntervalTime, LeftHand);

        } else {
            left_light.SetActive(false);
        }
        if (distanceInd2RightHand <= shootThreshold)
        {
            right_light.SetActive(true);
            ShootWithInterval(shootIntervalTime, RightHand);
            
        } else {
            right_light.SetActive(false);
        }

    }

    void ShootWithInterval(float interval, GameObject shootSource)
    {
        shootTimer += Time.deltaTime;
        if(shootTimer > shootIntervalTime)
        {
            ProjectileShooterLeft.CreateProjectile(shootSource);
            shootTimer = 0;
        }
    }

}
