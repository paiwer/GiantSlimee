using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TagSelector))]
public class Fall2 : Editor
{
    public override void OnInspectorGUI()
    {
        TagSelector tagSelector = (TagSelector)target;

        // Get all the tags defined in the project
        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

        // Find the index of the currently selected tag
        int selectedIndex = Mathf.Max(0, System.Array.IndexOf(tags, tagSelector.selectedTag));

        // Display a dropdown list for selecting tags
        selectedIndex = EditorGUILayout.Popup("Select Tag", selectedIndex, tags);

        // Update the selected tag based on the dropdown selection
        tagSelector.selectedTag = tags[selectedIndex];
    }
}
