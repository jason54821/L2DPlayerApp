using UnityEngine;
using Live2D.Cubism.Framework.Raycasting;
using UnityEngine.SceneManagement;

/// <summary>
/// Demo_A��Live2D�A�j���[�V��������E�����Đ��E�Փ˔���N���X
/// </summary>
public class L2DManager : MonoBehaviour
{
    private Animator l2d_Animator;  // Live2D�L�����N�^�[�̃A�j���[�^�[
    private AudioSource audioSource; // �T�E���h�Đ��p�̃I�[�f�B�I�\�[�X

    public int sequenceID = 0; // ���݂̃V�[�P���XID

    [SerializeField]
    private L2DSound l2dSound; // Live2D�T�E���h�Ǘ��N���X

    // �J�[�\���֘A
    public Texture2D touch_mouse;
    public CursorMode cursorMode = CursorMode.Auto; 
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        // �R���|�[�l���g�̎擾
        l2d_Animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        l2dSound = GetComponent<L2DSound>();
    }

    void Update()
    {
        // �}�E�X�̈ʒu�ɑ΂���Raycast���s���A�L�����N�^�[���N���b�N���ꂽ�������o
        HandleMouseInteraction();
    }

    // �}�E�X�ŃL�����N�^�[�Ƃ̃C���^���N�V�������Ǘ�
    private void HandleMouseInteraction()
    {
        var raycaster = GetComponent<CubismRaycaster>();
        var results = new CubismRaycastHit[4]; // �q�b�g�����I�u�W�F�N�g���i�[
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // �}�E�X�ʒu����Ray�𔭎�
        var hitCount = raycaster.Raycast(ray, results); // Raycast�����s

        if (hitCount > 0) // �L�����N�^�[�Ƀq�b�g�����ꍇ
        {
            Cursor.SetCursor(touch_mouse, hotSpot, cursorMode); // �J�[�\����ύX
            if (Input.GetMouseButtonDown(0)) // ���N���b�N�������ꂽ��
            {
                HandleSequence(); // �V�[�P���X�̏������s��
            }
        }
        else
        {
            // �L�����N�^�[�Ƀq�b�g���Ă��Ȃ��ꍇ�̓J�[�\�����f�t�H���g�ɖ߂�
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    // �V�[�P���XID�Ɋ�Â��ď������Ǘ�
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
                Cursor.SetCursor(null, Vector2.zero, cursorMode); // �J�[�\�����f�t�H���g�ɖ߂�
                SceneManager.LoadScene("Demo_B"); // �V�[����؂�ւ�
                break;
        }
    }

    // �V�[�P���X�̎��s���������ʉ�
    private void ExecuteSequence(string soundMethod, System.Action animationAction, int nextSequenceID)
    {
        // �T�E���h�Đ��ƃA�j���[�V�����̃g���K�[��ݒ�
        typeof(L2DSound).GetMethod(soundMethod).Invoke(l2dSound, null);
        animationAction();
        sequenceID = nextSequenceID; // ���̃V�[�P���XID�Ɉڍs
    }

    public void SetTouch()
    {
        l2d_Animator.SetTrigger("Touch"); // �A�j���[�^�[��"Touch"�g���K�[���N��
    }

    public void SetChange()
    {
        l2d_Animator.SetTrigger("Change"); // �A�j���[�^�[��"Change"�g���K�[���N��
    }
}

