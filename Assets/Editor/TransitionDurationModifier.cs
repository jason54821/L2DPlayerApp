using UnityEditor;
using UnityEngine;
using UnityEditor.Animations;

public class TransitionDurationModifier : MonoBehaviour
{
    [MenuItem("Tools/Set Transition Duration to 0.1")]
    static void SetTransitionDuration()
    {
        // �I������Ă���AnimatorController���擾
        AnimatorController animatorController = Selection.activeObject as AnimatorController;

        if (animatorController == null)
        {
            Debug.LogError("AnimatorController��I�����Ă�������");
            return;
        }

        // �e���C���[�����[�v
        foreach (var layer in animatorController.layers)
        {
            // ���C���[���̂��ׂẴX�e�[�g�����[�v
            foreach (var state in layer.stateMachine.states)
            {
                // �X�e�[�g�̃g�����W�V�������擾
                foreach (var transition in state.state.transitions)
                {
                    transition.hasFixedDuration = false; // Fixed Duration���I�t�ɐݒ�
                    transition.duration = 0.1f; // Transition Duration��0.1�i10%�j�ɐݒ�
                    Debug.Log($"Transition Duration��0.1�ɐݒ�: {state.state.name} -> {transition.destinationState.name}");
                }
            }
        }
    }
}
