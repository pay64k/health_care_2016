using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {

    public GameObject projectilePrefab;
    public GameConfig Config;
    public AudioClip [] clips;

    private float travelSpeed;
    private float projectileSpread;

	void Start () {
        travelSpeed = Config.ProjectileTravelSpeed;
        projectileSpread = Config.projectileSpawnSpread;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateProjectile(GameObject projectileSource)
    {
        GameObject projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = transform.position + projectileSource.transform.forward + new Vector3(Random.Range(-projectileSpread, projectileSpread),0,0);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = projectileSource.transform.up * travelSpeed;
        playShootSound();
    }
    public void playShootSound()
    {
        //if (GetComponent<AudioSource>().isPlaying)
        //{
        //    return;
        //}
        int clipPick = Random.Range(0, clips.Length);
        GetComponent<AudioSource>().clip = clips[clipPick];
        GetComponent<AudioSource>().Play();
    }
}
