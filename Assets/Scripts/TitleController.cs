using System.Data;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public Text highScoreText;
    // 追加
    public Text SetMessage;
    public GameObject messagePanel;
    public Text DBtext;
    // 追加

    public void Start()
    {
        // ハイスコアを表示
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore") + "m";

        // 追加
        messagePanel.SetActive(false);
        DBtext.text = "";
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    // 追加↓

    // メッセージラベルの表示
    public void Confirmation()
    {
        messagePanel.SetActive(true);
    }

    // ハイスコアの削除
    public void OnDeleteButtonClicked()
    {
        PlayerPrefs.SetInt("HighScore", 0);

        // ハイスコアを表示
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("HighScore") + "m";
    }

    public void InsertDataBase()
    {
        try
        {

        }
        catch (Exception e)
        {
            DBtext.text = ("データベースに登録を失敗しました。" + e.Message);
        }
        finally
        {

        }
    }



    // 追加↑
}
