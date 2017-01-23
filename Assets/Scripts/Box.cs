using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Character {
	private int rand;
	private Life life;
	private Coin coin;

	private void Awake()
	{
		life = Resources.Load<Life>("Life");
		coin = Resources.Load<Coin>("Coin");
	}

	public override void damage()
	{

		Vector3 position = transform.position;
		rand = Random.Range (0,100);
		Destroy(gameObject);

		if (rand <= 30) {
			Instantiate (life, position, life.transform.rotation);
		}

		if (rand > 31 && rand <= 50) {
			Instantiate(coin, position, coin.transform.rotation);
		}

	}
}
