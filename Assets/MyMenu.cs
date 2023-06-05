using UnityEditor;
using UnityEngine;

public class MyMenu : MonoBehaviour
{
    private static Camera _cam;

    //  Add a menu item named "Create/Camera" to McGUI in the menu bar.
    [MenuItem("MyMenu/Create/Camera")]
    static void CreateCamera()
    {
        GameObject obj = UnityEditor.EditorUtility.CreateGameObjectWithHideFlags("Camera", HideFlags.None, typeof(Camera));
        _cam = obj.GetComponent<Camera>();
    }
}