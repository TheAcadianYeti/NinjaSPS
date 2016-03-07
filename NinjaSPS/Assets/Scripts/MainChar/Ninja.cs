using UnityEngine;
using System.Collections;

public class Ninja : MonoBehaviour {

	private const float circleRadius = 1.7f;

	public LayerMask whatIsGround,whatIsWall;
	public Transform groundCheck;

	public BoxCollider2D swordHitBox;
	public float max_speed = 5.0f, jump_speed = 10.0f, air_speed = 10.0f;

	private Animator anim;
	private Rigidbody2D rigid;
	
	private float attackCount = 0.0f;
	public bool onGround = true, onWall = false, facingRight = true, attacking = false;//Used to determine if we are on the ground, or if we are attached to a wall
	// Use this for initialization
	void Start () 
	{
		swordHitBox = GetComponentInChildren<BoxCollider2D>();
		swordHitBox.enabled = false;
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		onGround = Physics2D.OverlapCircle(groundCheck.position, circleRadius, whatIsGround);

		if (!onGround)
		{
			onWall = Physics2D.OverlapCircle(groundCheck.position, circleRadius, whatIsWall);
		}
		else
		{
			onWall = false;
		}

		if( onGround && Input.GetButtonDown("Jump"))
		{
			Jump();
		}
		else if(!attacking)
		{
            //Perform transforms
            float xMovement = Input.GetAxis("Horizontal");
			float xVel;
			if (!onGround)
			{
				xVel = (xMovement * air_speed);
			}
			else
			{
				xVel = (xMovement * max_speed);
			}
			rigid.velocity = new Vector2(xVel, rigid.velocity.y);
			//Check to see if we're on ground, if so animate
			if(onGround && xVel != 0.0f)
			{   
                anim.SetBool("Running", true);
		   }
			else
			{
			
				anim.SetBool("Running", false);
				
            }

			if(xMovement < 0.0f && facingRight)
			{
				Flip();
			}
			else if(xMovement > 0.0f && !facingRight)
			{
				Flip();
			}	
		}
	}

	//Overriting on trigger enter 2d method
	public void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log(collider.enabled);
		bool whoHit = collider.enabled;
		//We would play the death animation here. which will kill the unit
		if (collider.gameObject.tag == "Ninja" && !whoHit)
		{
			die();
		}

	}

	/**
	*Kill method
	*/
	public void die()
	{
		//Kills this object and removes it from the scene
		Destroy(this.gameObject, 0.0f);
	}



	//Jump method
	void Jump()
	{
		rigid.velocity = new Vector2(rigid.velocity.x, jump_speed);
		anim.SetBool("Running", false);
	}
	//Attack method
	public void Attack()
	{
		attacking = true;
		anim.SetBool("Running", false);
		anim.SetBool("Attacking", true);
		//swordHitBox.toggle();
		swordHitBox.enabled = true;
		if (onGround)
		{
			rigid.velocity = new Vector2(0, rigid.velocity.y);
		}
		else
		{
			rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y);
		}

	}

	/**
	Method called by behaviour to stop attacking
	*/
	public void doneAttacking()
	{
		anim.SetBool("Attacking", false);
		swordHitBox.enabled = false; 
		attacking = false;
	}

	void Flip()
	{
		
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
