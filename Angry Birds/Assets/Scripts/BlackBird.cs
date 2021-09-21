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

            //matikan dan buat burung statis
            birdSpriteRenderer.enabled = false;
            birdRigidbody.bodyType = RigidbodyType2D.Static;

            //mainkan particle system emit 30 particle
            smokeParticleSystem.Emit(30);
            StartCoroutine(DestroyAfter(2));
        }
    }

    private void Explode()
    {
        //dapetin obstacle atau enemy terdekat dalam radius explosion
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, explosionLayerMask);
        foreach (Collider2D hit in hits)
        {
            Rigidbody2D rbHit = hit.GetComponent<Rigidbody2D>();
            if (rbHit != null)
            {
                //hitung direction dan distance dari benda ke burung
                Vector2 direction = hit.transform.position - transform.position;
                float distance = direction.magnitude;

                //tambah y biar keliatan meledak naik
                direction.y += upwardsModifier;
                //normalize direction
                direction.Normalize();
                
                //kasih force sesuai dengan arah dan forcenya, (1/distance) agar semakin dekat dengan titik explosion semakin kuat force nya
                rbHit.AddForce(direction * (1 / distance) * explosionForce);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
