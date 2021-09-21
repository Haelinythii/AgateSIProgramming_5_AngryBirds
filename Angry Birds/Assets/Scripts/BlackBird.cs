using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    [SerializeField] float explosionForce = 10f;
    [SerializeField] float explosionRadius = 1f;
    [SerializeField] float upwardsModifier = 1f;
    public LayerMask explosionLayerMask;
    [SerializeField] ParticleSystem smokeParticleSystem;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.transform.tag == "Enemy" || collision.transform.tag == "Obstacle")
        {
            Explode();
            birdSpriteRenderer.enabled = false;
            birdRigidbody.bodyType = RigidbodyType2D.Static;
            smokeParticleSystem.Emit(30);
            StartCoroutine(DestroyAfter(2));
        }
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, explosionLayerMask);
        foreach (Collider2D hit in hits)
        {
            Rigidbody2D rbHit = hit.GetComponent<Rigidbody2D>();
            if (rbHit != null)
            {
                Vector2 direction = hit.transform.position - transform.position;
                float distance = direction.magnitude;

                direction.y += upwardsModifier;
                //normalize direction
                direction.Normalize();
                

                rbHit.AddForce(direction * (1 / distance) * explosionForce);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
