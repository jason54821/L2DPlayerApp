using UnityEngine;

public class UIFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;  // UI�̃t�F�[�h�p
    public SpriteRenderer[] sprites; // Sprite�̃t�F�[�h�p
    public float fadeDuration = 1.0f;  // �t�F�[�h�̒���
    public float fadeOutTargetAlpha = 0.2f;  // �t�F�[�h�A�E�g���̍ŏI�����x
    private float currentRemainTime;
    private bool isFading = false;   // �t�F�[�h�����쒆���ǂ���
    private bool isFadingOut = false;  // �t�F�[�h�A�E�g�����ǂ���

    private void Start()
    {
        // ������Ԃł͓����x�͐ݒ肵�Ȃ�
        currentRemainTime = fadeDuration;  // �f�t�H���g�͊��S�ɕ\�����ꂽ���
        UpdateAlpha();  // �����̃A���t�@�l��ݒ�
    }

    private void Update()
    {
        // �t�F�[�h�����쒆�łȂ��ꍇ�͏������s��Ȃ�
        if (!isFading) return;

        // �t�F�[�h�̐i�s
        if (isFadingOut)
        {
            currentRemainTime -= Time.deltaTime;
        }
        else
        {
            currentRemainTime += Time.deltaTime;
        }

        currentRemainTime = Mathf.Clamp(currentRemainTime, 0, fadeDuration);

        // �A���t�@�l���X�V
        UpdateAlpha();

        // �t�F�[�h���I���������~
        if (currentRemainTime == 0 || currentRemainTime == fadeDuration)
        {
            isFading = false;
        }
    }

    // �A���t�@�l��UI�ɓK�p����
    private void UpdateAlpha()
    {
        // �t�F�[�h�C������1�i���S�\���j�A�t�F�[�h�A�E�g����fadeOutTargetAlpha�i�w�肳�ꂽ�����x�j
        float alpha = isFadingOut
            ? Mathf.Lerp(1, fadeOutTargetAlpha, 1 - currentRemainTime / fadeDuration)
            : Mathf.Lerp(fadeOutTargetAlpha, 1, currentRemainTime / fadeDuration);

        // CanvasGroup�̃A���t�@�l���X�V
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
        }

        // SpriteRenderer�̃A���t�@�l���X�V
        foreach (var sprite in sprites)
        {
            var color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }
    }

    // �t�F�[�h�A�E�g�J�n
    public void StartFadeOut(float targetAlpha = -1)
    {
        if (targetAlpha >= 0)
        {
            fadeOutTargetAlpha = targetAlpha;  // �w�肳�ꂽ�S�[�������x���Z�b�g
        }
        isFadingOut = true;
        isFading = true;
    }

    // �t�F�[�h�C���J�n
    public void StartFadeIn()
    {
        isFadingOut = false;
        isFading = true;
    }

}
