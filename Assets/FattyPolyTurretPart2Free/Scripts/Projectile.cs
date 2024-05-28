using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public TurretAI.TurretType type = TurretAI.TurretType.Single;
    public Transform target;
    public bool lockOn;

    public float Atk;

    public float speed = 1;
    public float turnSpeed = 1;
    public bool catapult;

    public float knockBack = 0.1f;
    public float boomTimer = 1;

    public VFXPool explosion;

    private void OnEnable()
    {
        if (catapult)
        {
            lockOn = true;
        }
    }

    private void Update()
    {

        if (type == TurretAI.TurretType.Single) return;

        if (type == TurretAI.TurretType.Catapult)
        {
            if (lockOn)
            {

                this.transform.DOJump(target.transform.position, speed, 1, 0.3f);
                lockOn = false;
            }
        }else if(type == TurretAI.TurretType.Dual)
        {
            if (!target.gameObject.activeInHierarchy)
            {
                //Destroy(gameObject);
                ObjectPooler.EnqueueObject<Projectile>(this, this.name);
            }
            Vector3 dir = target.position - transform.position;
            //float distThisFrame = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(-transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);

            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(-newDirection);

        }
    }

    Vector3 CalculateCatapult(Vector3 target, Vector3 origen, float time)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && other.transform == target && type == TurretAI.TurretType.Dual)
        {
            Explosion(other.transform);
            other.transform.parent.GetComponent<EnemyData>().GetDamage(Atk, type);
        }

        if(other.tag == "Plane" && type == TurretAI.TurretType.Catapult)
        {
            Explosion(this.transform, Atk);
        }

        if (other.tag == "Enemy" && type == TurretAI.TurretType.Single)
        {
            other.transform.parent.GetComponent<EnemyData>().GetDamage(Atk, type);
        }
    }

    public void Explosion(Transform _transform)
    {
        VFXPool vfx = ObjectPooler.DequeueObject(explosion.name, explosion);
        vfx.transform.SetPositionAndRotation(_transform.position, transform.rotation);
        vfx.gameObject.SetActive(true);
        ObjectPooler.EnqueueObject<Projectile>(this, this.name);
    }

    public void Explosion(Transform _transform, float _Atk)
    {
        VFXPool p = Instantiate(explosion, _transform.position, transform.rotation);
        p.GetComponentInChildren<NukeBoom>().Atk = _Atk;
        Destroy(gameObject);
    }
}
