using UnityEditor;
using UnityEngine;

public class EditorSelectDependenciesHotkey
{
    [MenuItem("Tools/Select Dependencies %g")]
    private static void SelectDependencies()
    {
        Object[] selectedObjects = Selection.objects;
        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected");
            return;
        }
        
        Object[] dependencies = EditorUtility.CollectDependencies(selectedObjects);
        Selection.objects = dependencies;
    }
}
