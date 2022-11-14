using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    [SerializeField]
    public bool _hasExplode = false;
    public float explodeImpact;
    public float boomForce;
    public LayerMask layerHit;
    public GameObject explodePrefab;
 
    public void Explode()
    {
        //if (!_hasExplode)
        //{
        //_hasExplode = true;

        //Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explodeImpact, layerHit);
        //foreach(Collider2D obj in objects)
        //{
        //Vector2 direction = obj.transform.position - transform.position;
        //obj.GetComponent<Rigidbody2D>().AddForce(direction * boomForce);
        //}
        //Debug.Log("boom");
        //}

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explodeImpact);
        foreach(Collider2D obj in objects)
        {
            Rigidbody2D rb2D = obj.GetComponent<Rigidbody2D>();
            if(rb2D != null)
            {
                GenerateExplode();
                Vector2 direction = obj.transform.position - transform.position;
                float distance = 1 + direction.magnitude;
                float final = boomForce / distance;
                rb2D.AddForce(direction * final);
                //Destroy(obj);
            }
        
        
        }
        Debug.Log("boom");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeImpact);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
        Debug.Log("Col");
    }

    public void GenerateExplode()
    {
        GameObject explodeEffect = Instantiate(explodePrefab, transform.position, Quaternion.identity);
        Destroy(explodeEffect, 2);
    }
}
