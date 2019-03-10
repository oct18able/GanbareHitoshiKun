using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_CloseHint : MonoBehaviour
{
    //ヒントの格納
    public GameObject hint;

    //クリック時にヒントをしまう
    public void OnClickCloseHint()
    {
        hint.SetActive(false);
    }



}
