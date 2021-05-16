using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerScore : MonoBehaviour
{

    private GameController gameController;
    void Start()
    {
        gameController = FindObjectsOfType<GameController>()[0];
    }

    void OnTriggerEnter(Collider myTrigger)
    {
        if (myTrigger.gameObject.name == "CheckerHitBall" 
            &&  myTrigger.transform.parent.GetComponent<Rigidbody>().velocity.y < 0
            && GameController.isRingTouch
            )
        {
            GameController.isTry = false;
            gameController.SwapBacket();
            gameController.RingChecker(false);
   
        }
    }




}
