using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputFader : MonoBehaviour
{
    public UIFader fader;  // フェードスクリプトを参照
    public float inactiveTime = 3.0f;  // マウス操作がない時間
    [SerializeField]
    private float stepTime;
    private bool isUIActive = true;

    void Start()
    {
        stepTime = 0.0f;
    }

    void Update()
    {
        // マウスの動きがあるか確認 (X軸とY軸の両方の動きをチェック)
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            // マウスが動いていれば経過時間をリセット
            stepTime = 0.0f;

            if (!isUIActive)
            {
                // UIが非表示ならフェードイン
                fader.StartFadeIn();
                isUIActive = true;  // UIがアクティブ状態になる
            }
        }
        else
        {
            // マウスが動いていない場合、経過時間をカウント
            stepTime += Time.deltaTime;

            // 一定時間経過でフェードアウト
            if (stepTime >= inactiveTime && isUIActive)
            {
                fader.StartFadeOut();
                isUIActive = false;  // UIが非アクティブ状態になる
            }
        }
    }

}
