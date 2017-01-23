using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bird : Character {
	private Vector3 direction;
	[SerializeField]
	private float jumpforce = 0.3F;
	private SpriteRenderer sprite;
	new private Rigidbody2D rigidbody;
	private bool isHero = false;


	void Start () {
		direction = transform.right;
	}

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}
		
	private void FixedUpdate()
	{
		atHero();
	}

	void Update () {
		if (isHero) fly ();
		direction = transform.position;
		if (direction.y < -5)
			Destroy (gameObject);
	}

	private void fly()
	{
		rigidbody.AddForce(transform.right * -1 * jumpforce, ForceMode2D.Impulse);
		rigidbody.AddForce(transform.up * -1 * jumpforce, ForceMode2D.Impulse);
	}

	private void atHero()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, 4F);
		if (!colliders.All (x => !x.GetComponent<Hero> ())) 
		{
			isHero = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		Character character = collider.GetComponent<Character> (); 

		if (character && character is Hero) {
				character.damage ();
		}
	}
}



