using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] LayerMask enemyLayer;
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    public float searchRate = 0.5f;
    public bool manualShootIsOn;
    private float fireCountDown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyLayerName = "Enemy";
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] float manualShootRangeMod;



    void Start()
    {
        enemyLayer = LayerMask.GetMask(enemyLayerName);
        //UpdateTargetAuto();
    }

    void UpdateTargetAuto()
    {
        RaycastHit[] Enemies;
        //cast a sphere and get every object in range inside the enemies layer
        Enemies = Physics.SphereCastAll(transform.position, range, Vector3.up, 0.01f, enemyLayer); //GameObject.FindGameObjectsWithTag(enemyTag);
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        float distanceToEnemy;

        //Check which enemy is closer to turret
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

        //Check if closest enemy is within range
        if (shortestDistance <= range)
        {
            target = nearestEnemy;
        }
        else return;
    }
    void UpdateTargetManual()
    {
        //Get a enemy by clicking on it
        RaycastHit enemyHitted;
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(cameraRay, out enemyHitted, Mathf.Infinity, enemyLayer))
        {
            Debug.Log("Not clicked on anything");
            return;
        }

        //if a enemy was hitted, check if is in range
        Transform enemy = enemyHitted.transform;
        float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);
        if (distanceToEnemy <= range * manualShootRangeMod)
        {
            target = enemy;
        }
    }
    float GetTargetDistance()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    void Update()
    {
        //Search for target if button is down
        if (manualShootIsOn)
        {
            if (Input.GetMouseButton(0))
            {
                UpdateTargetManual();
                if (target == null)
                    Debug.Log("target get failed");
            }

            if (target == null)
            {
                return;
            }
        }
        else if (target == null) //Search for target if there is none
        {
            //If timer is done, search for target (spherecast), else, advance timer
            if (fireCountDown > 0)
            {
                //uses fire timer so it doesn't spherecast every frame (optimization)
                AdvanceFireCountdown();
            }
            else
            {
                //Debug.Log("Searching Target");
                UpdateTargetAuto();

                fireCountDown = searchRate;
            }
            return;
        }

        //Check if target is still in range
        if (!manualShootIsOn && GetTargetDistance() > range)
        {
            target = null;
            return;
        }

        //Rotate Turret Head to Target
        RotateToTarget();

        //Shoot if timer ended
        if (fireCountDown <= 0f)
        {
            //Debug.Log("working");
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

    void RotateToTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.Seek(target);
    }

    public void TurnManualShoot()
    {
        manualShootIsOn = !manualShootIsOn;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}