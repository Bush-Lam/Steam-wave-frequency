using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerGunController : MonoBehaviour
{
    public Transform target;
    public float fireDistance;
    public GameObject bullet;
    public float firePower;
    public float fireCooldown;
    private float lastFire;

    void Start() {
        lastFire = 0f;
        target = GameObject.Find("Player").transform;
    }

    void Update() {
        adjustAngle();

        // Fire if the target is within firing distance
        if (Mathf.Abs(target.transform.position.x - transform.position.x) < fireDistance) {
            fireAtTarget();
        }
    }

    void fireAtTarget() {
        // Get the 2D velocity vector for where to shoot at, and instantiate an
        // EnemyBullet object with a slight offset from the cannon, and with the
        // initial velocity of the 2D velocity vector times the firing power.
        if (Time.time - lastFire > fireCooldown) {
            float angle = (transform.eulerAngles.z + 90) * (Mathf.PI / 180);
            Vector2 velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 placement = new Vector2(transform.position.x + (Mathf.Cos(angle)),
                                            transform.position.y + (Mathf.Sin(angle)));
            GameObject cannonball = Instantiate(bullet,
                                                placement,
                                                transform.rotation);
            cannonball.GetComponent<Rigidbody2D>().AddForce(firePower * velocity);
            lastFire = Time.time;
        }
    }

    void adjustAngle() {
        // Get x and y distance between target and tower
        float dx = target.position.x - transform.position.x;
        float dy = target.position.y - transform.position.y;

        // Calculate hypotenuse of the right triangle
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));

        // Get angle to point in
        float angle = Mathf.Asin(dx / hypotenuse);
        angle = angle * (180 / Mathf.PI);

        // Rotate gun
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            -1 * angle
        );
    }

    void OnCollisionEnter2D(Collision2D other) {
        // Ignore collision with towers and enemies
        if (other.collider.tag == "Tower" || other.collider.tag == "Enemy") {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),
                                      GetComponent<Collider2D>());
        }
    }
}
