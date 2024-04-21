using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DeSpawn());
    }

    private IEnumerator DeSpawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
