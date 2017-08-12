using UnityEngine;

public delegate void OnBaseDestroyed(Base thisBase);

public delegate void OnTurnFinished();

[RequireComponent(typeof(Collider))]
public class Base : MonoBehaviour
{
    public OnBaseDestroyed onBaseDestroyed;
    public OnTurnFinished onTurnFinished;

    [SerializeField]
    private float maxHP = 500F;

    [SerializeField]
    private Catapult catapult;

    [SerializeField]
    private RayBeam rayBeam;

    private float currentHP;
    private int turnsPassed;
    private int repairUsed;

    private bool canAttack;
    private bool defending;

    public void EnableTurn()
    {
        enabled = true;
        canAttack = true;
    }

    public void AttackWithCatapult()
    {
        catapult.Fire();
        turnsPassed += 1;
        onTurnFinished();
        Debug.Log("Used Attack with catapult");
    }

    public void AttackWithRay()
    {
        if (turnsPassed < 3)
        {
            return;
        }
        turnsPassed = 0;
        rayBeam.Fire();
        onTurnFinished();
        Debug.Log("Used Attack with ray");
    }

    public void Repair()
    {
        if (repairUsed >= 2)
        {
            return;
        }
        repairUsed += 1;
        turnsPassed += 1;

        currentHP += ((25 * maxHP) / 100);

        onTurnFinished();
        Debug.Log("Used repair");
    }

    public void Defend()
    {
        defending = true;
        turnsPassed += 1;
        onTurnFinished();
        Debug.Log("Used defend");
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

    public void TakeDamage(RayBeam ray)
    {
        if (defending)
        {
            currentHP -= ray.DamagePts - ((25 * ray.DamagePts) / 100);
        }
        else
        {
            currentHP -= ray.DamagePts;
        }


        if (currentHP <= 0)
        {
            Destroy();
        }
    }

    private void TakeDamage(Projectile projectile)
    {
        switch (projectile.Type)
        {
            case DamageType.Normal:
                if (defending)
                {
                    currentHP -= projectile.DamagePts - ((25 * projectile.DamagePts) / 100);
                }
                else
                {
                    currentHP -= projectile.DamagePts;
                }
                break;

            case DamageType.Medium:
                if (defending)
                {
                    currentHP -= projectile.DamagePts + ((5 * projectile.DamagePts) / 100) - ((25 * projectile.DamagePts) / 100);
                }
                else
                {
                    currentHP -= projectile.DamagePts + ((5 * projectile.DamagePts) / 100);
                }
                break;

            case DamageType.Heavy:
                if (defending)
                {
                    currentHP -= projectile.DamagePts + ((10 * projectile.DamagePts) / 100) - ((25 * projectile.DamagePts) / 100);
                }
                else
                {
                    currentHP -= projectile.DamagePts + ((10 * projectile.DamagePts) / 100);
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

    // Use this for initialization
    private void Start()
    {
        currentHP = maxHP;
        turnsPassed = 0;
        repairUsed = 0;

        enabled = false;
        canAttack = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canAttack)
        {

        }
    }

    private void Destroy()
    {
        onBaseDestroyed(this);
    }
}