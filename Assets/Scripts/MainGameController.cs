using UnityEngine;
using System.Collections;

public class MainGameController : MonoBehaviour {

    public PositionIndicatorSpawner IndicatorSpawner;
    public float activeTimeSeconds = 1f;
    public GameObject LeftHand;
    public GameObject RightHand;


    private float spawnTimer = 0f;
    private bool indicatorsSpawned = false;
    private GameObject indicator1;
    private GameObject indicator2;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!indicatorsSpawned)
        {
            indicator1 = IndicatorSpawner.SpawnIndicator(new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0));
            indicator2 = IndicatorSpawner.SpawnIndicator(new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0));
            indicatorsSpawned = !indicatorsSpawned;
        }
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= activeTimeSeconds)
        {
            Destroy(indicator1, 0);
            Destroy(indicator2, 0);
            spawnTimer = 0;
            indicatorsSpawned = !indicatorsSpawned;
        }

    }
}
