#if UNITY_EDITOR
using CupkekGames.InventorySystem.Crafting;
using UnityEditor;
using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting.Editor
{
    [CustomPropertyDrawer(typeof(IngredientEssenceRatio))]
    public class IngredientEssenceRatioDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            property.isExpanded = EditorGUI.Foldout(
                new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                property.isExpanded,
                label
            );

            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                SerializedProperty valuesArray = property.FindPropertyRelative("_values");
                float lineHeight = EditorGUIUtility.singleLineHeight;
                float spacing = EditorGUIUtility.standardVerticalSpacing;

                float total = 0f;
                for (int i = 0; i < valuesArray.arraySize; i++)
                {
                    position.y += lineHeight + spacing;
                    SerializedProperty element = valuesArray.GetArrayElementAtIndex(i);
                    element.floatValue = EditorGUI.Slider(
                        new Rect(position.x, position.y, position.width, lineHeight),
                        $"Slot {i}",
                        element.floatValue,
                        0f,
                        1f
                    );
                    total += element.floatValue;
                }

                position.y += lineHeight + spacing;
                if (!Mathf.Approximately(total, 1f))
                {
                    EditorGUI.HelpBox(
                        new Rect(position.x, position.y, position.width, lineHeight * 2),
                        $"The sum of all values is {total:F2}, which does not equal 1.",
                        MessageType.Warning
                    );
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
                return EditorGUIUtility.singleLineHeight;

            SerializedProperty valuesArray = property.FindPropertyRelative("_values");
            float lineHeight = EditorGUIUtility.singleLineHeight;
            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float totalHeight = valuesArray.arraySize * (lineHeight + spacing);

            float total = 0f;
            for (int i = 0; i < valuesArray.arraySize; i++)
                total += valuesArray.GetArrayElementAtIndex(i).floatValue;

            if (!Mathf.Approximately(total, 1f))
                totalHeight += lineHeight * 2 + spacing;

            return totalHeight + lineHeight + spacing; // Foldout height
        }
    }
}
#endif
