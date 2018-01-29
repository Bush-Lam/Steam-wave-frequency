using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour {
    Canvas canvas;
    public float speed = 50;
    Vector2 SpaceShipMovement;

    public Image HealthBarPrefab;
    public Image PlayerHealthBar;
    float PlayerDamageDelay;

    ParticleSystem exhaustTrail;
    bool flip;

    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;
    public AudioSource audioSource;

    public float bulletDelay;
    public GameObject bulletObject;
    public float bulletPower; // damage done 
    
    float lastFire;
    
    void Start() {
        audioSource = GameObject.Find("FireAudioSource").GetComponent<AudioSource>();
        flip = true;

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        exhaustTrail = transform.GetComponentInChildren<ParticleSystem>();

        PlayerHealthBar = Instantiate(HealthBarPrefab, transform.position, Quaternion.identity) as Image;
        PlayerHealthBar.transform.SetParent(canvas.transform);
        ParticleSystem ExHaustTrail = Instantiate(exhaustTrail, transform.position, transform.rotation) as ParticleSystem;
        ExHaustTrail.transform.SetParent(transform);
        exhaustTrail.Play();

        lastFire = 0.0f; 
        bulletDelay = 0.2f; // default delay
        bulletPower = 0.2f; // 
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Enemy" && PlayerDamageDelay < Time.time) {
            PlayerDamageDelay = Time.time + 1;
            PlayerHealthBar.transform.Find("PlayerHealthBarDamage").GetComponent<Image>().fillAmount -= 0.1f;

            if (PlayerHealthBar.transform.Find("PlayerHealthBarDamage").GetComponent<Image>().fillAmount <= 0.1f) {
				SceneManager.LoadScene ("GameOver");
            }

            AudioSource asource = transform.Find("PlayerHitAudioSource").GetComponent<AudioSource>();
            asource.Play();

        } else if (other.collider.tag == "Projectile") {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),
                                      GetComponent<Collider2D>());
        }
   }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time - lastFire > bulletDelay) {
            shootBullet();
            audioSource.Play();
            lastFire = Time.time;
        }
    }

    void shootBullet() {
        int direction = flip ? 1 : -1;
        Vector2 velocity = new Vector2(direction * 1000, 0);
        Vector2 location = new Vector2((gameObject.transform.position.x) + (direction * 1),
                                       gameObject.transform.position.y);
        GameObject cannonball = Instantiate(bulletObject,
                                            location,
                                            transform.rotation);
        cannonball.GetComponent<Rigidbody2D>().AddForce(velocity);
    }

    void FixedUpdate()
    {      
        SpaceShipMovement.x = Input.GetAxis("Horizontal");
        SpaceShipMovement.y = Input.GetAxis("Vertical");
        SpaceShipMovement *= speed;

        if (SpaceShipMovement.x < 0) {
            flip = false;
            transform.GetComponentInChildren<ParticleSystem>().transform.position = new Vector2(transform.position.x + 0.8f, transform.GetComponentInChildren<ParticleSystem>().transform.position.y);
            transform.GetComponentInChildren<ParticleSystem>().transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (SpaceShipMovement.x > 0) {
            flip = true;
            transform.GetComponentInChildren<ParticleSystem>().transform.position = new Vector2(transform.position.x - 0.5f, transform.GetComponentInChildren<ParticleSystem>().transform.position.y);
            transform.GetComponentInChildren<ParticleSystem>().transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        if (transform.position.x < leftLimit) {
            transform.position = new Vector3(leftLimit,
                                             transform.position.y,
                                             transform.position.z);
        }

        if (transform.position.x > rightLimit) {
            transform.position = new Vector3(rightLimit,
                                             transform.position.y,
                                             transform.position.z);
        }

        if (transform.position.y > topLimit) {
            transform.position = new Vector3(transform.position.x,
                                             topLimit,
                                             transform.position.z);
        }

        if (transform.position.y < bottomLimit) {
            transform.position = new Vector3(transform.position.x,
                                             bottomLimit,
                                             transform.position.z);
        }

        transform.GetComponent<SpriteRenderer>().flipX = flip;

        transform.GetComponent<Rigidbody2D>().velocity = SpaceShipMovement;

        PlayerHealthBar.transform.position = (Vector2)transform.position + new Vector2(0.25f, 0.75f);
    }
}

