using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerFloorCollision : MonoBehaviour
{
    private GameController gameController;

    void Start() {
        gameController = FindObjectsOfType<GameController>()[0];
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            if (GameController.isTry) {
                gameController.Damage();
                GameController.isTry = false;
            }
        }
    }
}
