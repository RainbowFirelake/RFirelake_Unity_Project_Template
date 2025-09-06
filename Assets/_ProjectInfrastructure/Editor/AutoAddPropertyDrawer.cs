using System.Reflection;
using RFirelake.Infrastructure.Attributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RFirelake.Editor
{
    [CustomPropertyDrawer(typeof(AutoAddAttribute))]
    public class AutoAddPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var visualElement = new VisualElement();
            visualElement.Add(new PropertyField(property));

            if (property.propertyType != SerializedPropertyType.ObjectReference)
                return visualElement;
            
            var type = property.serializedObject.targetObject.GetType();
            
            var field = type.GetField(
                property.name, 
                BindingFlags.Public | 
                BindingFlags.NonPublic | 
                BindingFlags.Instance);
            
            if (field == null)
                return visualElement;
            
            var fieldType = field.FieldType;
            var component = property.serializedObject.targetObject as Component;

            if (component == null)
                return visualElement;

            if (component.gameObject.GetComponent(fieldType) != null) 
                return visualElement;
            
            var addedComponent = component.gameObject.AddComponent(fieldType);
            property.objectReferenceValue = addedComponent;
            property.serializedObject.ApplyModifiedProperties();

            return visualElement;
        }
    }
}