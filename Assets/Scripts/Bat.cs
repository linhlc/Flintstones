using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {
	private GameObject parent;
	public GameObject Parent { set {parent = value;} get {return parent;}}

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 0.1F);
	}

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		
		Character character = collider.GetComponent<Character> ();

		if (character && character.gameObject != parent) {
			Destroy (gameObject);
			character.damage ();
		}
	}
}