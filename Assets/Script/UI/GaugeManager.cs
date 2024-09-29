using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    ////H3_Animation Base;
    //Transform mask;
    //Vector3 pos;

    //private float barSpeed = 0.7f;

    //public int barTrigger = 0;

    //public bool shootTrigger = false;

    //public Button shootButton;
    //public GameObject shootobj;

    //// Use this for initialization
    //void Awake()
    //{
    //    shootobj = GameObject.Find("Cum");
    //    shootButton = shootobj.GetComponent<Button>();

    //    //GameObject obj = GameObject.Find("H3"); //Playerっていうオブジェクトを探す
    //    //Base = obj.GetComponent<H3_Animation>(); //付いているスクリプトを取得
    //}

    //void Update()
    //{
    //    mask = this.transform;
    //    pos = mask.localPosition;

    //    if (pos.y <= 15f)
    //    {
    //        if (barTrigger == 1)
    //        {
    //            pos.y += barSpeed * Time.deltaTime;
    //            mask.localPosition = pos;
    //        }
    //        if (barTrigger == 2)
    //        {
    //            pos.y += barSpeed * 2 * Time.deltaTime;
    //            mask.localPosition = pos;
    //        }

    //    }

    //    CheckStatus();
    //    // if (Input.GetKey (KeyCode.LeftArrow)){
    //    //     moveMask();
    //    // }
    //    // if (Input.GetKey (KeyCode.RightArrow)){
    //    //     moveMaskF();
    //    // }



    //}

    //public void MoveMask(){
    //    pos = mask.localPosition;
    //    pos.y += 0.015f; 
    //    mask.localPosition = pos;
    //}

    //public void MoveMaskF(){
    //    pos = mask.localPosition;
    //    pos.y += 0.03f; 
    //    mask.localPosition = pos;
    //}

    //void CheckStatus(){
    //    pos = mask.localPosition;
    //    if(pos.y > 14.3f){
    //        shootTrigger = true;
    //        shootButton.interactable = true;
    //    }else{
    //        shootButton.interactable = false;
    //    }
    //}

    //public void ResetMask(){
    //    mask.localPosition = new Vector3(0, 0,0);
    //}

    [SerializeField] public Transform mask;
    public float barSpeed = 0.7f; // 通常の速度
    public float fastSpeedMultiplier = 2f; // 高速時の倍率
    public float maxYPosition = 14.3f; // ゴールとなるY座標
    public float startYPosition = 0f; // 開始時のY座標

    public int barTrigger = 0;
    public bool shootTrigger = false;

    [SerializeField] public Button shootButton;
    //public GameObject shootobj;

    void Awake()
    {
        //shootobj = GameObject.Find("Cum");
        //shootButton = shootobj.GetComponent<Button>();
        //mask = this.transform;
    }

    void Update()
    {
        // barTriggerが0の時はゲージが増えない
        if (barTrigger == 0)
        {
            return;  // ゲージ増加処理をスキップ
        }

        // マスクの移動処理
        if (mask.localPosition.y <= maxYPosition)
        {
            float speed = barSpeed * Time.deltaTime;
            if (barTrigger == 2)
            {
                speed *= fastSpeedMultiplier; // 高速移動
            }

            mask.localPosition = new Vector3(mask.localPosition.x, mask.localPosition.y + speed, mask.localPosition.z);
        }

        // ステータスチェック
        CheckStatus();
    }

    // スムーズな移動をイージングで実現（オプション）
    public void MoveMaskSmooth(float targetY, float duration)
    {
        StartCoroutine(SmoothMove(targetY, duration));
    }

    IEnumerator SmoothMove(float targetY, float duration)
    {
        float startY = mask.localPosition.y;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / duration);
            mask.localPosition = new Vector3(mask.localPosition.x, newY, mask.localPosition.z);
            yield return null;
        }

        mask.localPosition = new Vector3(mask.localPosition.x, targetY, mask.localPosition.z);
    }

    void CheckStatus()
    {
        if (mask.localPosition.y >= maxYPosition)
        {
            shootTrigger = true;
            shootButton.interactable = true;
        }
        else
        {
            shootButton.interactable = false;
        }
    }

    public void ResetMask(bool animate = false)
    {
        if (animate)
        {
            // アニメーションでリセット
            MoveMaskSmooth(startYPosition, 0.5f);
        }
        else
        {
            // 即座にリセット
            mask.localPosition = new Vector3(mask.localPosition.x, startYPosition, mask.localPosition.z);
        }
    }
}
