using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    [Header("Topladýðý Altýn Anlamýnda:")]
    GameObject myObject;
    [Header("Toplanan Altýnýn Gideceði Konum: ")]
    public GameObject meter;
    bool onHit;
    private void Start()
    {
        myObject = this.gameObject;   
    }
    void Update()
    {
        if (onHit == true)
        {
            myObject.transform.position = Vector3.Lerp(transform.position, meter.transform.position, 2f * Time.deltaTime);
            Destroy(this.gameObject,2f);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ScoreManager.score++;
            PlayerPrefs.SetInt("CoinAmount", ScoreManager.score);
            onHit = true;
            //other.gameObject.SetActive(false);
        }
    }
}
