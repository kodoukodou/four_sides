using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class CamWindow2 : EditorWindow
{
	Camera Main;
	Camera front_cam;
	Camera right_cam;
	Camera back_cam;
	Camera left_cam;
	RenderTexture ren;
	int count=0;
	// 全カメラを格納する箱
	private Camera[] cameras;
	// カメラ用のテーブル
	public Dictionary<int, Camera> table;

	String[] names;

	Vector2 scrollPosition;
	//[MenuItem("MyMenu/Create/CamWindow")]
	public static void Init()
	{
		CamWindow.GetWindow<CamWindow>(false, "CamWindow");
	}

	void Awake()
	{
		count = Camera.allCameras.Length;
		//public RenderTexture[] rt = new RenderTexture[count];
	}

	private void Update()
	{
		//Repaint();
	}
	void OnGUI()
	{
		front_cam = EditorGUILayout.ObjectField("front_cam", front_cam, typeof(Camera), true) as Camera;
		right_cam = EditorGUILayout.ObjectField("right_cam", right_cam, typeof(Camera), true) as Camera;
		back_cam = EditorGUILayout.ObjectField("back_cam", back_cam, typeof(Camera), true) as Camera;
		left_cam = EditorGUILayout.ObjectField("left_cam", left_cam, typeof(Camera), true) as Camera;

		//ren = EditorGUILayout.ObjectField("ren", ren, typeof(RenderTexture), true) as RenderTexture;

		RenderTexture[] rt = new RenderTexture[count];
		Texture[] tex = new Texture[count];
		//Camera[] cam= new Camera[count];

		scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

		if (GUILayout.Button("CameraLoad"))
		{
			Main = Camera.main;
			Main.enabled = false;
			//count = Camera.allCameras.Length;

			// シーンないの全てのカメラの数を配列の長さにする
			cameras = new Camera[count];
			//names = new String[count];
			// シーンないの全てのカメラを取得(enable=trueのみ)
			Camera.GetAllCameras(cameras);
			// テーブルの初期化
			table = new Dictionary<int, Camera>();
			// テーブルに全てのカメラを登録
			for (int i = 0; i < count; i++)
			{
				// カメラの名前を取得
				//names[i] = cameras[i].gameObject.name;
				// テーブルに登録
				table.Add(i, cameras[i]);
			}

			for (int i = 0; i < count; i++)
			{
				rt[i] = new RenderTexture(540, 540, 0);
				rt[i].Create();
				//rt[i].name = string.Format("{0}_rt", place[i]);
			}
			for (int i = 0; i < count; i++)
			{
				table[i].targetTexture = rt[i];
			}
			Main.enabled = true;
		}

		//Texture front= (Texture)ren;

		//if (count != 0)
		//{
			EditorGUILayout.BeginHorizontal(GUI.skin.box);
			{
				EditorGUIUtility.SetIconSize(Vector2.one * 100);

				for (int i = 0; i < 4; i++)
				{
					tex[i] = (Texture)rt[i];
					EditorGUIUtility.SetIconSize(Vector2.one * 100);

					if (GUILayout.Button((Texture)rt[i], GUILayout.Width(100), GUILayout.Height(100)))
					{
						Camera.main.transform.position = table[i].transform.position;
						Camera.main.transform.rotation = table[i].transform.rotation;
					}
				}

				/*Texture right = (Texture)AssetDatabase.LoadAssetAtPath("Assets/right.renderTexture", typeof(Texture));
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
				}*/
			}
			EditorGUILayout.EndHorizontal();

			EditorGUIUtility.SetIconSize(Vector2.zero);

			GUILayout.EndScrollView();
		//}
	}
}