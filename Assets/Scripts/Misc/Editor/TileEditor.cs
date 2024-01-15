using System.Diagnostics;
using System.Drawing.Printing;
using UnityEditor;
using static Tile;

[CanEditMultipleObjects]
[CustomEditor(typeof(Tile))]
public class TileEditor : Editor
{
    SerializedProperty SerializedTileTypeProperty;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SerializedTileTypeProperty = serializedObject.FindProperty("_tileType");
        Debug.WriteLine("TileEditor OnEnable");

        serializedObject.Update();

        Tile tile = (Tile)target;


        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(SerializedTileTypeProperty);
        if (EditorGUI.EndChangeCheck())
        {
            // Call the setter of your property here
            tile._TileType = (TileType)SerializedTileTypeProperty.enumValueIndex;

        }





        serializedObject.ApplyModifiedProperties();


    }
}
