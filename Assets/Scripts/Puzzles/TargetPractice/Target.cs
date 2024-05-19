using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private bool IsMoving = false;
    private int ScoreValue = 1;

    public void Init(bool shouldMove, int points, Vector3 velocity)
    {
        IsMoving = shouldMove;
        ScoreValue = points;

        if (IsMoving)
        {
            gameObject.GetComponent<Rigidbody>().velocity = velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            //Add Player Points, 
            Destroy(this.gameObject);
            Destroy(this);
        }
    }
}
