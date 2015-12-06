using UnityEngine;
using System.Collections;

public class ProjectilesController : MonoBehaviour
{

    public GameObject nozzle;
    public GameObject projectile;
    private ParticleSystem projectileSystem;

    void Start()
    {
        GameObject instantiatedProjectileSystem = (GameObject)Instantiate(projectile, nozzle.transform.position, nozzle.transform.rotation);
        instantiatedProjectileSystem.transform.parent = nozzle.transform;
        projectileSystem = instantiatedProjectileSystem.GetComponent<ParticleSystem>();
    }

    public void Shoot()
    {
        projectileSystem.Emit(1);
    }
}
