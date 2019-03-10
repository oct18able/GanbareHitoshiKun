using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1_Quiz_TextController : MonoBehaviour
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


    //鍵の画像格納
    public GameObject keyImg;

    ////会話文をどれだけ読み進めたかのカウンター
    private int textCounter = 0;

    //二人の画像を格納
    public GameObject hitoshi_normal_img;
    public GameObject timu_normal_img;
    public GameObject hitoshi_dirty_img;
    public GameObject timu_dirty_img;
    public GameObject timu_shiny_img;

    //二人の画像を表示させるタイミングを行列で格納
    public int[] hitoshi_normal;
    public int[] timu_normal;

    //場面の切り替え変数
    public int sceneChange_Number;

    //鍵の画像を消すカウンター
    public int keyDeleat_Number;

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

        //鍵の画像を消す
        DeleteKeyImg(textCounter);

        Display_Hitoshi_Normal_Img(hitoshi_normal, textCounter);
        Display_Timu_Normal_Img(timu_normal, textCounter);

        textCounter++;

        ChangeScene(textCounter);

    }


    //鍵の画像を消す関数
    void DeleteKeyImg(int changeNumber)
    {
        if (changeNumber == keyDeleat_Number)
        {
            keyImg.SetActive(false);
        }

    }

    

    //場面の変化関数
    void ChangeScene(int changeNumber)
    {
        if (changeNumber == 4)
        {
            SceneManager.LoadScene("Stage2_Story");
        }
    }

    //ヒトシの画像(普通)を表示する
    void Display_Hitoshi_Normal_Img(int[] array, int changeNumber)
    {

        for (int i = 0; i < array.Length; i++)
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
