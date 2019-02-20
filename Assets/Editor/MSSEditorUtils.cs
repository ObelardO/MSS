using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System;
using Obel.MSS;

namespace Obel.MSS.Editor
{
    public static class MSSEditorUtils
    {
        public static T GetObjectBySerializedProperty<T>(FieldInfo fieldInfo, SerializedProperty property) where T : class
        {
            var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
            if (obj == null) { return null; }

            T actualObject = null;
            if (obj.GetType().IsArray)
            {
                var index = Convert.ToInt32(new string(property.propertyPath.Where(c => char.IsDigit(c)).ToArray()));
                actualObject = ((T[])obj)[index];
            }
            else
            {
                actualObject = obj as T;
            }
            return actualObject;
        }
    }
}
