using UnityEngine;
using System.Collections;

public class ProjectilesController : MonoBehaviour
{

    public GameObject nozzle;
    public GameObject projectile;

    public float reloadTime = 1f;

    private bool reloading = false;
    private ParticleSystem projectileSystem;

    private ProjectileSettings projectileSettings;



    void Start()
    {
        GameObject instantiatedProjectileSystem = (GameObject)Instantiate(projectile, nozzle.transform.position, nozzle.transform.rotation);
        instantiatedProjectileSystem.transform.parent = nozzle.transform;
        projectileSystem = instantiatedProjectileSystem.GetComponent<ParticleSystem>();
        projectileSettings = instantiatedProjectileSystem.GetComponent<ProjectileSettings>();
    }

    IEnumerator WaitForReload()
    {
        reloading = true;
        if (projectileSettings)
            yield return new WaitForSeconds(projectileSettings.reloadTime);
        else
            yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }

    public void Shoot()
    {
        Debug.Log("Shooting");
        if (!reloading)
        {
            projectileSystem.Emit(1);
            StartCoroutine("WaitForReload");
        }
    }
}
