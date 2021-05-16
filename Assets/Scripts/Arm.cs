using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{

    public float test = 1;//= 0f
    public float test2 = 1;//= 0f
    public float restPosition;//= 0f
    public float pressetPosition;//= 45f
    public float maxPressetPosition;//= 45f
    public float hitStrenght;//= 10000f
    public float flipDamper;//= 150f
    private HingeJoint hinge;
    private GameController gameController;

    void Start() {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
        gameController = FindObjectsOfType<GameController>()[0];
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            GameController.isContactBallwithFlipper = true;
            
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.name == "Ball" )
        {
            GameController.isContactBallwithFlipper = false;
        }
    }

    void Update() {

        JointSpring spring = new JointSpring();
        spring.spring = hitStrenght;
        spring.damper = flipDamper;

        if (GameController.isButtonDown)
        {
            test = Time.time - test2;
            spring.targetPosition = pressetPosition * test * 40;

            if (GameController.isContactBallwithFlipper && !GameController.isButtonOnce) {
                gameController.RingChecker(false);
                GameController.isButtonOnce = true;
            }
        } else {
            spring.targetPosition = restPosition;
            test = 1;
            test2 = Time.time;
        }

        hinge.spring = spring;
        hinge.useLimits = true;

    }

    public void ButtonDown()
    {
        GameController.isButtonDown = true;
    }

    public void ButtonUp() {
        GameController.isButtonDown = false;
        GameController.isButtonOnce = false;
    }

}
