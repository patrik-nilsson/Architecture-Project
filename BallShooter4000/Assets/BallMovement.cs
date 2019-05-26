using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float speed;
    public LayerMask GroundLayer;
    public SphereCollider col;
    public float jumpForce;
    private bool previousGrounded = true;
    private bool explodeTime = false;
    public float knockBackStrength;
    public float knockBackRadius;
    private ParticleSystem hitParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!previousGrounded && IsGrounded())
        {
            explodeTime = true;
        }
        previousGrounded = IsGrounded();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(h, 0.0f, v);

        rb.AddForce(movement * speed);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, GroundLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Floor" || collision.collider.gameObject.name == "Planks" || collision.collider.gameObject.name == "Base" || collision.collider.gameObject.name == "Sides")
        {

            if (explodeTime && IsGrounded())
            {
                explodeTime = false;
                Collider[] colliders = Physics.OverlapSphere(transform.position, knockBackRadius);

                for (int i = 0; i < colliders.Length; i++)
                {
                    Rigidbody rb = colliders[i].GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        if (rb.gameObject.name != "Sphere")
                        {
                            EnemyHealth enemyHealth = rb.gameObject.GetComponent<EnemyHealth>();
                            hitParticles.transform.position = transform.position;
                            hitParticles.Play();
                            if (enemyHealth != null)
                            {
                                enemyHealth.TakeDamage(50, rb.gameObject.transform.position);
                            }
                            rb.AddExplosionForce(knockBackStrength, transform.position, knockBackRadius, 0.0f, ForceMode.Impulse);
                        }
                    }
                }
            }
        }
    }
}
