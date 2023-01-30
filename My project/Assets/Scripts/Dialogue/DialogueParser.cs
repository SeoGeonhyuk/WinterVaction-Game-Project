using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

데이터 패서 / Excel CSV 파일 데이터를 유니티로 임포트하는 스크립트

*/

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); // 대화 리스트 생성

        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); // csv 파일 가져옴 (Resources 폴더에 있는 파일을 가져옴)

        string[] data = csvData.text.Split(new char[] { '\n' }); // 엔터(한 줄 한 줄) 기준으로 쪼갬

        

        for (int i = 5; i < data.Length;) // 시트의 5번 부터 반복 (0부터 시작임!) // 시작부분 정하는 줄
        {
            string[] row = data[i].Split(new char[] { ',' }); // 쉼표 기준으로 쪼갬

            Dialogue dialogue = new Dialogue(); // ...?

            dialogue.name = row[1];
            
            List<string> contextList = new List<string>();  // 문장 리스트 생성
            List<string> spriteList = new List<string>();   // 스프라이트 리스트 생성
            List<string> VoiceList = new List<string>();    // 보이스 리스트 생성


            do
            {
                contextList.Add(row[2]);                    // 반복문으로 문장리스트에 2번 열 (0부터 시작임) CSV 데이터 넣기
                spriteList.Add(row[3]);                     // 반복문으로 문장리스트에 3번 열 (0부터 시작임) CSV 데이터 넣기             
                VoiceList.Add(row[4]);                      // 반복문으로 문장리스트에 4번 열 (0부터 시작임) CSV 데이터 넣기

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
                
            } while(row[0].ToString() == "");

            //Debug.Log(data[i]); // Parse 테스트


            dialogue.contexts = contextList.ToArray();

            dialogue.spriteName = spriteList.ToArray();

            dialogue.VoiceName = VoiceList.ToArray();

            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();


    }

    /*

    private void Start() // Parse 테스트
    {
        Debug.Log("dzdz");
        Parse("test");
    }
    */

}
