using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 25f;
    public float lifeTime = 5;
    public int minDmg = 20;
    public int maxDmg = 35;

    public GameObject bulletHolePrefab;


    void Start()
    {
        
    }
    
    
    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

   
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().Damage(Random.Range(minDmg, maxDmg));
        }else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Damage(Random.Range(minDmg, maxDmg));
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(bulletHolePrefab, transform.position, Quaternion.identity);

        }
        Destroy(gameObject);
    }
}
