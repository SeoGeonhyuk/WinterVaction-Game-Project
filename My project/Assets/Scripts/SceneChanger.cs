using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public float ChangeTime;
    public string SceneName;

    public GameObject SplashObj;               //�ǳڿ�����Ʈ
    public Image image;                            //�ǳ� �̹���
    private bool checkbool = false;     //������ ���� ������ ����



    void Awake()
    {
        // SplashObj = this.gameObject;                         //��ũ��Ʈ ������ ������Ʈ

        //image = SplashObj.GetComponent<Image>();    //�ǳڿ�����Ʈ�� �̹��� ����
    }


    void Update()
    {
        ChangeTime -= Time.deltaTime;
        if (ChangeTime <= 0)
        {

            //StartCoroutine("MainSplash");                        //�ڷ�ƾ    //�ǳ� ������ ����
            SceneManager.LoadScene(SceneName);
            if (checkbool)                                            //���� checkbool �� ���̸�
            {
                Destroy(this.gameObject);                        //�ǳ� �ı�, ����
            }
        }


    }



    IEnumerator MainSplash()

    {
        Color color = image.color;                            //color �� �ǳ� �̹��� ����

        for (int i = 100; i >= 0; i--)                            //for�� 100�� �ݺ� 0���� ���� �� ����

        {
            color.a -= Time.deltaTime * 0.01f;               //�̹��� ���� ���� Ÿ�� ��Ÿ �� * 0.01
            image.color = color;                                //�ǳ� �̹��� �÷��� �ٲ� ���İ� ����

            if (image.color.a <= 0)                        //���� �ǳ� �̹��� ���� ���� 0���� ������
            {
                SceneManager.LoadScene(SceneName);
                checkbool = true;                              //checkbool �� 
            }
        }

        yield return null;                                        //�ڷ�ƾ ����

    }

}