using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gregController : MonoBehaviour {
    public GameObject target;
    public GameObject bullet;
    public float speed;
    public float fireDistance;
    bool flip;
    float xVel;
    float yVel;
	
    void Start() {
        flip = false;
    }

	void Update () {
        // Update flip to face the player and set the X velocity
        if (target.transform.position.x < transform.position.x) {
            flip = false;
        } else {
            flip = true;
        }
        xVel = (speed) * (flip ? 1 : -1);

        // Set the Y velocity
        bool pointUp = (target.transform.position.y > transform.position.y);
        if (Mathf.Abs(target.transform.position.y - transform.position.y) > 2.5) {
            yVel = (speed / 2) * (pointUp ? 1 : -1);
        }

        // Flip the sprite and move
        GetComponent<SpriteRenderer>().flipX = flip;
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);
	}

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
