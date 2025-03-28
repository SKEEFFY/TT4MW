using UnityEditor;
using UnityEngine;

public class EditorFindDependenciesHotkey
{
    [MenuItem("MyTools/Select Dependencies %g")]
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
