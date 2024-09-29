using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Demo_B��Live2D�A�j���[�V��������E�����Đ��N���X
/// </summary>
public class H3L2D : MonoBehaviour
{
    private Animator baseAnimator;
    private AudioSource audioSource;

    [SerializeField]
    private H3SoundManager soundManager; // �T�E���h�}�l�[�W���[

    [SerializeField]
    GaugeManager gaugeManager; // �Q�[�WUI�̐���}�l�[�W���[

    [SerializeField]
    private bool notPlayingB = false; // �A�j���[�V����B���Đ������ǂ���

    [SerializeField]
    private bool notPlayingC = false; // �A�j���[�V����C���Đ������ǂ���

    // �Đ���Ԃ̏�����
    private bool playingIdle = false;
    private bool playingLoop1 = false;
    private bool playingLoop2 = false;
    private bool playingShoot = false;

    public bool shootLock = false; // �Q�[�W�����^���ɂȂ�܂Ń��b�N����

    void Start()
    {
        baseAnimator = GetComponent<Animator>();
        audioSource = soundManager.GetComponent<AudioSource>();

        soundManager.Play("idle_dia"); // ������Ԃ�idle_dia���Đ�
        PlayIdle(); // Idle��Ԃɐݒ�
    }

    void Update()
    {
        // �Q�[�W�Ǘ�
        HandleGaugeManager();

        // �A�j���[�V�����̏�ԂɊ�Â��ď����𐧌�
        HandleAnimationStates();
    }

    // �Q�[�W�̊Ǘ����W�b�N
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

    // �A�j���[�V������Ԃ̊Ǘ�
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

    // ���݂̃A�j���[�V�������w�肳�ꂽ���O���ǂ����𔻒�
    private bool IsCurrentAnimation(string animationName)
    {
        return GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash(animationName));
    }

    // �A�j���[�V�����Đ����̋��ʏ���
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

    // Idle�A�j���[�V�����̍Đ�
    public void PlayIdle()
    {
        PlayAnimation("idle", ref playingIdle, 0, "idle_dia");
    }

    // Loop1�A�j���[�V�����̍Đ�
    public void PlayLoop1()
    {
        PlayAnimation("loop1", ref playingLoop1, 1, "loop1_dia");
    }

    // Loop2�A�j���[�V�����̍Đ�
    public void PlayLoop2()
    {
        PlayAnimation("loop2", ref playingLoop2, 2, "loop2_dia");
    }

    // Shoot�A�j���[�V�����̍Đ�
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

    // �X�e�[�^�X�̃��Z�b�g
    void ResetStatus()
    {
        playingIdle = false;
        playingLoop1 = false;
        playingLoop2 = false;
        playingShoot = false;
    }

    // �I�[�f�B�I�Đ��̏�ԃ`�F�b�N
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

