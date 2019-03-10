using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Correct : MonoBehaviour
{
    //正解画像
    public GameObject correctImg;

    //正解時に問題を切り替えるため、現在の問題と次の問題を格納
    public GameObject thisQuiz;
    public GameObject nextQuiz;

    //正解がクリックされたときの処理
    public void OnClickCorrect()
    {
        correctImg.SetActive(true); //正解画像表示
        Invoke("SetNextQuiz", 1.0f);    //1秒後に次の問題を表示する
    }

    //現在の問題をリセットして、次の問題を表示
    private void SetNextQuiz()
    {
        correctImg.SetActive(false);
        thisQuiz.SetActive(false);
        nextQuiz.SetActive(true);
    }
}


