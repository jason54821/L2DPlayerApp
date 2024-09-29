using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;

public class TransitionDurationModifier : MonoBehaviour
{
    [MenuItem("Tools/Set Transition Duration to 0.1")]
    static void SetTransitionDuration()
    {
        // 選択されているAnimatorControllerを取得
        AnimatorController animatorController = Selection.activeObject as AnimatorController;

        if (animatorController == null)
        {
            Debug.LogError("AnimatorControllerを選択してください");
            return;
        }

        // 各レイヤーをループ
        foreach (var layer in animatorController.layers)
        {
            // レイヤー内のすべてのステートをループ
            foreach (var state in layer.stateMachine.states)
            {
                // ステートのトランジションを取得
                foreach (var transition in state.state.transitions)
                {
                    transition.hasFixedDuration = false; // Fixed Durationをオフに設定
                    transition.duration = 0.1f; // Transition Durationを0.1（10%）に設定
                    Debug.Log($"Transition Durationを0.1に設定: {state.state.name} -> {transition.destinationState.name}");
                }
            }
        }
    }
}
