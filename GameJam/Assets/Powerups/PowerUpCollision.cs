using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpCollision : MonoBehaviour {
    ShipController Player;

    void Start() {
        Player = GameObject.Find("Player").GetComponent<ShipController>();
    }

    public void IncrementBulletDelay()
    {
        Player.GetComponent<ShipController>().bulletDelay += 0.05f;
    }

    public void DecrementShipSpeed()
    {
        Player.GetComponent<ShipController>().speed -= 1;
    }

    public void DecrementBulletPower()
    {
        Player.GetComponent<ShipController>().bulletPower -= 0.05f;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Reset position if colliding with any entitiy. Update player 
        if (collision.collider.tag == "Player") {
            if (transform.name.Contains("HP") &&
                Player.PlayerHealthBar.transform.Find("PlayerHealthBarDamage").GetComponent<Image>().fillAmount <= 1) {
                Player.PlayerHealthBar.transform.Find("PlayerHealthBarDamage").GetComponent<Image>().fillAmount += 0.2f;
            }
            else if (transform.name.Contains("Oil")) {
                if (Player.GetComponent<ShipController>().bulletDelay >= 0.1f) {
                    {
                        Player.GetComponent<ShipController>().bulletDelay -= 0.05f;
                        Invoke("IncrementBulletDelay", 15);
                    }
                }
            }
            else if (transform.name.Contains("Lead")) // increase damage done
            {
                if (Player.GetComponent<ShipController>().bulletPower <= 0.4f)
                {
                    Player.GetComponent<ShipController>().bulletPower += 0.05f;
                    Invoke("DecrementBulletPower", 15);
                }
            }
            else if (transform.name.Contains("Coal"))
            {
                if (Player.GetComponent<ShipController>().speed <= 15)
                {
                    Player.GetComponent<ShipController>().speed += 1;
                    Invoke("DecrementShipSpeed", 15);
                }
            }

            transform.GetComponent<Rigidbody2D>().isKinematic = false;
        } else {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(),
                                      GetComponent<Collider2D>());
        }
        transform.position = new Vector2(300, 300);
    }
}
