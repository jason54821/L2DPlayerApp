using UnityEngine;

public class UIFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;  // UIのフェード用
    public SpriteRenderer[] sprites; // Spriteのフェード用
    public float fadeDuration = 1.0f;  // フェードの長さ
    public float fadeOutTargetAlpha = 0.2f;  // フェードアウト時の最終透明度
    private float currentRemainTime;
    private bool isFading = false;   // フェードが動作中かどうか
    private bool isFadingOut = false;  // フェードアウト中かどうか

    private void Start()
    {
        // 初期状態では透明度は設定しない
        currentRemainTime = fadeDuration;  // デフォルトは完全に表示された状態
        UpdateAlpha();  // 初期のアルファ値を設定
    }

    private void Update()
    {
        // フェードが動作中でない場合は処理を行わない
        if (!isFading) return;

        // フェードの進行
        if (isFadingOut)
        {
            currentRemainTime -= Time.deltaTime;
        }
        else
        {
            currentRemainTime += Time.deltaTime;
        }

        currentRemainTime = Mathf.Clamp(currentRemainTime, 0, fadeDuration);

        // アルファ値を更新
        UpdateAlpha();

        // フェードが終了したら停止
        if (currentRemainTime == 0 || currentRemainTime == fadeDuration)
        {
            isFading = false;
        }
    }

    // アルファ値をUIに適用する
    private void UpdateAlpha()
    {
        // フェードイン時は1（完全表示）、フェードアウト時はfadeOutTargetAlpha（指定された透明度）
        float alpha = isFadingOut
            ? Mathf.Lerp(1, fadeOutTargetAlpha, 1 - currentRemainTime / fadeDuration)
            : Mathf.Lerp(fadeOutTargetAlpha, 1, currentRemainTime / fadeDuration);

        // CanvasGroupのアルファ値を更新
        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
        }

        // SpriteRendererのアルファ値を更新
        foreach (var sprite in sprites)
        {
            var color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }
    }

    // フェードアウト開始
    public void StartFadeOut(float targetAlpha = -1)
    {
        if (targetAlpha >= 0)
        {
            fadeOutTargetAlpha = targetAlpha;  // 指定されたゴール透明度をセット
        }
        isFadingOut = true;
        isFading = true;
    }

    // フェードイン開始
    public void StartFadeIn()
    {
        isFadingOut = false;
        isFading = true;
    }

}
