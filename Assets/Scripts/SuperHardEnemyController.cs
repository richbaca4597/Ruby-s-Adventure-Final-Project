using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHardEnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    bool broken = true;



    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    Animator animator;
     
    private RubyController rubyController;

    public int score;

    public int damage;

    
    
    
    // Start is called before the first frame update
    void Start()
    {   
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
                GameObject rubyControllerObject = GameObject.FindWithTag("RubyController");

        if (rubyControllerObject != null)
        {
            rubyController = rubyControllerObject.GetComponent<RubyController>();
            print ("Found the RubyController Script!");

        }
        if (rubyController == null)
        {
            print ("Cannot find GameController Script!");
        }
    }

    void Update()
    {
        if(!broken)
        
        {
            return;
            
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    
    }
    
    void FixedUpdate()
    {
        if(!broken)
        {
            return;
        }
        Vector2 position = rigidbody2D.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2D.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other)
{
    RubyController player = other.gameObject.GetComponent<RubyController>();

    

    if (player != null)
    {
        player.ChangeHealth(-2);
        
    }
}

public void Fix()
{
    
      if(damage == 2)
      {
                    broken = false;
    rigidbody2D.simulated = false;
  
    
    rubyController.ChangeScore(1);
    animator.SetTrigger("Fixed");
      }
          
    
    }
    
   


    
}
