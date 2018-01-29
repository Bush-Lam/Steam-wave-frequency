using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShipController : MonoBehaviour
{
    Canvas canvas;
    public float speed = 50;
    private Vector3 newPosition;
    Vector2 SpaceShipMovement;
    public GameObject Bullets;
    public List<GameObject> BulletsList;
    int BulletIndex;
    public Image HealthBarPrefab;
    Image PlayerHealthBar;
    float PlayerDamageDelay;
    float BulletDelay;
    ParticleSystem exHaustTrail;
    bool flip;
    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy" && PlayerDamageDelay < Time.time)
        {
            PlayerDamageDelay = Time.time + 1;
            PlayerHealthBar.transform.Find("PlayerHealthBarDamage").GetComponent<Image>().fillAmount -= 0.2f;

            if (PlayerHealthBar.transform.Find("PlayerHealthBarDamage").GetComponent<Image>().fillAmount <= 0.2f)
            {
                PlayerHealthBar.transform.Find("PlayerHealthBarDamage").GetComponent<Image>().fillAmount = 1;
            }
        }
    }

    void Start()
    {
        flip = true;
        newPosition = transform.position;
        BulletsList = new List<GameObject>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        exHaustTrail = transform.GetComponentInChildren<ParticleSystem>();

        PlayerHealthBar = Instantiate(HealthBarPrefab, transform.position, Quaternion.identity) as Image;
        PlayerHealthBar.transform.SetParent(canvas.transform);
        ParticleSystem ExHaustTrail = Instantiate(exHaustTrail, transform.position, transform.rotation) as ParticleSystem;
        ExHaustTrail.transform.SetParent(transform);
        exHaustTrail.Play();

        for (int i = 0; i < 15; i++)
        {
            GameObject tempBullet = Instantiate(Bullets, new Vector2(300, 300), Quaternion.identity) as GameObject;
            tempBullet.GetComponent<Rigidbody2D>().isKinematic = true;
            BulletsList.Add(tempBullet);
        }
    }

    void ShootBullets(Vector2 MousePosition)
    {
        BulletDelay = Time.time + 0.2f;
        BulletIndex++;
        if (BulletIndex == 14)
            BulletIndex = 0;
        
        StartCoroutine(ResetPosition(BulletsList[BulletIndex].transform));
        BulletVelocity(BulletsList[BulletIndex].transform);
    }

    void BulletVelocity(Transform BulletTransform)
    {
        BulletTransform.GetComponent<Rigidbody2D>().isKinematic = false;
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)); // mouse position
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y + 0.5f); // ship 
        Vector2 direction = target - myPos; // directional vector
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        BulletTransform.rotation = rotation;
        BulletTransform.transform.position = (Vector2)transform.position + new Vector2(direction.x * 2.5f, direction.y * 2.5f);
        BulletTransform.GetComponent<Rigidbody2D>().velocity = direction * 30;
    }

    IEnumerator ResetPosition(Transform BulletTransform)
    {
        yield return new WaitForSeconds(5);
        BulletTransform.position = new Vector2(300, 300);
        BulletTransform.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Fire1") && BulletDelay < Time.time)
            ShootBullets(Input.mousePosition);
    }

    void FixedUpdate()
    {      
        SpaceShipMovement.x = Input.GetAxis("Horizontal");
        SpaceShipMovement.y = Input.GetAxis("Vertical");
        SpaceShipMovement *= speed;

        if (SpaceShipMovement.x < 0)
        {
            flip = false;
            transform.GetComponentInChildren<ParticleSystem>().transform.position = new Vector2(transform.position.x + 0.8f, transform.GetComponentInChildren<ParticleSystem>().transform.position.y);
            transform.GetComponentInChildren<ParticleSystem>().transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (SpaceShipMovement.x > 0)
        {
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

