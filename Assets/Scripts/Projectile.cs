using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    private SuperHardEnemyController SuperHardEnemyController;
    

    // Start is called before the first frame update
    void Awake()
    {
       rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        {
        EnemyController e = other.collider.GetComponent<EnemyController>
        ();
            if(e != null)
            {
                e.Fix();
            }
        Destroy(gameObject);
    }
               
               {
                HardEnemyController e = other.collider.GetComponent<HardEnemyController>
        ();
            if(e != null)
            {
                e.Fix();
            }
        Destroy(gameObject);
               }
                    {
                SuperHardEnemyController e = other.collider.GetComponent<SuperHardEnemyController>
        ();
            
            if(e != null)
            {
                
                e.Fix();
                
            
            }
        Destroy(gameObject);
              e.damage += 1;
               }

    }

    

    

         
                
    
        
    
}
