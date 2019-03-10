using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroTextController : MonoBehaviour
{
    //21行目まで、テキスト制御の変数
    public string[] scenarios;
    [SerializeField] Text uiText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int currentLine = 0;
    private int lastUpdateCharacter = -1;


    //背景2種類の変数
    public GameObject homeCanvas;
    public GameObject bookCanvas;

    //会話文をどれだけ読み進めたかのカウンター
    private int textCounter=0;

    //服を着る音
    public GameObject se_puttingon;

    //二人の画像を格納
    public GameObject hitoshi_normal_img;
    public GameObject timu_normal_img;
    public GameObject hitoshi_dirty_img;
    public GameObject timu_dirty_img;
    public GameObject timu_shiny_img;

    //二人の画像を表示させるタイミングを行列で格納
    public int[] hitoshi_normal;
    public int[] timu_normal;

    //背景の切り替え変数
    public int display_bookCanvas;
    public int display_homeCanvas;

    //場面の切り替え変数
    public int sceneChange_Number;

    //SEを鳴らす変数
    public int se_counter;


    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    //開始時の処理
    void Start()
    {
        SetNextLine();
        
    }

    //フレームごとの処理
    void Update()
    {
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
            if (currentLine < scenarios.Length && Input.GetMouseButtonDown(0))
            {
                SetNextLine();
            }
        }
        else
        {
            // 完了してないなら文字をすべて表示する
            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }
        }

        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }

    //次の行を表示する
    void SetNextLine()
    {
        

        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;

        //背景の切り替え
        CanvasChange(textCounter);

        //服を着るタイミングでSE流す
        SE_PutOn(textCounter);

        //二人の画像の表示
        Display_Hitoshi_Normal_Img(hitoshi_normal, textCounter);
        Display_Timu_Normal_Img(timu_normal, textCounter);

        //会話文の進行をカウントする
        textCounter++;

        //会話終了ならば次の場面へ移動
        SceneChange(textCounter);


    }
    

    //背景の変化関数
    void CanvasChange(int changenumber)
    {
        if(changenumber == display_bookCanvas)
        {
            homeCanvas.SetActive(false);
            bookCanvas.SetActive(true);
        }
        if (changenumber == display_homeCanvas)
        {
            homeCanvas.SetActive(true);
            bookCanvas.SetActive(false);
        }
    }

    
    //場面の変化関数
    void SceneChange(int changeNumber)
    {
        if (changeNumber == sceneChange_Number)
        {
            SceneManager.LoadScene("Stage1_Story");
        }
    }

    

    //SE(服を着る)
    void SE_PutOn(int changeNumber)
    {
        if (changeNumber == se_counter)
        {
            se_puttingon.SetActive(true);
        }
    }

    //ヒトシの画像(普通)を表示する
    void Display_Hitoshi_Normal_Img(int[] array,int changeNumber)
    {
        
        for(int i = 0; i < array.Length; i++)
        {
            if (array[i] == changeNumber)
            {
                hitoshi_normal_img.SetActive(true);
                break;
            }
            else
            {
                hitoshi_normal_img.SetActive(false);
            }
        }
    }


    //ティムの画像(普通)を表示する
    void Display_Timu_Normal_Img(int[] array, int changeNumber)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == changeNumber)
            {
                timu_normal_img.SetActive(true);
                break;
            }
            else
            {
                timu_normal_img.SetActive(false);
            }
        }
    }



}
