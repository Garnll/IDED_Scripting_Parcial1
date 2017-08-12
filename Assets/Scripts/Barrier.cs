using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Barrier : MonoBehaviour
{
    [SerializeField]
    private DamageType type;

    [SerializeField]
    private float maxHp;

    private float currentHP;

    public float CurrentHP
    {
        get
        {
            return currentHP;
        }

        set
        {
            currentHP = value;
        }
    }

    public void TakeDamage(RayBeam ray)
    {
        currentHP -= ray.DamagePts;


        if (currentHP <= 0)
        {
            Destroy();
        }
    }

    public void TakeDamage(Projectile projectile)
    {
        switch (projectile.Type)
        {
            case DamageType.Normal:
                if (type == DamageType.Normal)
                {
                    currentHP -= projectile.DamagePts;
                }
                if (type == DamageType.Medium)
                {
                    currentHP -= projectile.DamagePts + ((projectile.DamagePts * 5) / 100);
                }
                if (type == DamageType.Heavy)
                {
                    currentHP -= projectile.DamagePts + ((projectile.DamagePts * 10) / 100);
                }
                break;

            case DamageType.Medium:
                if (type == DamageType.Normal)
                {
                    currentHP -= projectile.DamagePts - ((projectile.DamagePts * 5) / 100);
                }
                if (type == DamageType.Medium)
                {
                    currentHP -= projectile.DamagePts;
                }
                if (type == DamageType.Heavy)
                {
                    currentHP -= projectile.DamagePts + ((projectile.DamagePts * 5) / 100);
                }
                break;

            case DamageType.Heavy:
                if (type == DamageType.Normal)
                {
                    currentHP -= projectile.DamagePts - ((projectile.DamagePts * 10) / 100);
                }
                if (type == DamageType.Medium)
                {
                    currentHP -= projectile.DamagePts - ((projectile.DamagePts * 5) / 100);
                }
                if (type == DamageType.Heavy)
                {
                    currentHP -= projectile.DamagePts;
                }
                break;

            default:
                currentHP -= projectile.DamagePts;
            break;
        }


        if (currentHP <= 0)
        {
            Destroy();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.GetType() == typeof(Projectile))
        {
            Projectile otherHit = other.gameObject.GetComponent<Projectile>();
            TakeDamage(otherHit);
            Debug.Log("Golpeado");
        }
    }

    // Use this for initialization
    private void Start()
    {
        currentHP = maxHp;
    }

    private void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}