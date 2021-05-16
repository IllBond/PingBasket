using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


class GameController : MonoBehaviour
{
    public Transform Backet;
    public GameObject gameOver;
    public Text value;
    public Transform cloud; // Фон
    public Transform water; // Фон


    public static bool isTry = false; // Если пересечь линию то попытка заброситьбудет считаться
    public static bool isContactBallwithFlipper = false; // Контактирует ли мяч с флиппером?
    public static bool isButtonDown = false; 
    public static bool isButtonOnce = false; // Проверочная функция. Что бы при зажатой кнопке функция не работала бесконечно, но при этом флипер был поднят
    public static bool isRingTouch = false; // Сброс если дотронулся до пола (что бы не засчитывать ложные очки)

    public static int health = 10;

    private bool isFadeBacket = false; // Вспомогательная для исчезания кольца
    private bool isShowBacket = false; // Вспомогательная для появления кольца
    private bool BacketRotation = false; // Вспомгательная для разворота кольца
    private float BacketPosMin = 1f; // Рандомный диапазон по Y
    private float BacketPosMax = 2.73f; // Рандомный диапазон по Y
    private float BacketPosLeft = -3.93f; // Крайнее положение слева
    private float BacketPosRight = 0.48f; // Крайнее положение справа

    private float FadePos; // Вспомогательная для исчезновения. Записываем крайнее левое и праваое положение +-3

    private float SpeedCloud = 10; //Фон. Скорость облоков
    private float StartCloud = -13.01f; //Фон. Старт анимации
    private float EndCloud = 19.39f; //Фон. Конец анимации

    private float speedWater = 2f; //Фон. Скорость волны
    private float StartWater = -1.9f;//Фон. Старт анимации
    private float EndWater = -3.4f; //Фон. Конец анимации


    void Start() {
        value.text = " "+ health;
    }

    void Update()
    {
        BacketFade();
        BacketShow();
        backgroundMove();
    }



    public void BacketFade() {
        if (isFadeBacket) {
            if (BacketRotation)
            {
                if (Mathf.Abs(FadePos - Backet.position.x) > 0.3f)
                {
                    Backet.position = new Vector3(BacketPosLeft - 3, Random.Range(BacketPosMin, BacketPosMax), Backet.position.z);
                    BacketRotation = false;
                    Backet.position = Vector3.Lerp(Backet.position, new Vector3(FadePos, Backet.position.y, Backet.position.z), Time.deltaTime * 10);
                    Backet.Rotate(0, 180, 0, 0);
                    isFadeBacket = false;
                    isShowBacket = true;
                }
            } else {
                if (Mathf.Abs(FadePos - Backet.position.x) > 0.3f) {
                    Backet.position = new Vector3(BacketPosRight + 3, Random.Range(BacketPosMin, BacketPosMax), Backet.position.z);
                    BacketRotation = true;
                    Backet.position = Vector3.Lerp(Backet.position, new Vector3(FadePos, Backet.position.y, Backet.position.z), Time.deltaTime * 10);
                    Backet.Rotate(0, 180, 0, 0);
                    isFadeBacket = false;
                    isShowBacket = true;
                }
            }
        }
    }    
    
    public void BacketShow() {
        if (isShowBacket) {
            if (!BacketRotation)
            {
                 Backet.position = Vector3.Lerp(Backet.position, new Vector3(BacketPosLeft, Backet.position.y, Backet.position.z), Time.deltaTime * 10);
                 if (Mathf.Abs(BacketPosRight - Backet.position.x) < 0.3f)
                    {
                        Backet.position = new Vector3(BacketPosLeft, Backet.position.y, Backet.position.z);
                        isShowBacket = false;
                    }
            } else {
                 Backet.position = Vector3.Lerp(Backet.position, new Vector3(BacketPosRight, Backet.position.y, Backet.position.z), Time.deltaTime * 10);
                 if (Mathf.Abs(BacketPosLeft - Backet.position.x) < 0.3f)
                    {
                        Backet.position = new Vector3(BacketPosRight, Backet.position.y, Backet.position.z);
                        isShowBacket = false;
                    }
            }
        }
    }

    public void SwapBacket ()
    {
        isFadeBacket = true;
        if (BacketRotation) {
            FadePos = Backet.position.x + 3;
        } else {
            FadePos = Backet.position.x - 3;
        }
    }


    private void backgroundMove() {
        cloud.position = Vector3.Lerp(cloud.position, new Vector3(EndCloud, cloud.position.y, cloud.position.z), Time.deltaTime / SpeedCloud);

        if (cloud.position.x > EndCloud)
        {
            float temp = StartCloud;
            StartCloud = EndCloud;
            EndCloud = temp;
        }

        water.position = Vector3.Lerp(water.position, new Vector3(water.position.x, EndWater, water.position.z), Time.deltaTime / speedWater);

        if (Mathf.Abs(EndWater - water.position.y) < 0.3f)
        {
            float temp = StartWater;
            StartWater = EndWater;
            EndWater = temp;
        }
    }

    public void RingChecker(bool state)
    {
        isRingTouch = state;
    }

    public void Damage()
    {
        if (health > 0)
        {
            health--;
            if (health == 0)
            {
                gameOver.SetActive(true);
            }
            value.text = " " + health;
        }
    }
}
