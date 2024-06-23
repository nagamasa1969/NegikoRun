using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 30;

    int currentChipIndex;

    public Transform character;                                              // ターゲットキャラクターの作成
    public GameObject[] stageChips;                                          // ステージチッププレハブ配列
    public int startChipindex;                                               // 自動生成開始インデックス
    public int preInstantiate;                                               // 生成先読み個数
    public List<GameObject> generatedStageList = new List<GameObject>();     // 生成済みステージチップ保持リスト
    public GameController GM;                                                // ゲームコントローラー

    // Start is called before the first frame update
    // 初期化処理
    void Start()
    {
        currentChipIndex = startChipindex - 1;
        UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        // キャラクターの位置から現在のステージチップのインデックスを計算
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        // 次のステージチップに入ったらステージの更新を行う
        if(charaPositionIndex + preInstantiate > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiate);
        }
    }

    // 指定のIndexまでのステージチップを生成して、管理下に置く
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;

        // 指定のステージチップまで作成
        for(int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            // 生成したステージチップを管理リストに追加
            generatedStageList.Add(stageObject);
        }

        // ステージ保持上限内になるまで古いステージを削除
        while (generatedStageList.Count > preInstantiate + 2) DestroyoldestStage();

        currentChipIndex = toChipIndex;
    }

    // 指定のインデックス位置にStageオブジェクトをランダム生成
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageCHip = 0;
        if (GM.score >= 300)
        {
            nextStageCHip = Random.Range(0, stageChips.Length);
        }
        else if(GM.score >= 200)
        {
            nextStageCHip = Random.Range(0, stageChips.Length - 1);
        }
        else
        {
            nextStageCHip = Random.Range(0, stageChips.Length - 2);
        }

        GameObject stageObject = (GameObject)Instantiate(
            stageChips[nextStageCHip],
            new Vector3(0, 0, chipIndex * StageChipSize),
            Quaternion.identity
            );

        return stageObject;
    }

    // 一番古いステージを削除
    private void DestroyoldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
