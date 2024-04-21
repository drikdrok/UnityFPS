using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 25f;
    public float lifeTime = 5;
    public int damage = 30;

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

   
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().Damage(damage);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Instantiate(bulletHolePrefab, transform.position, Quaternion.identity);

        }
        Destroy(gameObject);
    }
}
