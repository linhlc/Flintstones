using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dragon : Character {
	[SerializeField]
	private Vector3 direction;
	[SerializeField]
	private float jumpforce = 10.0F;
	private float speed = 1.0F;
	private int lives = 15;
	private bool isGround = false;
	private Balls balls;
	private SpriteRenderer sprite;
	private Balls ball;
	new private Rigidbody2D rigidbody;
	private float lasttime;
	private float time = 5.0F;
	private bool isHero = false;

	private void Awake()
	{
		
			lasttime = Time.time;
			rigidbody = GetComponent<Rigidbody2D> ();
			balls = Resources.Load<Balls> ("Ball");
			sprite = GetComponentInChildren<SpriteRenderer> ();

	}

	void Start () {
		direction = transform.right;
	}

	private bool flag = true;

	private void FixedUpdate()
	{
		onGround();
		CheckOrHero();
	}
		
	void Update () {
		if (isHero) {
			if (lasttime + time < Time.time) {
				Jump ();
				lasttime = Time.time;
			}
			
			if (!ball && isGround) {
				Shot ();
			}
		}
	}

	public int Live
	{
		get {return lives;}
		set {
			if (value < 15)
				lives = value;
		}
	}

	public override void damage()
	{
		lives--;
		print ("lives dracon = "+lives);
		if (lives == 0)
			Destroy (gameObject);
	}

	private void Shot()
	{
		Vector3 position = transform.position;
		position.y += 0.7F;
		ball = Instantiate(balls, position, balls.transform.rotation) as Balls;
		ball.Parent = gameObject;
		ball.Direction = ball.transform.right * (sprite.flipX ? -1.0F : 1.0F);
	}

	private void Jump()
	{
		rigidbody.AddForce (transform.up * 360, ForceMode2D.Impulse);
		rigidbody.AddForce(transform.right * 240 * (sprite.flipX ? -1.0F : 1.0F), ForceMode2D.Impulse);
		flag = true;
		sprite.flipX ^= true;
	}

	private void CheckOrHero()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5F + transform.right * (sprite.flipX ? -1.0F : 1.0F) * 10F, 0.1F);
		if (colliders.Length > 0 && !colliders.All (x => !x.GetComponent<Hero> ())) isHero = true;
	}

	private void onGround()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
		isGround = colliders.Length > 1;
	}
		
}
