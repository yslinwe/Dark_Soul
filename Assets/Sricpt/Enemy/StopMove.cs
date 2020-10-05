using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMove : MonoBehaviour
{
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponentInParent<Rigidbody>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && rigid != null)
            rigid.isKinematic = true;
    }
    void OnTriggerExit()
    {
        if (rigid != null)
            rigid.isKinematic = false;
    }
}
