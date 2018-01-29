using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletController : MonoBehaviour {
    public float lifetime;
    float birthTime;

	void Start () {
        birthTime = Time.time;
	}

    void Update() {
        // Destroy itself after its lifetime
        if (Time.time - birthTime > lifetime) {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        // Ignore collision only with the player, otherwise destroy itself
        if (other.collider.tag == "Player") {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),
                                      GetComponent<Collider2D>());
        } else {
            Destroy(this.gameObject);
        }
    }
}
