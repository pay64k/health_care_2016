using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour {

    public GameObject projectilePrefab;
    public float travelSpeed;
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject CreateProjectile(GameObject projectileSource)
    {
        GameObject projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = transform.position + projectileSource.transform.forward;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = projectileSource.transform.up * travelSpeed;
        return projectile;
    }
}
