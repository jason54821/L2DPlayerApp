using UnityEngine;
using Live2D.Cubism.Framework.Raycasting;
using UnityEngine.SceneManagement;

/// <summary>
/// Demo_AのLive2Dアニメーション制御・音声再生・衝突判定クラス
/// </summary>
public class L2DManager : MonoBehaviour
{
    private Animator l2d_Animator;  // Live2Dキャラクターのアニメーター
    private AudioSource audioSource; // サウンド再生用のオーディオソース

    public int sequenceID = 0; // 現在のシーケンスID

    [SerializeField]
    private L2DSound l2dSound; // Live2Dサウンド管理クラス

    // カーソル関連
    public Texture2D touch_mouse;
    public CursorMode cursorMode = CursorMode.Auto; 
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        // コンポーネントの取得
        l2d_Animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        l2dSound = GetComponent<L2DSound>();
    }

    void Update()
    {
        // マウスの位置に対するRaycastを行い、キャラクターがクリックされたかを検出
        HandleMouseInteraction();
    }

    // マウスでキャラクターとのインタラクションを管理
    private void HandleMouseInteraction()
    {
        var raycaster = GetComponent<CubismRaycaster>();
        var results = new CubismRaycastHit[4]; // ヒットしたオブジェクトを格納
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // マウス位置からRayを発射
        var hitCount = raycaster.Raycast(ray, results); // Raycastを実行

        if (hitCount > 0) // キャラクターにヒットした場合
        {
            Cursor.SetCursor(touch_mouse, hotSpot, cursorMode); // カーソルを変更
            if (Input.GetMouseButtonDown(0)) // 左クリックが押されたら
            {
                HandleSequence(); // シーケンスの処理を行う
            }
        }
        else
        {
            // キャラクターにヒットしていない場合はカーソルをデフォルトに戻す
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    // シーケンスIDに基づいて処理を管理
    private void HandleSequence()
    {
        switch (sequenceID)
        {
            case 0:
                ExecuteSequence("PlaySequence2", SetTouch, 1);
                break;
            case 1:
                ExecuteSequence("PlaySequence3", SetChange, 2);
                break;
            case 2:
                ExecuteSequence("PlaySequence4", SetTouch, 3);
                break;
            case 3:
                Cursor.SetCursor(null, Vector2.zero, cursorMode); // カーソルをデフォルトに戻す
                SceneManager.LoadScene("Demo_B"); // シーンを切り替え
                break;
        }
    }

    // シーケンスの実行処理を共通化
    private void ExecuteSequence(string soundMethod, System.Action animationAction, int nextSequenceID)
    {
        // サウンド再生とアニメーションのトリガーを設定
        typeof(L2DSound).GetMethod(soundMethod).Invoke(l2dSound, null);
        animationAction();
        sequenceID = nextSequenceID; // 次のシーケンスIDに移行
    }

    public void SetTouch()
    {
        l2d_Animator.SetTrigger("Touch"); // アニメーターの"Touch"トリガーを起動
    }

    public void SetChange()
    {
        l2d_Animator.SetTrigger("Change"); // アニメーターの"Change"トリガーを起動
    }
}

