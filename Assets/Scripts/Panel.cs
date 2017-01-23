using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {

	private Transform[] heart = new Transform[5];
	private Hero hero;

	private void Awake()
	{
		hero = FindObjectOfType<Hero>();
		for (int i = 0; i < heart.Length; i++)
		{
			heart[i] = transform.GetChild(i);
			
		}
	}

	public void Refresh()
	{
		for (int i = 0; i < heart.Length; i++)
		{
			if (i < hero.Live) heart[i].gameObject.SetActive(true);
			else heart[i].gameObject.SetActive(false);
		}
	}
}
