using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Hareket Ayarları : ")]
    public float moveSpeed;
    public float swipeSpeed;
    float touchPosX;


    void Update()
    {
        LevelBoundaries();
        if (this.gameObject.transform.position.y < 37 ) 
        {
            Time.timeScale = 0;
            UIManager.Instance.panelElements[0].SetActive(true);
        }
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        if (TouchController.Instance.canMove)
        {
            touchPosX = TouchController.Instance.touch.deltaPosition.x * Time.deltaTime * swipeSpeed;
            transform.position += Vector3.right * touchPosX;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickupable" || other.tag == "Fail")
        {
            if (PickupObject.carriedObject == null)
            {
                PickupObject.carriedObject = other.gameObject;
                PickupObject.carrying = true;
                if (PickupObject.carriedObject != null)
                {
                    other.gameObject.GetComponent<Animator>().SetBool("Carried", true);
                }
            }
        }
        if(other.tag == "Obstacle")
        {
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            PickupObject.carrying = false;
            this.gameObject.GetComponentInChildren<Animator>().SetBool("Lifting", false);
            this.gameObject.GetComponentInChildren<Animator>().SetBool("Lose",true);
            StartCoroutine(WaitForFallAnim());
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="FinishLine")
        {
            StartCoroutine(WaitForStay());
        }
    }
    void LevelBoundaries()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -3.66f, 2.34f);
        transform.position = pos;
    }
    IEnumerator WaitForStay()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.GetComponentInChildren<Animator>().SetBool("Win",true);
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(1f);
        UIManager.Instance.panelElements[1].SetActive(true);
    }

    IEnumerator WaitForFallAnim()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.panelElements[0].SetActive(true);
    }
}

