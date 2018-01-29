using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearEnemy : MonoBehaviour
{
    GameObject Player;
    GearEnemySpawn EnemySpawn;
    public Image EnemyHealth;
    ParticleSystem Explosionparticle;
    Canvas canvas;
    private AudioSource audioSource;

    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Projectile")
        {
            EnemyHealth.transform.Find("EnemyHealthBarDamage").GetComponent<Image>().fillAmount -= 0.2f;
            other.collider.transform.position = new Vector2(300, 300);

            if (EnemyHealth.transform.Find("EnemyHealthBarDamage").GetComponent<Image>().fillAmount <= 0.2f)
            {
                EnemyHealth.transform.Find("EnemyHealthBarDamage").GetComponent<Image>().fillAmount = 1;
                Explosionparticle = Instantiate(EnemySpawn.Explosion, transform.position, transform.rotation) as ParticleSystem;
                transform.position = new Vector2(300, 300);
                Explosionparticle.Play();
                Invoke("DestroyParticle", 1);
                EnemySpawn.Score++;
                EnemySpawn.AddScore();
                audioSource.Play();
            } else {
                AudioSource sfx = GameObject.Find("GearHitAudioSource").GetComponent<AudioSource>();
                sfx.Play();
            }
        } else if (other.gameObject.tag == "Obstacle") {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),
                                      GetComponent<Collider2D>());
        }
    }

    void DestroyParticle()
    {
        Destroy(Explosionparticle.gameObject);
    }

    void Start ()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        EnemySpawn = GameObject.Find("Scenery").GetComponent<GearEnemySpawn>();
        Player = GameObject.Find("Player").gameObject;
        EnemyHealth = Instantiate(EnemySpawn.EnemyHealthBarPrefab, transform.position, Quaternion.identity) as Image;
        EnemyHealth.transform.SetParent(canvas.transform);
        StartCoroutine("GearMovement");
        audioSource = GameObject.Find("ExplosionAudioSource").GetComponent<AudioSource>();
    }

    IEnumerator GearMovement()
    {
        yield return new WaitForSeconds(2);

        if (Random.Range(0, 2) == 1)
            transform.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + (-Vector2.right + Vector2.up) * 1000);
        else
            transform.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + (Vector2.right + Vector2.up) * 1000);
        yield return new WaitForSeconds(Random.Range(2, 4));
        if (Random.Range(0, 2) == 1)
            transform.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + (-Vector2.right + Vector2.up) * 1000);
        else
            transform.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + (Vector2.right + Vector2.up) * 1000);
        yield return new WaitForSeconds(Random.Range(2, 4));
        StartCoroutine("GearMovement");
    }

    void FixedUpdate ()
    {
        if (transform.position.x < leftLimit)
        {
            transform.position = new Vector3(leftLimit,
                                             transform.position.y,
                                             transform.position.z);
        }

        if (transform.position.x > rightLimit)
        {
            transform.position = new Vector3(rightLimit,
                                             transform.position.y,
                                             transform.position.z);
        }

        if (transform.position.y < bottomLimit)
        {
            transform.position = new Vector3(transform.position.x,
                                             bottomLimit,
                                             transform.position.z);
        }

        if (Vector2.Distance(Player.transform.position, transform.position) > 20)
        {
            transform.position = (Vector2)transform.position + new Vector2(0, -0.5f);
            Vector2 direction = Player.transform.position - transform.position;
            transform.GetComponent<Rigidbody2D>().velocity = direction * 2;
        }
	}

    private void Update()
    {
        EnemyHealth.transform.position = (Vector2)transform.position + new Vector2(0.15f, 0.75f);
    }
}
