using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {
    GameObject Player;
    public GameObject hpupPrefab;
    List<GameObject> hpupList = new List<GameObject>();
    public GameObject oilPrefab;
    List<GameObject> oilList = new List<GameObject>();
    public GameObject CoalPrefab;
    List<GameObject> CoalList = new List<GameObject>();
    public GameObject LeadPrefab;
    List<GameObject> LeadList = new List<GameObject>();

    int OilIndex; // obj pooling
    int hpupIndex; // obj pooling
    int LeadIndex; // obj pooling
    int CoalIndex; // obj pooling

    // Use this for initialization
    void Start ()
    {
        Player = GameObject.Find("Player").gameObject;

        for (int i = 0; i < 5; i++)
        {
            GameObject Oil = Instantiate(oilPrefab, new Vector2(300, 300), transform.rotation);
            Oil.GetComponent<Rigidbody2D>().isKinematic = true;
            oilList.Add(Oil);
            GameObject HpUP = Instantiate(hpupPrefab, new Vector2(300, 300), transform.rotation);
            HpUP.GetComponent<Rigidbody2D>().isKinematic = true;
            hpupList.Add(HpUP);

            GameObject Coal = Instantiate(CoalPrefab, new Vector2(300, 300), transform.rotation);
            Coal.GetComponent<Rigidbody2D>().isKinematic = true;
            CoalList.Add(Coal);
            GameObject Lead = Instantiate(LeadPrefab, new Vector2(300, 300), transform.rotation);
            Lead.GetComponent<Rigidbody2D>().isKinematic = true;
            LeadList.Add(Lead);
        }

        StartCoroutine("DropHpUpPowers");
        StartCoroutine("DropOilPowers");
        StartCoroutine("DropLeadPowers");
        StartCoroutine("DropCoalPowers");

    }

    IEnumerator DropOilPowers()
    {
        OilIndex++;
        if (OilIndex >= 4)
            OilIndex = 0;
        oilList[OilIndex].transform.position = (Vector2)Player.transform.position + new Vector2(Random.Range(-10, 10), 10);
        oilList[OilIndex].GetComponent<Rigidbody2D>().isKinematic = false;
        yield return new WaitForSeconds(Random.Range(4, 8));
        StartCoroutine(ChangePositionOfPowerUps(oilList[OilIndex].transform));
        StartCoroutine("DropOilPowers");
    }

    IEnumerator DropHpUpPowers()
    {
        hpupIndex++;
        if (hpupIndex >= 4)
            hpupIndex = 0;
        hpupList[hpupIndex].transform.position = (Vector2)Player.transform.position + new Vector2(Random.Range(-10, 10), 10);
        hpupList[hpupIndex].GetComponent<Rigidbody2D>().isKinematic = false;
        yield return new WaitForSeconds(Random.Range(5, 8));
        StartCoroutine(ChangePositionOfPowerUps(hpupList[hpupIndex].transform));
        StartCoroutine("DropHpUpPowers");
    }

    IEnumerator DropLeadPowers()
    {
        LeadIndex++;
        if (LeadIndex >= 4)
            LeadIndex = 0;
        LeadList[LeadIndex].transform.position = (Vector2)Player.transform.position + new Vector2(Random.Range(-10, 10), 10);
        LeadList[LeadIndex].GetComponent<Rigidbody2D>().isKinematic = false;
        yield return new WaitForSeconds(Random.Range(5, 10));
        StartCoroutine(ChangePositionOfPowerUps(LeadList[LeadIndex].transform));
        StartCoroutine("DropLeadPowers");
    }

    IEnumerator DropCoalPowers()
    {
        CoalIndex++;
        if (CoalIndex >= 4)
            CoalIndex = 0;
        CoalList[CoalIndex].transform.position = (Vector2)Player.transform.position + new Vector2(Random.Range(-10, 10), 10);
        CoalList[CoalIndex].GetComponent<Rigidbody2D>().isKinematic = false;
        yield return new WaitForSeconds(Random.Range(3, 6));
        StartCoroutine(ChangePositionOfPowerUps(CoalList[CoalIndex].transform));
        StartCoroutine("DropCoalPowers");
    }

    IEnumerator ChangePositionOfPowerUps(Transform obj)
    {
        yield return new WaitForSeconds(4);
        obj.transform.position = new Vector2(300, 300);
        obj.GetComponent<Rigidbody2D>().isKinematic = true;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
