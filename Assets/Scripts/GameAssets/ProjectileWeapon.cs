using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField]
    public GameObject projectileToSpawn;
    
    [SerializeField]
    public float delayBetweenShots = 0.2f;

    private bool canFire = true;

    public void OnTriggerPressed()
    {
        if (canFire)
        {
            Fire();
            canFire = false;
            StartCoroutine(AttackDelay());
        }
    }
    
    private void Fire()
    {
        Instantiate(projectileToSpawn);
    }
    
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(delayBetweenShots);

        canFire = true;
    }

}
