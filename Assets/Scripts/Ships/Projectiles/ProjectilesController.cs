using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ProjectilesController : MonoBehaviour
{

    private ParticleSystem projectileSystem;

    void Start()
    {
        projectileSystem = GetComponent<ParticleSystem>();
    }

    public void Shoot()
    {
        projectileSystem.Emit(1);
    }
}
