using UnityEngine;
using System.Collections;

public class Ninja : MonoBehaviour {

	private const float circleRadius = 1.7f;

	public LayerMask whatIsGround,whatIsWall;
	public Transform groundCheck;

	public float max_speed = 5.0f, jump_speed = 10.0f, attackDuration = 10.0f;
	public bool attacking = false;
	private Animator anim;
	private Rigidbody2D rigid;
	private float attackCount = 0.0f;
	private bool onGround = true, onWall = false, facingRight = true, unityIsStupid = false;//Used to determine if we are on the ground, or if we are attached to a wall
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
		Debug.Log(info.IsName ("Idle"));
	}
	
	// Update is called once per frame
	void Update () 
	{
		onGround = Physics2D.OverlapCircle(groundCheck.position, circleRadius, whatIsGround);
		if(!onGround)
		{
			onWall = Physics2D.OverlapCircle(groundCheck.position, circleRadius, whatIsWall);
		}
		else
		{
			onWall = false;
		}

		if(Input.GetButtonDown("Attack"))
		{
			Attack();
		}
		else if(onGround && Input.GetButtonDown("Jump"))
		{
			Jump();
		}
		else if(!attacking)
		{
            //Perform transforms
            float xMovement = Input.GetAxis("Horizontal");
			float xVel = (xMovement * max_speed);
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

	//Jump method
	void Jump()
	{
		rigid.velocity = new Vector2(rigid.velocity.x, jump_speed);
		anim.SetBool("Running", false);
	}
	//Attack method
	void Attack()
	{
		rigid.velocity = new Vector2(0, rigid.velocity.y);
		attacking = true;
		anim.SetBool("Running", false);
		anim.SetBool("Attacking", true);
	}

	/**
	Method called by behaviour to stop attacking
	*/
	public void doneAttacking()
	{
		anim.SetBool("Attacking", false);
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
