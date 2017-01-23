using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	private void Start()
	{
		Destroy (gameObject, 10.0F);
	}

	private void Update()
	{
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		Hero hero = collider.GetComponent<Hero>();

		if (hero)
		{
			hero.Live++;
			Destroy(gameObject);
		}
	}

}
