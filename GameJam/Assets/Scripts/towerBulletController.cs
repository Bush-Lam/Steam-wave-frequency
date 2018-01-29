using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour {
    public float lifetime;
    float birthTime;

	// Use this for initialization
	void Start () {
        birthTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - birthTime > lifetime) {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Tower")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),
                                      GetComponent<Collider2D>());
        } else {
            Destroy(this.gameObject);
        }
    }
}
