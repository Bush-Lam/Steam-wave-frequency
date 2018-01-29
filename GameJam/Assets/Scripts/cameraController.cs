using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
    public GameObject player;
    public GameObject leftLimit;
    public GameObject rightLimit;

	// Update is called once per frame
	void Update () {
        float playerX = player.transform.position.x;
        Vector3 newCamPos = new Vector3(playerX,
                                        transform.position.y,
                                        transform.position.z);

        transform.position = newCamPos;

        if (transform.position.x < leftLimit.transform.position.x) {
            transform.position = new Vector3(leftLimit.transform.position.x,
                                             transform.position.y,
                                             transform.position.z);
        }

        if (transform.position.x > rightLimit.transform.position.x) {
            transform.position = new Vector3(rightLimit.transform.position.x,
                                             transform.position.y,
                                             transform.position.z);
        }
	}
}
