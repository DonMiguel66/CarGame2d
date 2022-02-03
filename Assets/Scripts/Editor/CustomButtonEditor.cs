using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CarGame2D
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : ButtonEditor
    {
        private SerializedProperty m_InteractableProperty;

        protected override void OnEnable()
        {
            m_InteractableProperty = serializedObject.FindProperty("m_Interactable");
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var changeButtonType = new PropertyField(serializedObject.FindProperty(CustomButton.ChangeButtonType));
            var duration = new PropertyField(serializedObject.FindProperty(CustomButton.Duration));
            var pauseTime = new PropertyField(serializedObject.FindProperty(CustomButton.PauseTime));
            var punchDuration = new PropertyField(serializedObject.FindProperty(CustomButton.PunchDuration));
            var punchVibrato = new PropertyField(serializedObject.FindProperty(CustomButton.PunchVibrato));
            var punchElasticity = new PropertyField(serializedObject.FindProperty(CustomButton.PunchElasticity));
            var punchScale = new PropertyField(serializedObject.FindProperty(CustomButton.PunchScale));
            var strength = new PropertyField(serializedObject.FindProperty(CustomButton.Strength));

            var tweenLable = new Label("Tweens");
            var shakeLable = new Label("Shake Strength");
            var bounceLable = new Label("Bounce Settings");
            root.Add(tweenLable);
            root.Add(changeButtonType);
            root.Add(duration);
            root.Add(pauseTime);
            root.Add(bounceLable);
            root.Add(punchDuration);
            root.Add(punchVibrato);
            root.Add(punchElasticity);
            root.Add(punchScale);
            root.Add(shakeLable);
            root.Add(strength);
            root.Add(new IMGUIContainer(OnInspectorGUI));

            return root;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_InteractableProperty);
            EditorGUI.BeginChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
