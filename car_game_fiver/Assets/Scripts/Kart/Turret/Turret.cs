using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Turret : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] LayerMask enemyLayer;
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    public float searchRate = 0.5f;
    private float fireCountDown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyLayerName = "Enemy";
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint;



    void Start()
    {
        enemyLayer = LayerMask.GetMask(enemyLayerName);
        UpdateTarget();
    }

    void UpdateTarget()
    {
        RaycastHit[] Enemies;
        //cast a sphere and get every object in range inside the enemies layer
        Enemies = Physics.SphereCastAll(transform.position, range, Vector3.up, 0.01f, enemyLayer); //GameObject.FindGameObjectsWithTag(enemyTag);
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        float distanceToEnemy;

        foreach (var Enemy in Enemies)
        {
            distanceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                Debug.Log("UpdateTarget");
                shortestDistance = distanceToEnemy;
                nearestEnemy = Enemy.transform;
            }
        }

        if (shortestDistance <= range)
        {
            target = nearestEnemy;
        }
        else return;
    }
    float GetTargetDistance()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    void Update()
    {
        if (target == null)
        {
            if (fireCountDown > 0) //uses fire timer so it doesn't spherecast every frame (optimization)
            {
                AdvanceFireCountdown();
            }
            else
            {
                Debug.Log("Searching Target");
                UpdateTarget();
                fireCountDown = searchRate;
            }
            return;
        }
        if (GetTargetDistance() > range)
        {
            target = null;
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0f)
        {
            Debug.Log("working");
            Shoot();
        }
        AdvanceFireCountdown();
    }
    void AdvanceFireCountdown()
    {
        if (fireCountDown <= 0f)
            fireCountDown = 1f / fireRate;
        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        //if(bullet != null)
        Debug.Log("bullet found");
        bullet.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}