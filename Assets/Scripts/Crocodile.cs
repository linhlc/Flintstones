using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Crocodile : Character {

	[SerializeField]
	private float speed = 2.0f;
	private Vector3 direction;

	void Start () {
		direction = transform.right;
	}

	void Update () {
		run ();
	}

	private void run()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position + transform.up * 0.5F + transform.right * direction.x * 0.5F, 0.01F);
		if (colliders.Length > 0 && colliders.All (x => !x.GetComponent<Character> ()))
			direction *= -1.0F;
		colliders = Physics2D.OverlapCircleAll (transform.position + transform.up * -0.5F + transform.right * direction.x * 0.5F, 0.001F);
		if (colliders.Length < 1)
			direction *= -1.0F;
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
