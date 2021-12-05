using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public float maxSpeed = 6.0f;
    
    public int maxHealth = 5;

    public Text AmmoText;

    public Text LoseText;

    public Text WinText;

    public Text scoreText;

    public Text transitionText;
    public int score = 0;
    
    int currentHealth;

    public float timeInvincible = 2.0f;
    public int health { get { return currentHealth; }}

    public GameObject projectilePrefab;


    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    AudioSource audioSource;

    public AudioClip ThrowCogClip;
    public AudioClip hitSound;

    public AudioClip BackgroundClip;

    public AudioClip WinClip;

    public AudioClip LoseClip;

    public AudioClip MusicClip;

    public AudioClip KnightClip;

    public AudioClip CogClip;

    public AudioClip DuckClip;

    public AudioSource MusicSource;

    public ParticleSystem HitEffect;

    public ParticleSystem HealthEffect;


    bool gameOver;

    private static int Level = 1;

    private int cogs = 4;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        SetScoreText();
        WinText.text = "";
        LoseText.text = "";
        transitionText.text = "";
        gameOver = false;
        audioSource.clip = BackgroundClip;
        audioSource.Play();
        audioSource.loop = true;
        AmmoText.text = cogs.ToString();
        speed = 3.0f;
        
        

    }
    

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        
        

    }

    // Update is called once per frame
    void Update()
    {
          horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
              if (cogs == 0)
            {
                return;
                
            }
            
                Launch();
                cogs -= 1;
                AmmoText.text = cogs.ToString();
            
            
           
            
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + 
            Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = 
                hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
                if (score == 5)
                {
                    SceneManager.LoadScene("Scene 2");
                    Level = Level + 1;
                  

                }
                
            }
        }
        
                if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + 
            Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC2"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = 
                hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                      PlaySound(KnightClip);

                }

                
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (gameOver == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
          Application.Quit();  
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if(isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;

            PlaySound(hitSound);
            HitEffect.Play();

            
            
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if (amount > 0)
            {
             HealthEffect.Play();
            }

            if (currentHealth <= 0)
            {
                LoseText.text = "You Lose! Press R to Restart";
                gameOver = true;
                speed = 0.0f;
                audioSource.clip = BackgroundClip;
                audioSource.Stop();
                MusicSource.clip = MusicClip;
                MusicSource.Stop();

                    
                
            }
              if (currentHealth <= 0)
                    {
                         audioSource.clip = LoseClip;
                audioSource.Play();
                audioSource.loop = false;
                    }
        
        
    }

    public void ChangeScore(int amount)
    {
        score = (score + 1);
        SetScoreText();
        if (Level == 1)
        {
            if(score == 5)
        {
             transitionText.text = "Talk to Jambi to visit stage 2";
        }
        }
       
            if (Level == 2)
               {
                if (score == 7)
        {
            WinText.text = "You Win! Game by Richard Bacallao!";
            gameOver = true;

             
            
        }
        if (gameOver == true)
        {
                          audioSource.clip = BackgroundClip;
                audioSource.Stop();
                MusicSource.clip = MusicClip;
                MusicSource.Stop();
                audioSource.clip = WinClip;
                audioSource.Play();
                audioSource.loop = false;
        }
               }
            
      
      
         
         

    }

    void Launch()
    {
         
        GameObject projectileObject = Instantiate(projectilePrefab,
        rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.Launch(lookDirection, 300);

                animator.SetTrigger("Launch");
                PlaySound(ThrowCogClip);
                
            

               
                
    }
    void SetScoreText ()
    {
        scoreText.text = "Score:" + score.ToString ();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ammo")
        {
            cogs += 3;
            AmmoText.text = cogs.ToString();
            Destroy(collision.collider.gameObject);
            PlaySound(CogClip);

        }

        if(collision.collider.tag == "Speed")
        {
            speed += 1;
            Destroy(collision.collider.gameObject);
            PlaySound(DuckClip);
        }
    }
    
   

     
    
    

}

