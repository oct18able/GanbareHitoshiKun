using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage3_Story_textController : MonoBehaviour
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


    //会話文をどれだけ読み進めたかのカウンター
    private int textCounter = 0;

    //水をしまうボトルの画像格納
    public GameObject waterImg;

    //各種BGMとSEの格納
    public GameObject bgm_odayaka;
    public GameObject se_houkai;
    public GameObject bgm_serious;

    //二人の画像を格納
    public GameObject hitoshi_normal_img;
    public GameObject timu_normal_img;
    public GameObject hitoshi_dirty_img;
    public GameObject timu_dirty_img;
    public GameObject timu_shiny_img;

    //二人の画像を表示させるタイミングを行列で格納
    public int[] hitoshi_normal;
    public int[] timu_normal;

    //ビンの画像を表示するカウンター
    public int[] waterBottle;

    //BGMとSEの変更する為のカウンター
    public int[] bgmse_Counter;

    //場面の切り替え変数
    public int sceneChange_Number;



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

        //水の入ったボトルの画像表示
        ImgChange(textCounter);

        //BGMとSEの変更
        BGMChange(textCounter);

        //二人の画像の表示
        Display_Hitoshi_Normal_Img(hitoshi_normal, textCounter);
        Display_Timu_Normal_Img(timu_normal, textCounter);

        //会話文の進行をカウントする
        textCounter++;

        //会話終了ならば次の場面へ移動
        SceneChange(textCounter);

    }

   

    //水をビンに入れた画像の表示関数
    void ImgChange(int changeNumber)
    {
        //画像表示
        if (changeNumber == waterBottle[0])
        {
            waterImg.SetActive(true);
        }
        //画像消去
        if (changeNumber == waterBottle[1])
        {
            waterImg.SetActive(false);
        }
    }

    //場面の変化関数
    void SceneChange(int changeNumber)
    {
        if (changeNumber == sceneChange_Number)
        {
            SceneManager.LoadScene("Stage3_Quiz");
        }
    }

    
    //BGMとSEを変更する関数
    void BGMChange(int changeNumber)
    {
        if (changeNumber == bgmse_Counter[0])
        {
            bgm_odayaka.SetActive(false);
            se_houkai.SetActive(true);
        }
        if (changeNumber == bgmse_Counter[1])
        {
            bgm_serious.SetActive(true);
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
