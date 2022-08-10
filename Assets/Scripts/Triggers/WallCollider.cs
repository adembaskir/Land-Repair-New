using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallCollider : MonoBehaviour
{
    [Header("Taþýnýlan Obje Transformu, Taþýnýlan Objenin Ýlk Hedefi , Sonraki Hedefi Tanýmlayýnýz: ")]
    public Transform pickuped;
    public Transform pickupedTarget;
    public Transform pickupNextTarget;
    [Header("Kontrol Amaçlý Booleanler")]
    public bool triggered;
    public bool nextPos;
    [Header("Hedefe gidiþ hýzý")]
    public float pickupedMoveSpeed;
    PickupObject pickupScript;
    private void Start()
    {
       pickupScript = FindObjectOfType<PickupObject>();
    }
    void Update()
    {
        if (triggered == true)
        {

                pickuped.transform.position = Vector3.MoveTowards(pickuped.transform.position, pickupedTarget.transform.position, pickupedMoveSpeed * Time.deltaTime);
                nextPos = true;
        }
        if (nextPos==true)
        {
                StartCoroutine(WaitForTrigger());
                pickuped.transform.position = Vector3.MoveTowards(pickuped.transform.position, pickupNextTarget.transform.position, pickupedMoveSpeed * Time.deltaTime);
                pickuped.transform.rotation = pickupNextTarget.transform.rotation;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pickupable")
        {
            if (PickupObject.carriedObject != null)
            {
                if (PickupObject.carriedObject.transform.localScale.x == 1f && PickupObject.carriedObject.transform.localScale.y == 1f && PickupObject.carriedObject.transform.localScale.z == 1)
                {
                    triggered = true;
                    PickupObject.carrying = false;
                    pickupScript.otherObject.GetComponent<Animator>().SetBool("Lifting", false);
                    if (PickupObject.carriedObject != null)
                    {
                        PickupObject.carriedObject.transform.SetParent(null);
                        PickupObject.carriedObject.GetComponent<BoxCollider>().isTrigger = false;
                        PickupObject.carriedObject.GetComponent<Rigidbody>().isKinematic = true;
                        PickupObject.carriedObject = null;

                        other.gameObject.GetComponent<Animator>().SetBool("Carried", false);
                    }
                }
                else
                {
                    PickupObject.carrying = false;
                    pickupScript.otherObject.GetComponent<Animator>().SetBool("Lifting", false);
                    pickupScript.otherObject.GetComponent<Animator>().SetBool("Lose", true);
                    FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().enabled = false;
                    StartCoroutine(WaitForFallAnim());
                }
            }
        }
        if(other.tag == "Fail")
        {
            PickupObject.carrying = false;
            pickupScript.otherObject.GetComponent<Animator>().SetBool("Lifting", false);
            pickupScript.otherObject.GetComponent<Animator>().SetBool("Lose", true);
            FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(WaitForFallAnim());

        }
    }
    IEnumerator WaitForTrigger()
    {

        yield return new WaitForSeconds(0.3f);
        triggered = false;

    }
    IEnumerator WaitForFallAnim()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.panelElements[0].SetActive(true);
    }
}
