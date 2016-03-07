using UnityEngine;
using System.Collections;

public class Ninja : MonoBehaviour {

	private const float circleRadius = 1.5f;

	public LayerMask whatIsGround,whatIsWall;
	public Transform groundCheck;
	public float max_speed = 5.0f, jump_speed = 10.0f, attackDuration = 10.0f;

	private Animator anim;
	private float attackCount = 0.0f;
	private bool attacking = false, onGround = true, onWall = false, facingRight = true;//Used to determine if we are on the ground, or if we are attached to a wall
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
		Debug.Log(info.IsName ("Idle"));
	}
	
	// Update is called once per frame
	void FixedUpdate () 
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

		if(attacking)
		{

			attackCount++;
			if(attackCount > attackDuration)
			{
				attackCount = 0;
				anim.SetBool("Attacking", false);
				attacking = false;
			}
			else
			{
				if(onGround)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				}
			}
		}
		else
		{
			//Perform transforms
			float xMovement = Input.GetAxis("Horizontal");
			GetComponent<Rigidbody2D>().velocity = new Vector2(xMovement * max_speed, GetComponent<Rigidbody2D>().velocity.y);
			//Check to see if we're on ground, if so animate
			if(onGround && xMovement != 0.0f)
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
	

			if(Input.GetButtonDown("Jump") && onGround)
			{
				Jump();
			}
			else if(Input.GetButtonDown("Attack"))
			{
				Attack();
			}
				
		}
	}

	//Jump method
	void Jump()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, jump_speed);
		anim.SetBool("Running", false);
	}
	//Attack method
	void Attack()
	{
		anim.SetBool("Attacking", true);
		anim.SetBool("Running", false);
		attacking = true;

	}

	void Flip()
	{
		
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
