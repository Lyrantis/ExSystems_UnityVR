using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed = 10.0f;
    private void Awake()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
        Destroy(this);
    }
}
