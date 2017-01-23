using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Circle : Character {
	
	[SerializeField]
	private float speed = 2.0f;
	private Vector3 direction;
	private Vector3 direction1;

	void Start () {
		direction = transform.right;
		direction1 = transform.up * -1.0F;
	}

	private void FixedUpdate()
	{
		if (transform.position.y < -10)
			Destroy (gameObject);
	}
		
	void Update () {
		run ();
	}

	public override void damage()
	{
	}

	private void run()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position + transform.up * 0.5F + transform.right * direction.x * 0.5F, 0.01F);
		if (colliders.Length > 0 && colliders.All (x => !x.GetComponent<Character> ()))
			direction *= -1.0F;
		
		colliders = Physics2D.OverlapCircleAll (transform.position + transform.up * -0.01F + transform.right * direction.x * -0.4F, 0.01F);

		if (colliders.Length < 1)
			transform.position = Vector3.MoveTowards (transform.position, transform.position + direction1, speed * Time.deltaTime);	
		else
			transform.position = Vector3.MoveTowards (transform.position, transform.position + direction, speed * Time.deltaTime);
	}

	protected void OnTriggerEnter2D(Collider2D collider)
	{
		Character character = collider.GetComponent<Character> ();

		if (character && character is Hero) {
			character.damage ();
		}
	}
}
