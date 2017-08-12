using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private DamageType type;

    [SerializeField]
    private float damagePts;

    [SerializeField]
    private float flightTime = 3F;

    private float elapsedTime;

    private Vector3 targetLocation;
    private Vector3 initialLocation;

    public DamageType Type
    {
        get
        {
            return type;
        }
    }

    public float DamagePts
    {
        get
        {
            return damagePts;
        }
    }

    public void Start()
    {
        initialLocation = transform.position;

        if (GameController.Instance != null)
        {
            targetLocation = GameController.Instance.TargetLocation;
        }

        Invoke("AutoDestroy", 15F);
    }

    public void Update()
    {
        Move();
    }

    private void Move()
    {
        elapsedTime += Time.deltaTime;
        transform.position = Vector3.Slerp(initialLocation, targetLocation, elapsedTime / flightTime);
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }
}