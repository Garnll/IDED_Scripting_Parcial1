using UnityEngine;

public class RayBeam : MonoBehaviour
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float rayDistance = 50F;

    public Vector3 targetLocation;
    private Vector3 targetDirection;

    public float DamagePts
    {
        get
        {
            return damage;
        }
    }

    public void Fire()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, targetDirection);

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
           if (hit.GetType() == typeof(Barrier))
            {
                hit.collider.gameObject.GetComponent<Barrier>().TakeDamage(this);
                Debug.Log("He dado a barrera");
            }
            if (hit.GetType() == typeof(Base))
            {
                hit.collider.gameObject.GetComponent<Base>().TakeDamage(this); ;
                Debug.Log("He dado a base");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, targetDirection);
    }
}