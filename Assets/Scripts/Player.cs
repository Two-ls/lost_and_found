using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{

	public float restartLevelDelay = 1f;
	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 10;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public float runSpeed = 10.0f;

    float horizontal;
    float vertical;

	private Animator animator;
	// private int food;

	protected override void Start ()
	{
		animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); 
		// food = GameManager.instance.playerFoodPoints;
		base.Start();
	}

	public void OnDisable ()
	{
		// GameManager.instance.playerFoodPoints = food;
	}

    // Update is called once per frame
    void Update()
    {
        // int horizontal = 0;
        // int vertical = 0;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
        	vertical = 0;

        // if (horizontal != 0 || vertical != 0)
        // {
        // 	AttemptMove<Wall> (horizontal, vertical);
        // }

        if (Input.GetAxis("Horizontal") < 0) 
        {
            Debug.Log("flip x");
            if(transform.rotation.eulerAngles.z != -90) 
            {
                transform.localRotation = Quaternion.Euler(0,0,-90);
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("dont flip x");
            if(transform.rotation.eulerAngles.z != 90) 
            {
                transform.localRotation = Quaternion.Euler(0,0,90);

            }
        }
        else if (Input.GetAxis("Vertical") > 0) 
        {
            Debug.Log("flip y");
            transform.localRotation = Quaternion.Euler(0,0,0);
            sr.flipY = true;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Debug.Log("dont flip y");
            transform.localRotation = Quaternion.Euler(0,0,0);
            sr.flipY = false;
        }
    }

    private void FixedUpdate()
    {  
        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    protected override void AttemptMove <T> (int xDir, int yDir)
    {
    	// food--;

    	base.AttemptMove <T> (xDir, yDir);

    	RaycastHit2D hit;
    	CheckIfGameOver();
    	// GameManager.instance.playersTurn = false;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
    	if (other.tag == "Exit")
    	{
    		Invoke ("Restart", restartLevelDelay);
    		enabled = false;
    	}

    	// else if (other.tag == "Food")
    	// {
    	// 	food += pointsPerFood;
    	// 	other.gameObject.SetActive(false);	
    	// }

    	else if (other.tag == "Soda")
    	{
    		// food += pointsPerSoda;
    		other.gameObject.SetActive(false);	
    	}
    }

    protected override void OnCantMove <T> (T component)
    {
    	Wall hitWall = component as Wall;
    	hitWall.DamageWall(wallDamage);
    	animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
    	SceneManager.LoadScene("MainScene");
    }

    // public void LoseFood (int loss)
    // {
    // 	animator.SetTrigger("playerHit");
    // 	food -= loss;
    // 	CheckIfGameOver();
    // }

  	private void CheckIfGameOver ()
    {
    	// if (food <= 0)
    	// {
    	// 	GameManager.instance.GameOver();
    	// }
    }

}
