using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


class GameController : MonoBehaviour
{
    public Transform Backet;
    public GameObject gameOver;
    public Text value;
    public Transform cloud; // ���
    public Transform water; // ���


    public static bool isTry = false; // ���� �������� ����� �� ������� �������������� ���������
    public static bool isContactBallwithFlipper = false; // ������������ �� ��� � ���������?
    public static bool isButtonDown = false; 
    public static bool isButtonOnce = false; // ����������� �������. ��� �� ��� ������� ������ ������� �� �������� ����������, �� ��� ���� ������ ��� ������
    public static bool isRingTouch = false; // ����� ���� ���������� �� ���� (��� �� �� ����������� ������ ����)

    public static int health = 10;

    private bool isFadeBacket = false; // ��������������� ��� ��������� ������
    private bool isShowBacket = false; // ��������������� ��� ��������� ������
    private bool BacketRotation = false; // �������������� ��� ��������� ������
    private float BacketPosMin = 1f; // ��������� �������� �� Y
    private float BacketPosMax = 2.73f; // ��������� �������� �� Y
    private float BacketPosLeft = -3.93f; // ������� ��������� �����
    private float BacketPosRight = 0.48f; // ������� ��������� ������

    private float FadePos; // ��������������� ��� ������������. ���������� ������� ����� � ������� ��������� +-3

    private float SpeedCloud = 10; //���. �������� �������
    private float StartCloud = -13.01f; //���. ����� ��������
    private float EndCloud = 19.39f; //���. ����� ��������

    private float speedWater = 2f; //���. �������� �����
    private float StartWater = -1.9f;//���. ����� ��������
    private float EndWater = -3.4f; //���. ����� ��������


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
