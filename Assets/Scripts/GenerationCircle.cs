using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationCircle : MonoBehaviour {
	private Vector3 position;
	private Circle circle;
	private float lasttime;
	[SerializeField]
	private float time = 3.0F;

	private void Awake()
	{
		circle = Resources.Load<Circle>("Circle");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lasttime + time < Time.time) {
			generetion ();
			lasttime = Time.time;
		}
	}

	private void generetion() {
		position = transform.position;
		Circle newcircle = Instantiate(circle, position, circle.transform.rotation) as Circle;
	}
}
