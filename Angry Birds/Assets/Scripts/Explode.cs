using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public LayerMask layerMask;
    public float explosionForce = 10f;
    public GameObject kayu;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D[] hits = Physics2D.OverlapCircleAll(mousePos, 1f, layerMask);
            foreach (Collider2D hit in hits)
            {
                Rigidbody2D rbHit = hit.GetComponent<Rigidbody2D>();
                if (rbHit != null)
                {
                    Vector2 direction = (Vector2)hit.transform.position - mousePos;
                    float distance = direction.magnitude;

                    direction.y += 1f;
                    //normalize direction
                    direction.Normalize();

                    rbHit.AddForce(direction * (1 / distance) * explosionForce);
                }
            }
        }
    }
}
