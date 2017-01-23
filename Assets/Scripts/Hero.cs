using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hero : Character {
	[SerializeField]
	private float speed = 3.0F;
	private int lives = 5;
	private int coins = 0;
	private float jumpforce = 12.0F;
	private bool isGround = false;
	private Balls balls;
	private Bat bat;
	private bool hero1 = true;
	private Balls ball;
	new private Rigidbody2D rigidbody;
	//private Animator animator;
	public SpriteRenderer sprite;
	private Panel panel;
	private bool flag = false;
	private bool isStickTop = false;
	private bool isStickBottom = false;



	public int Live
	{
		get {return lives;}
		set {
			if (value < 5)
				lives = value;
			panel.Refresh ();
		}
	}

	public int Coin
	{
		get {return coins;}
		set {
				coins = value;
		}
	}

	private void Awake()
	{
		panel = FindObjectOfType<Panel>();
		rigidbody = GetComponent<Rigidbody2D>();
		//animator = GetComponent<Animator>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		balls = Resources.Load<Balls>("Ball");
		bat = Resources.Load<Bat>("Bat");
	}

	private void FixedUpdate()
	{
		onGround();
		toClimb();
	}


	private void Update()
	{
		Vector3 direction = transform.position;
		if (direction.y < -15) Live = 0;
		//if (isGrounded) State = CharState.Idle;

		if (Input.GetButtonDown ("Switch")) hero1 ^= true; // стріляти або бити

		if (Input.GetButtonDown("Fire3") && hero1 && !ball) Shot(); // стріляти
			else
				if (Input.GetButtonDown("Fire3") && !hero1) Attack(); // бити
		
		if (Input.GetButton("Horizontal") && !flag) Run(); // бігти

		if (isGround && Input.GetButtonDown("Jump")) Jump(); // стрибати

		if (isGround && Input.GetButtonDown("Jump1")) Jump();	// стрибати і
		if (Input.GetButton ("Jump1")) Climb(); 				// вилазити
		if (Input.GetButtonUp ("Jump1")) rigidbody.WakeUp(); 

		if (Input.GetButton ("Jump") && hero1) stick();
	}

	private void Run()
	{
		Vector3 direction = transform.right * Input.GetAxis("Horizontal");
		transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
		sprite.flipX = direction.x < 0.0F;
	
		//if (isGrounded) State = CharState.Run;
	}

	private void Jump()
	{
		rigidbody.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
	}

	private void toClimb()
	{
		float direction = (sprite.flipX ? -1.0F : 1.0F);
		Collider2D[] up = Physics2D.OverlapCircleAll (transform.position + transform.up * 2F, 0.05F);
		Collider2D[] right = Physics2D.OverlapCircleAll (transform.position + transform.up * 0.7F + transform.right * direction * 0.8F, 0.01F);
		Collider2D[] diagonal = Physics2D.OverlapCircleAll (transform.position + transform.up * 1.0F + transform.right * direction * 1.2F, 0.01F);

		if (up.Length == 0 && diagonal.Length == 0)
			if (right.Length > 0 && right.All (x => !x.GetComponent<Character> ())) 
				flag = true;
		
		Collider2D[] issticktop = Physics2D.OverlapCircleAll (transform.position + transform.up * 1.6F, 0.1F);//0.8 1.0
		Collider2D[] isstickbottom = Physics2D.OverlapCircleAll (transform.position, 0.1F);

		if (issticktop.Length > 0 && !issticktop.All (x => !x.GetComponent<Stick> ())) 
			isStickTop = true;
				else 
				isStickTop = false;

		if (isstickbottom.Length > 0 && !isstickbottom.All (x => !x.GetComponent<Stick> ())) 
			isStickBottom = true;
				else 
				isStickBottom = false;
	}

	private void Climb()
	{
		if (flag) {
			rigidbody.Sleep ();
			if (Input.GetButtonDown ("Vertical")) {
				rigidbody.AddForce (transform.up * 10, ForceMode2D.Impulse);
				rigidbody.AddForce (transform.right * 2 * (sprite.flipX ? -1.0F : 1.0F), ForceMode2D.Impulse);
				flag = false;
				}
			//if (isGround) flag = false;
			}
	}

	private void Shot()
	{
		Vector3 position = transform.position;
		position.y += 0.5F;
		ball = Instantiate(balls, position, balls.transform.rotation) as Balls;
		ball.Parent = gameObject;
		ball.Direction = ball.transform.right * (sprite.flipX ? -1.0F : 1.0F);
	}

	private void Attack()
	{
		Vector3 position = transform.position;
		position.x += 1F * (sprite.flipX ? -1.0F : 1.0F);
		Bat newbat = Instantiate(bat, position,bat.transform.rotation) as Bat;
		newbat.Parent = gameObject;
	}

	private void onGround()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

		isGround = colliders.Length > 1;
		if (isGround) flag = false;
		//if (!isGrounded) State = CharState.Jump;
	}
		
	public void stick()
	{
		if (isStickTop) 
		{
			rigidbody.Sleep ();
			if (Input.GetButtonDown ("Vertical")) {
				rigidbody.WakeUp();
				rigidbody.AddForce (transform.up * 80, ForceMode2D.Impulse);
			}
		}

		if (isStickBottom) 
		{
			rigidbody.Sleep ();
			if (Input.GetButtonDown ("Vertical")) {
				rigidbody.WakeUp();
				rigidbody.AddForce (transform.up * 50, ForceMode2D.Impulse);
			}
		}
	}
		
	public override void damage()
	{
		Live--;

		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce(transform.up * 5.0F, ForceMode2D.Impulse);

		if (lives == 0)
			Destroy (gameObject);
	}
		
}



/*public enum CharState
{
	Idle,
	Run,
	Jump
}*/