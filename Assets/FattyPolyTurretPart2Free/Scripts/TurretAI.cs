using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    public enum TurretType
    {
        Single = 1,
        Dual = 2,
        Catapult = 3,
    }
    
    public GameObject currentTarget;
    public Transform turreyHead;

    public float attackDist;
    public float attackDamage;
    public float shootCoolDown;
    private float timer;
    public float loockSpeed;
    public bool canFire;

    [SerializeField] GameObject Quad;

    //public Quaternion randomRot;
    public Vector3 randomRot;
    public Animator animator;

    [Header("[Turret Type]")]
    public TurretType turretType = TurretType.Single;
    
    public Transform muzzleMain;
    public Transform muzzleSub;
    public GameObject muzzleEff;
    public GameObject bullet;
    private bool shootLeft = true;

    private Transform lockOnPos;


    void Start () {
        InvokeRepeating(nameof(ChackForTarget), 0, 0.5f);

        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }
	
	void Update () {
        if (currentTarget != null && canFire)
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentTargetDist > attackDist || currentTarget.transform.parent.GetComponent<EnemyData>().IsDead)
            {
                currentTarget = null;
            }
        }
        else
        {
            IdleRitate();
        }

        timer += Time.deltaTime;
        if (timer >= shootCoolDown)
        {
            if (currentTarget != null && canFire)
            {
                timer = 0;
                
                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                    ShootTrigger();
                }
                else
                {
                    ShootTrigger();
                }
            }
        }
	}

    public void Selected()
    {
        canFire = false;
        Quad.SetActive(true);
    }

    public void EndSlected(bool _canFire)
    {
        canFire = _canFire;
        Quad.SetActive(false);
    }

    private void ChackForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Enemy" && !colls[i].transform.parent.GetComponent<EnemyData>().IsDead)
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    private void FollowTarget() //todo : smooth rotate
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        turreyHead.transform.localRotation = Quaternion.RotateTowards(turreyHead.localRotation, Quaternion.LookRotation(targetDir), 
                                                                        loockSpeed * Time.deltaTime);
    }

    private void ShootTrigger()
    {
        //shotScript.Shoot(currentTarget);
        Shoot(currentTarget);
        //Debug.Log("We shoot some stuff!");
    }
    
    Vector3 CalculateVelocity(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }

    public void IdleRitate()
    {
        bool refreshRandom = false;
        
        if (turreyHead.localRotation != Quaternion.Euler(randomRot))
        {
            turreyHead.localRotation = Quaternion.RotateTowards(turreyHead.transform.localRotation, Quaternion.Euler(randomRot), 
                                                                loockSpeed * Time.deltaTime * 0.02f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {

                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    public void Shoot(GameObject go)
    {
        if (turretType == TurretType.Catapult)
        {
            lockOnPos = go.transform;

            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.localRotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.localRotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.type = TurretType.Catapult;
            projectile.target = lockOnPos;
            projectile.Atk = attackDamage;
        }
        else if(turretType == TurretType.Dual)
        {
            if (shootLeft)
            {
                Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.localRotation);
                GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.localRotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
                projectile.type = TurretType.Dual;
                projectile.Atk = attackDamage/2;
            }
            else
            {
                Instantiate(muzzleEff, muzzleSub.transform.position, muzzleSub.localRotation);
                GameObject missleGo = Instantiate(bullet, muzzleSub.transform.position, muzzleSub.localRotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
                projectile.type = TurretType.Dual;
                projectile.Atk = attackDamage/2;
            }

            shootLeft = !shootLeft;
        }
        else
        {
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.type = TurretType.Single;
            projectile.Atk = attackDamage;
            bullet.SetActive(true);
        }
    }
}
