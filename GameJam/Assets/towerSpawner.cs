using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerSpawner : MonoBehaviour {
    public int numOfTowers;
    public int minX;
    public int maxX;
    public int y;
    public GameObject tower;
    public int limit;

    void Start() {
        int lastX = 0;
        for (int i = 0; i < numOfTowers; i++) {
            int x = 0;
            do {
                x = Random.Range(minX, maxX);

            } while (Mathf.Abs((float)(x) - lastX) < limit);

            Instantiate(tower,
                        new Vector2(x, 0),
                        Quaternion.identity);
        }
	}
}
