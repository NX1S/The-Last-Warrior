using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] GameObject HitEffect;
    [SerializeField] float Damage = 5;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDamage(float damage)
    {
        Damage = damage;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Environment") || collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            //GameObject Effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
            GameObject Enemy = collision.collider.gameObject;
            Enemy.GetComponent<EnemyAi>().TakeDamage(Damage);
            //Destroy(Effect, 0.5f);
            Destroy(gameObject, 0.3f);
        }
    }
}
