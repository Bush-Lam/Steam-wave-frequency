using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerController : MonoBehaviour
{
    public Image EnemyHealth;
    ParticleSystem Explosionparticle;
    GearEnemySpawn EnemySpawn;
    Canvas canvas;
    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;

    float TimeChangeDirection;
    Vector2 XDirection;

    void Start() {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        EnemySpawn = GameObject.Find("Scenery").GetComponent<GearEnemySpawn>();
        EnemyHealth = Instantiate(EnemySpawn.EnemyHealthBarPrefab, transform.position, Quaternion.identity) as Image;
        EnemyHealth.GetComponent<RectTransform>().sizeDelta = new Vector2(4, 2);
        EnemyHealth.transform.Find("EnemyHealthBarDamage").GetComponent<RectTransform>().sizeDelta = new Vector2(4, 2);
        EnemyHealth.transform.SetParent(canvas.transform);
        StartCoroutine("TowerMovement");
        InvokeRepeating("IncrementTime", 0, 12);
    }

    void IncrementTime()
    {
        TimeChangeDirection = Time.time + 6;
    }

    private void Update()
    {
        EnemyHealth.transform.position = (Vector2)transform.position + new Vector2(0, 3);
    }

    private void FixedUpdate()
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

        if (TimeChangeDirection > Time.time)
        {
            transform.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + ((-Vector2.up / 3) + (XDirection / 3)) * 20000);
        }
        else 
        {
            transform.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + ((Vector2.up) + (XDirection / 2)) * 50000);
        }  
    }

    IEnumerator TowerMovement()
    {
        yield return new WaitForSeconds(2);

        if (Random.Range(0, 2) == 1)
            XDirection = -Vector2.right;
        else
            XDirection = Vector2.right;
        yield return new WaitForSeconds(Random.Range(2, 4));
        if (Random.Range(0, 2) == 1)
            XDirection = Vector2.zero;
        else
            XDirection = -Vector2.right;
        yield return new WaitForSeconds(Random.Range(2, 4));
        if (Random.Range(0, 2) == 1)
            XDirection = Vector2.right;
        else
            XDirection = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(2, 4));
        StartCoroutine("TowerMovement");
    }

    void OnCollisionEnter2D(Collision2D other) {

        if (other.collider.tag == "Projectile")
        {
            EnemyHealth.transform.Find("EnemyHealthBarDamage").GetComponent<Image>().fillAmount -= 0.067f; // 15 hits
            other.collider.transform.position = new Vector2(300, 300);

            if (EnemyHealth.transform.Find("EnemyHealthBarDamage").GetComponent<Image>().fillAmount <= 0.067f)
            {
                EnemyHealth.transform.Find("EnemyHealthBarDamage").GetComponent<Image>().fillAmount = 1;
                Explosionparticle = Instantiate(EnemySpawn.Explosion, transform.position, transform.rotation) as ParticleSystem;
                transform.position = new Vector2(300, 300);
                Explosionparticle.Play();
                Invoke("DestroyParticle", 1);
                EnemySpawn.Score = EnemySpawn.Score + 2;
                EnemySpawn.AddScore();
                //audioSource.Play();
            }
        }       
    }
}
