using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBox;
    [SerializeField] GameObject go_NameBox;

    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_Name;

    Dialogue[] dialogues;


    bool isDialogue = false; // 대화중일 경우 true
    bool isNext = false; // 특정 키 입력 대기


    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;


    int lineCount = 0; // 대화카운트
    int contextCount = 0; //대사카운트

    //InteractionController theIC;
    //CameraController theCam;
    //SpriteManager theSpriteManager;
    //SplashManager theSplashManager;
    //CutSceneManager theCutSceneManager;

    
    void Start()
    {
        //theIC = FindObjectOfType<InteractionController>();
        //theCam = FindObjectOfType<CameraController>();
        //theSpriteManager = FindObjectOfType<SpriteManager>();
        //theSplashManager = FindObjectOfType<SplashManager>();
        //theCutSceneManager = FindObjectOfType<CutSceneManager>();
    }
    
    

    void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isNext = false;
                    txt_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if(++lineCount < dialogues.Length)
                        {
                            //StartCoroutine(CameraTargettingType()); 
                        }
                        else
                        {
                            //StartCoroutine(EndDialogue());
                        }
                    }
                }
            }
        }

    }



    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";
        //theIC.SettingUI(false);
        dialogues = p_dialogues;

        //theCam.CamOriginSetting();

        //StartCoroutine(CameraTargettingType());

        //CameraTargettingType();

        StartCoroutine(TypeWriter());
    }


    /*
    IEnumerator CameraTargettingType()
    {
       

        switch (dialogues[lineCount].cameraType)
        {
            case CameraType.FadeIn: SettingUI(false); SplashManager.isfinished = false; StartCoroutine(theSplashManager.FadeIn(false, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FadeOut: SettingUI(false); SplashManager.isfinished = false; StartCoroutine(theSplashManager.FadeOut(false, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FlashIn: SettingUI(false); SplashManager.isfinished = false; StartCoroutine(theSplashManager.FadeIn(true, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;
            case CameraType.FlashOut: SettingUI(false); SplashManager.isfinished = false; StartCoroutine(theSplashManager.FadeOut(true, true)); yield return new WaitUntil(() => SplashManager.isfinished); break;


            case CameraType.ObjectFront: theCam.CameraTargetSetting(dialogues[lineCount].tf_Target); break;
            case CameraType.Reset: theCam.CameraTargetSetting(null, 0.05f, true, false); break;


            case CameraType.ShowCutScene:
                SettingUI(false); CutSceneManager.isFinished = false;
                StartCoroutine(theCutSceneManager.CutSceneCoroutine(dialogues[lineCount].spriteName[contextCount], true));
                yield return new WaitUntil(()=>CutSceneManager.isFinished);
                break;

            case CameraType.HideCutScene:
                SettingUI(false); CutSceneManager.isFinished = false;
                StartCoroutine(theCutSceneManager.CutSceneCoroutine(null, false));
                yield return new WaitUntil(() => CutSceneManager.isFinished);
                theCam.CameraTargetSetting(dialogues[lineCount].tf_Target);
                break;

        }
        

       //StartCoroutine(TypeWriter());

    } */

    /*

    IEnumerator EndDialogue()
    {
        

        if (theCutSceneManager.ChectCutScene())
        {
            SettingUI(false); 
            CutSceneManager.isFinished = false;
            StartCoroutine(theCutSceneManager.CutSceneCoroutine(null, false));
            yield return new WaitUntil(() => CutSceneManager.isFinished);
        }
   
    isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        //theCam.CameraTargetSetting(null, 0.008f, true, true); // 대화 후 카메라 리셋 속도

    }
*/

    void ChangingSprite()
    {
        /*
        if(dialogues[lineCount].tf_Target != null)
        {
            if (dialogues[lineCount].spriteName[contextCount] != "")
            {
                StartCoroutine(theSpriteManager.SpriteChangeCoroutine(
                                                dialogues[lineCount].tf_Target,
                                                dialogues[lineCount].spriteName[contextCount]));
            }
        }
        */
    }

    void PlaySound()
    {
        if(dialogues[lineCount].VoiceName[contextCount] != "")
        {
            //SoundManager.instance.PlaySound(dialogues[lineCount].VoiceName[contextCount], 2);

        }


    }





    IEnumerator TypeWriter()
    {

        SettingUI(true);
        ChangingSprite();
        PlaySound();



        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");      // CSV를 읽는 도중 ' 를 ,로 변환하는 코드 
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");


        //글자색 변경
        bool t_silver = false;
        bool t_white = false;
        bool t_yellow = false;

        bool t_ignore = false;


        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            switch (t_ReplaceText[i])
            {
                case 'ⓦ': t_white = true; t_yellow = false; t_silver = false; t_ignore = true; break;
                case 'ⓨ': t_white = false; t_yellow = true; t_silver = false; t_ignore = true; break;
                case 'ⓢ': t_white = false; t_yellow = false; t_silver = true;  t_ignore = true; break;
                //case '①': StartCoroutine(theSplashManager.Splash()); SoundManager.instance.PlaySound("Emotion0", 1); t_ignore = true; break;
                //case '②': StartCoroutine(theSplashManager.Splash()); SoundManager.instance.PlaySound("Emotion1", 1); t_ignore = true; break;
            }

            string t_letter = t_ReplaceText[i].ToString();

            if(!t_ignore)
            {
                if (t_white) { t_letter = "<color=#ffffff>" + t_letter + "</color>"; }
                else if (t_yellow) { t_letter = "<color=#ffff00>" + t_letter + "</color>"; }
                else if (t_silver) { t_letter = "<color=#BABABA>" + t_letter + "</color>"; }
                txt_Dialogue.text += t_letter;

            }
            t_ignore = false;

            yield return new WaitForSeconds(textDelay); 
        }

        isNext = true;



    }



    void SettingUI(bool p_flag)
    {
        go_DialogueBox.SetActive(p_flag);

        if (p_flag)
        {
            if(dialogues[lineCount].name == "")
            {
                go_NameBox.SetActive(false);
            }else
            {
                go_NameBox.SetActive(true);
                txt_Name.text = dialogues[lineCount].name;
            }
        }
        else
        {
            go_NameBox.SetActive(false);
        }

    }



}
