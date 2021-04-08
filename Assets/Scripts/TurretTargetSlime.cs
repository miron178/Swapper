using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetSlime : MonoBehaviour
{

    private Transform slimeTarget;
    public float range = 15f;
    public string slimeTag = "Player";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] Slime = GameObject.FindGameObjectsWithTag(slimeTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject Player in Slime)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, Player.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = Player;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            slimeTarget = nearestEnemy.transform;
        }
        else
        {
            slimeTarget = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slimeTarget == null)
            return;
        Vector3 dir = slimeTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        bulletPrefab.GetComponent<BulletArmoured>().enabled = false;
        bulletPrefab.GetComponent<BulletSlime>().enabled = true;
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletSlime bulletSlime = bulletGO.GetComponent<BulletSlime>();

        if (bulletSlime != null)
        {
            bulletSlime.Seek(slimeTarget);
            return;
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
