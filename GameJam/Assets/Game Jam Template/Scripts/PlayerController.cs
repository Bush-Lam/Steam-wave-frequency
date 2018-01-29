using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = .1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//transform.Translate (speed * Time.deltaTime, 0f, 0f);
		//Camera.main.transform.Translate (speed * Time.deltaTime, 0f);

		if (Input.GetKey ("up"))
			transform.Translate (0f, speed * Time.deltaTime, 0f);
		if (Input.GetKey ("down"))
			transform.Translate (0f, -speed * Time.deltaTime, 0f);
		if (Input.GetKey ("left"))
			transform.Translate (-speed* Time.deltaTime, 0f,0f);
		if (Input.GetKey ("right"))
			transform.Translate (speed * Time.deltaTime, 0f,0f);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy (gameObject);
	}
}
	

