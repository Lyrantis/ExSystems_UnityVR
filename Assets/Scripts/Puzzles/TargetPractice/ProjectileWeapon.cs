using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField]
    public GameObject projectileToSpawn;

    [SerializeField]
    public GameObject projectileSpawnPoint;

    [SerializeField]
    public float delayBetweenShots = 0.2f;

    private bool canFire = true;

    private bool isLoaded = false;

    public void OnTriggerPressed()
    {
        if (canFire & isLoaded)
        {
            Fire();
            GetComponent<AudioSource>().Play();
            canFire = false;
            StartCoroutine(AttackDelay());
        }
    }
    
    private void Fire()
    {
        GameObject temp = Instantiate(projectileToSpawn, projectileSpawnPoint.transform);
    }
    
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(delayBetweenShots);

        canFire = true;
    }

    public void SetLoaded(bool loaded)
    {
        isLoaded = loaded;
    }

}
