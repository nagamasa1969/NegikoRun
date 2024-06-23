using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;

    // 追従ターゲットプロパティ
    public GameObject target;
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // 追従距離の計算
        diff = target.transform.position - transform.position;
    }

    // Update関数の後に呼び出される。カメラの追従に便利
    void LateUpdate()
    {
        // Vector3.Leap(a, b, t)a = 始点 b = 終点, t = aとbの間の割合
        transform.position = Vector3.Lerp(
            transform.position,
            target.transform.position - diff,
            Time.deltaTime * followSpeed
            );
    }
}
