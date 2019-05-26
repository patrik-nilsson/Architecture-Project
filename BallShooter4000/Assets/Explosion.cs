using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float knockBackStrength;
    public float knockBackRadius;
    ParticleSystem hitParticles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Zombunny" || collision.gameObject.name == "Zombunny(Clone)" 
            || collision.gameObject.name == "Hellephant" || collision.gameObject.name == "Hellephant(Clone)" || 
            collision.gameObject.name == "ZomBear" || collision.gameObject.name == "ZomBear(Clone)")
        {
            hitParticles = GetComponentInChildren<ParticleSystem>();

            Collider[] colliders = Physics.OverlapSphere(transform.position, knockBackRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody rb = colliders[i].GetComponent<Rigidbody>();

                if (rb != null)
                {
                    EnemyHealth enemyHealth = rb.gameObject.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        hitParticles.transform.position = transform.position;
                        hitParticles.Play();
                        enemyHealth.TakeDamage(50, rb.gameObject.transform.position);
                    }
                    rb.AddExplosionForce(knockBackStrength, transform.position, knockBackRadius, 0.0f, ForceMode.Impulse);

                }
            }
        }
    }
}
