using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTry : MonoBehaviour
{
    void OnTriggerEnter(Collider myTrigger)
    {
        if (myTrigger.gameObject.name == "CheckerHitBall" &&
            myTrigger.transform.parent.GetComponent<Rigidbody>().velocity.y > 0)
        {
            GameController.isTry = true;
        }
    }
}
