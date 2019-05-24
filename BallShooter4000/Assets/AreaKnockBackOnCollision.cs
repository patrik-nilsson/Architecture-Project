using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaKnockBackOnCollision : MonoBehaviour
{
    private float knockBackStrength;
    private float knockBackRadius;

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, knockBackRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(knockBackStrength, transform.position, knockBackRadius);
            }
        }
    }
}
