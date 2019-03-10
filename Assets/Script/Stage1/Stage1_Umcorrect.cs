using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Umcorrect : MonoBehaviour
{

    public GameObject uncorrectImg; //不正解画像の格納
    public GameObject openHint; //ヒントボタンの格納
    
    //不正解をクリックしたとき
    public void OnClickUncorrrect()
    {
        uncorrectImg.SetActive(true);   //不正解画像の表示
        Invoke("AppearOpenHint", 1.0f); //1秒後にヒントボタンを表示
    }

    //ヒントボタンの表示関数
    private void AppearOpenHint()
    {
        uncorrectImg.SetActive(false);  //不正解画像を消去
        openHint.SetActive(true);   //ヒントボックスを表示
    }


}
