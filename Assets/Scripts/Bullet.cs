using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public int damage = 1;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);


    }

    void HitTarget()
    {
        Enemy enemy = target.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.Damage(damage);
        }

        Destroy(gameObject);
    }

    //onCo
}
