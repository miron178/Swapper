using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetArmoured : MonoBehaviour
{
    private Transform armouredTarget;
    public float range = 15f;
    public string armouredTag = "Armoured";

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
        GameObject[] Armoured = GameObject.FindGameObjectsWithTag(armouredTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject Player in Armoured)
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
            armouredTarget = nearestEnemy.transform;
        }
        else
        {
            armouredTarget = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (armouredTarget == null)
            return;
        Vector3 armoured = armouredTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(armoured);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        bulletPrefab.GetComponent<BulletArmoured>().enabled = true;
        bulletPrefab.GetComponent<BulletSlime>().enabled = false;
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletArmoured bulletArmoured = bulletGO.GetComponent<BulletArmoured>();

        if (bulletArmoured != null)
        {
            bulletArmoured.Seek(armouredTarget);
            return;
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
