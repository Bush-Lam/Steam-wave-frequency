using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timekeeper : MonoBehaviour {
    public float roundTime;
    public float remainingTime;
    public GameObject timeLabel;

	void Start () {
        remainingTime = roundTime;
	}
	
	void Update () {
        // Lower remaining time and update time label
        remainingTime -= Time.deltaTime;
        timeLabel.GetComponent<Text>().text = ((int)(remainingTime)).ToString();
        if ((int)(remainingTime) <= 0) {
            // Lose
        }
	}
}
