using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class FileIconTest2 : EditorWindow
{
	Camera front_cam;
	Camera right_cam;
	Camera back_cam;
	Camera left_cam;

	Vector2 scrollPosition;
	[MenuItem("Window/File Icon2")]
	public static void Init()
	{
		FileIconTest2.GetWindow<FileIconTest2>(false, "FileIcon");
	}

	private void Update()
	{
		Repaint();
	}
	void OnGUI()
	{
		front_cam = EditorGUILayout.ObjectField("front_cam", front_cam, typeof(Camera), true) as Camera;
		right_cam = EditorGUILayout.ObjectField("right_cam", right_cam, typeof(Camera), true) as Camera;
		back_cam = EditorGUILayout.ObjectField("back_cam", back_cam, typeof(Camera), true) as Camera;
		left_cam = EditorGUILayout.ObjectField("left_cam", left_cam, typeof(Camera), true) as Camera;

		scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

		Texture front= (Texture)AssetDatabase.LoadAssetAtPath("Assets/front.renderTexture", typeof(Texture));
		EditorGUILayout.BeginHorizontal(GUI.skin.box);
		{
			EditorGUIUtility.SetIconSize(Vector2.one * 100);
			if (GUILayout.Button(front, GUILayout.Width(100), GUILayout.Height(100))) 
			{
				Camera.main.transform.position = front_cam.transform.position;
				Camera.main.transform.rotation = front_cam.transform.rotation;
			}

			Texture right = (Texture)AssetDatabase.LoadAssetAtPath("Assets/right.renderTexture", typeof(Texture));
			EditorGUIUtility.SetIconSize(Vector2.one * 100);
			if (GUILayout.Button(right, GUILayout.Width(100), GUILayout.Height(100)))
			{
				Camera.main.transform.position = right_cam.transform.position;
				Camera.main.transform.rotation = right_cam.transform.rotation;
			}

			Texture back = (Texture)AssetDatabase.LoadAssetAtPath("Assets/back.renderTexture", typeof(Texture));
			EditorGUIUtility.SetIconSize(Vector2.one * 100);
			if (GUILayout.Button(back, GUILayout.Width(100), GUILayout.Height(100))) 
			{
				Camera.main.transform.position = back_cam.transform.position;
				Camera.main.transform.rotation = back_cam.transform.rotation;
			}

			Texture left = (Texture)AssetDatabase.LoadAssetAtPath("Assets/left.renderTexture", typeof(Texture));
			EditorGUIUtility.SetIconSize(Vector2.one * 100);
			if (GUILayout.Button(left, GUILayout.Width(100), GUILayout.Height(100))) 
			{
				Camera.main.transform.position = left_cam.transform.position;
				Camera.main.transform.rotation = left_cam.transform.rotation;
			}
		}
        EditorGUILayout.EndHorizontal();

		EditorGUIUtility.SetIconSize(Vector2.zero);

		GUILayout.EndScrollView();
	}
}