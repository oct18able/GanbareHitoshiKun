using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2_Quiz2_InputManager : MonoBehaviour
{
    //InputFieldの用意
    InputField inputField;

    //正解の単語の格納
    public string correctAnswer;
    
    //正解時に問題を切り替えるため、現在の問題と次の問題を格納
    public GameObject thisQuiz;
    public GameObject nextQuiz;

    //正解不正解画像の格納
    public GameObject correctImg;
    public GameObject uncorrectImg;

    //ヒントボタンの格納
    public GameObject openHint;


    /// InputFieldコンポーネントの取得と初期化メソッドの実行
    void Start()
    {

        inputField = GetComponent<InputField>();

        InitInputField();
    }


    //入力値を取得して正解と比較、その後初期化
    public void InputLogger()
    {
        //入力された文字列の取得
        string inputValue = inputField.text;
        
        //フィールドに格納されたデータと、答えを比較
        CheckAnswer(inputValue);

        //フィールドの初期化
        InitInputField();
    }


    //入力値をリセットする関数
    void InitInputField()
    {

        // 値をリセット
        inputField.text = "";
                
    }

    //答えを比較する関数
    void CheckAnswer(string Answer)
    {
        //正解の時
        if (Answer.Equals(correctAnswer))
        {
            correctImg.SetActive(true); //正解画像の表示
            Invoke("SetNextQuiz", 1.0f);    //1秒後次のクイズの表示
        }

        //不正解の時
        if (Answer.Equals(correctAnswer) == false)
        {
            uncorrectImg.SetActive(true);   //不正解画像の表示
            Invoke("ResetUncorrect", 1.0f); //1秒後不正解画像を消し、ヒントボタンの表示する関数の起動
        }

    }

    //次のクイズへと変更する関数
    void SetNextQuiz()
    {
        correctImg.SetActive(false);    //正解画像の消去

        thisQuiz.SetActive(false);  //現在のクイズの消去
        nextQuiz.SetActive(true);   //次のクイズの表示
    }

    //不正解の時のリセット
    void ResetUncorrect()
    {
        uncorrectImg.SetActive(false);  //不正解画像の消去
        openHint.SetActive(true);   //ヒントボタンの表示
    }







}
