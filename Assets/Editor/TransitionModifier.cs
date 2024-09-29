using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;

public class TransitionModifier : MonoBehaviour
{
    [MenuItem("Tools/Disable Fixed Duration in Transitions")]
    static void DisableFixedDuration()
    {
        AnimatorController animatorController = Selection.activeObject as AnimatorController;

        if (animatorController == null)
        {
            Debug.LogError("AnimatorControllerを選択してください");
            return;
        }

        foreach (var layer in animatorController.layers)
        {
            foreach (var state in layer.stateMachine.states)
            {
                foreach (var transition in state.state.transitions)
                {
                    transition.hasFixedDuration = false; // Fixed Durationをオフに設定
                    Debug.Log($"Fixed Durationオフ: {state.state.name} -> {transition.destinationState.name}");
                }
            }
        }
    }
}
