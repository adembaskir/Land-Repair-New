using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [Header("Ta��n�lacak Objeyi Kendi At�yor Dokunmay�n:  ")]
    [SerializeField]public static GameObject carriedObject;
    [Header("Ta��yac� Transformu at�yoruz: (Guide yazan)")]
    public GameObject tempParent;
    [Header("Playerin i�inde bulunan Player.Land Objesini Aktar: ")]
    public GameObject otherObject;
    Animator otherAnimator; 
    [Header("Kontrol Ama�l� Booleanler: ")]
    [SerializeField]public static bool carrying;
    public bool big, small,middle;


    public void Start()
    {
        carrying = false;
        otherAnimator = otherObject.GetComponent<Animator>();
        
    }
    void Update()
    {
        if (carrying == true)
        {

            carriedObject.GetComponent<Rigidbody>().isKinematic = false;
            carriedObject.GetComponent<Rigidbody>().useGravity = false;
            carriedObject.transform.position = Vector3.Slerp(carriedObject.transform.position, tempParent.transform.position, 5 * Time.deltaTime);
            carriedObject.transform.parent = tempParent.transform;
            otherAnimator.SetBool("Lifting", true);

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Portal+")
        {
            Debug.Log("Artt�r�c�");
            carriedObject.transform.localScale += new Vector3(0.45f, 0.45f, 0.45f);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

        }
        if (other.tag == "Portal-")
        {
           
            carriedObject.transform.localScale -= new Vector3(0.45f, 0.45f, 0.45f);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("D���r�c�");

        }
    }

}
