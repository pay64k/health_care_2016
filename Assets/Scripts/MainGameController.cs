using UnityEngine;
using System.Collections;

public class MainGameController : MonoBehaviour
{

    public PositionIndicatorSpawner IndicatorSpawner;
    public float activeTimeSeconds = 1f;
    public GameObject LeftHand;
    public GameObject RightHand;
    public float shootThreshold = 0.1f;

    private float spawnTimer = 0f;
    private bool indicatorsSpawned = false;
    private GameObject indicator1;
    private GameObject indicator2;
    //Variables for debugging
    private GameObject left_light;
    private GameObject right_light;
    void Start()
    {
        left_light = GameObject.Find("Debugging/left_light");
        right_light = GameObject.Find("Debugging/right_light");
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
        //Debug.Log(distanceInd1LeftHand);

        if (distanceInd1LeftHand <= shootThreshold) { left_light.SetActive(true); } else { left_light.SetActive(false); }
        if (distanceInd2RightHand <= shootThreshold) { right_light.SetActive(true); } else { right_light.SetActive(false); }


        



    }
}
