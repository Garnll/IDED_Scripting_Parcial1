using UnityEngine;

public class Catapult : MonoBehaviour
{
    [SerializeField]
    private GameObject projectileToFire;

    [SerializeField]
    private Transform projectileSpawnTranform;

    private GameObject copy;

    public void Fire()
    {
        copy = Instantiate(projectileToFire, projectileSpawnTranform);
    }
}