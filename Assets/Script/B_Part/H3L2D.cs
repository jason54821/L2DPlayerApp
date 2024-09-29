using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Demo_BのLive2Dアニメーション制御・音声再生クラス
/// </summary>
public class H3L2D : MonoBehaviour
{
    private Animator baseAnimator;
    private AudioSource audioSource;

    [SerializeField]
    private H3SoundManager soundManager; // サウンドマネージャー

    [SerializeField]
    GaugeManager gaugeManager; // ゲージUIの制御マネージャー

    [SerializeField]
    private bool notPlayingB = false; // アニメーションBが再生中かどうか

    [SerializeField]
    private bool notPlayingC = false; // アニメーションCが再生中かどうか

    // 再生状態の初期化
    private bool playingIdle = false;
    private bool playingLoop1 = false;
    private bool playingLoop2 = false;
    private bool playingShoot = false;

    public bool shootLock = false; // ゲージが満タンになるまでロックする

    void Start()
    {
        baseAnimator = GetComponent<Animator>();
        audioSource = soundManager.GetComponent<AudioSource>();

        soundManager.Play("idle_dia"); // 初期状態でidle_diaを再生
        PlayIdle(); // Idle状態に設定
    }

    void Update()
    {
        // ゲージ管理
        HandleGaugeManager();

        // アニメーションの状態に基づいて処理を制御
        HandleAnimationStates();
    }

    // ゲージの管理ロジック
    private void HandleGaugeManager()
    {
        if (!gaugeManager.shootTrigger)
        {
            int sequence = baseAnimator.GetInteger("Sequence");
            if (sequence == 0 || sequence == 3)
            {
                gaugeManager.barTrigger = 0;
            }
            else if (sequence == 1)
            {
                gaugeManager.barTrigger = 1;
                notPlayingC = false;
            }
            else if (sequence == 2)
            {
                gaugeManager.barTrigger = 2;
                notPlayingC = false;
            }
        }
    }

    // アニメーション状態の管理
    private void HandleAnimationStates()
    {
        if (!notPlayingB && IsCurrentAnimation("B"))
        {
            baseAnimator.SetInteger("Sequence", 3);
            shootLock = true;
            notPlayingB = true;
        }

        if (!notPlayingC && IsCurrentAnimation("C"))
        {
            gaugeManager.ResetMask(true);
            gaugeManager.shootTrigger = false;
            shootLock = false;
            notPlayingC = true;
        }
    }

    // 現在のアニメーションが指定された名前かどうかを判定
    private bool IsCurrentAnimation(string animationName)
    {
        return GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash(animationName));
    }

    // アニメーション再生時の共通処理
    private void PlayAnimation(string animationName, ref bool playingFlag, int sequence, string sound)
    {
        if (baseAnimator.GetInteger("Sequence") != sequence && !shootLock)
        {
            soundManager.Stop();
            soundManager.Play(sound);
            Debug.Log(sound);
        }

        ResetStatus();
        playingFlag = true;

        if (!shootLock)
        {
            notPlayingB = false;
            notPlayingC = false;
            baseAnimator.SetInteger("Sequence", sequence);
        }

        StartCoroutine(CheckAudioPlaying(playingFlag, audioSource, () =>
        {
            Debug.Log($"{animationName}_vo");
            soundManager.Play($"{animationName}_vo");
        }));
    }

    // Idleアニメーションの再生
    public void PlayIdle()
    {
        PlayAnimation("idle", ref playingIdle, 0, "idle_dia");
    }

    // Loop1アニメーションの再生
    public void PlayLoop1()
    {
        PlayAnimation("loop1", ref playingLoop1, 1, "loop1_dia");
    }

    // Loop2アニメーションの再生
    public void PlayLoop2()
    {
        PlayAnimation("loop2", ref playingLoop2, 2, "loop2_dia");
    }

    // Shootアニメーションの再生
    public void PlayShoot()
    {
        if (!shootLock && gaugeManager.shootTrigger)
        {
            soundManager.Stop();
            soundManager.Play("shoot_dia");
            Debug.Log("shoot_dia");

            ResetStatus();
            playingShoot = true;
            baseAnimator.SetTrigger("Shoot");
            baseAnimator.SetInteger("Sequence", 0);

            StartCoroutine(CheckAudioPlaying(playingShoot, audioSource, () =>
            {
                Debug.Log("shoot_vo");
                soundManager.Play("shoot_vo");
            }));
        }
    }

    // ステータスのリセット
    void ResetStatus()
    {
        playingIdle = false;
        playingLoop1 = false;
        playingLoop2 = false;
        playingShoot = false;
    }

    // オーディオ再生の状態チェック
    private IEnumerator CheckAudioPlaying(bool playingCondition, AudioSource audio, UnityAction callback)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (!playingCondition || !audio.isPlaying)
            {
                callback();
                break;
            }
        }
    }
}

