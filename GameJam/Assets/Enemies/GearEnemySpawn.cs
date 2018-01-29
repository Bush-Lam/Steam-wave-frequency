using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearEnemySpawn : MonoBehaviour
{
    public GameObject GearObject;
    public GameObject PlayerObject;
    public Text ScoreText;
    public int Score;

    public List<GameObject> ListofGearEnemies;
    public Image EnemyHealthBarPrefab;
    public ParticleSystem Explosion;

    void Start ()
    {
        ListofGearEnemies = new List<GameObject>();
        InvokeRepeating("GearSpawn", 1, 5);
	}

    public void AddScore()
    {
        ScoreText.text = "Score: " + Score.ToString();
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("Score"));
    }
	
    void GearSpawn()
    {
        if (ListofGearEnemies.Count < 5)
        {
            GameObject GearTemp = Instantiate(GearObject, (Vector2)PlayerObject.transform.position + new Vector2(Random.Range(-10, 10), 10), Quaternion.identity) as GameObject;
            ListofGearEnemies.Add(GearTemp);
        }
    }

    void Update ()
    {
		
	}
}
