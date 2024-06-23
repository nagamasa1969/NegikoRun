using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public NejikoController nejiko;
    public Text scoreText;              // ScoreTextの参照
    public LifePanel lifePanel;
    public int score;

    // Update is called once per frame
    void Update()
    {
        // スコアを更新
        score = CalcScore();
        scoreText.text = "Score : " + score + "m";     // テキストの更新

        // ライフパネルを更新
        lifePanel.UpdateLife(nejiko.Life());           // ライフパネルの更新

        // ねじ子のライフが0になったらゲームオーバー
        if(nejiko.Life() <= 0)
        {
            // これ以降のUpdateは止める
            enabled = false;

            // ハイスコアを更新
            if(PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            // 2秒後にRetrunToTitleを呼び出す
            Invoke("ReturnToTitle", 2.0f);
        }
    }

    int CalcScore()
    {
        // ねじ子の走行距離をスコアとする
        return (int)nejiko.transform.position.z;
    }

    void ReturnToTitle()
    {
        // タイトルシーンに切り替え
        SceneManager.LoadScene("Title");
    }
}
