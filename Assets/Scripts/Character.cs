using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public virtual void damage()
	{
		Die();
	}

	protected virtual void Die()
	{
		Destroy(gameObject);
	}
}
