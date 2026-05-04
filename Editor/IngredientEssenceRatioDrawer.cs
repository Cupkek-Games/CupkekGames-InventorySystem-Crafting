#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CupkekGames.InventorySystem.Crafting.Editor
{
    /// <summary>
    /// Inspector for an <see cref="IngredientEssenceRatio"/>. Shows one slider per stored entry,
    /// labeled by essence key. Designers add/remove entries via the underlying list — this drawer
    /// just presents them.
    /// </summary>
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
                    SerializedProperty entry = valuesArray.GetArrayElementAtIndex(i);
                    SerializedProperty keyProp = entry.FindPropertyRelative("EssenceKey");
                    SerializedProperty valueProp = entry.FindPropertyRelative("Value");

                    string label2 = string.IsNullOrEmpty(keyProp.stringValue) ? $"Slot {i}" : keyProp.stringValue;
                    valueProp.floatValue = EditorGUI.Slider(
                        new Rect(position.x, position.y, position.width, lineHeight),
                        label2,
                        valueProp.floatValue,
                        0f,
                        1f
                    );
                    total += valueProp.floatValue;
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
            {
                SerializedProperty entry = valuesArray.GetArrayElementAtIndex(i);
                total += entry.FindPropertyRelative("Value").floatValue;
            }

            if (!Mathf.Approximately(total, 1f))
                totalHeight += lineHeight * 2 + spacing;

            return totalHeight + lineHeight + spacing; // Foldout height
        }
    }
}
#endif
