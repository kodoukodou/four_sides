using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Linq;

public class CamWindow : EditorWindow
{
	Camera Main;
	RenderTexture ren;
	int count=0;
	Texture[] tex;
	RenderTexture[] rt;

	// 全カメラを格納する箱
	private Camera[] cameras;
	// カメラ用のテーブル
	//public Dictionary<int, Camera> table;

	String[] names;

	Vector2 scrollPosition;
	[MenuItem("MyMenu/Create/CamWindow")]
	public static void Init()
	{
		CamWindow.GetWindow<CamWindow>(false, "CamWindow");
	}

	void Awake()
	{
		Main = Camera.main;
		Main.enabled = true;
	}

	private void Update()
	{
		//Repaint();
	}
	void OnGUI()
	{
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

		if (GUILayout.Button("CameraLoad"))
		{
			Main.enabled = false;
			count = Camera.allCameras.Length;
			rt = new RenderTexture[count];
			tex = new Texture[count];

			// シーンないの全てのカメラの数を配列の長さにする
			cameras = new Camera[count];
			names = new String[count];
			// シーンないの全てのカメラを取得(enable=trueのみ)
			Camera.GetAllCameras(cameras);
			// テーブルの初期化
			//table = new Dictionary<int, Camera>();
			// テーブルに全てのカメラを登録
			for (int i = 0; i < count; i++)
			{
				// カメラの名前を取得
				names[i] = cameras[i].gameObject.name;
				// テーブルに登録
				//table.Add(i, cameras[i]);
			}

			for (int i = 0; i < count; i++)
			{
				rt[i] = new RenderTexture(540, 540, 0);
				rt[i].Create();
				//rt[i].name = string.Format("{0}_rt", place[i]);
				cameras[i].targetTexture = rt[i];
			}
		}
		Main.enabled = true;
		
		if (GUILayout.Button("Reset"))
        {
			if (count != Camera.allCameras.Length)
			{
				count = Camera.allCameras.Length;

				Debug.Log(count);

				for (int i = 0; i < count; i++)
				{

					if (cameras[i] == null)
					{
						rt[i].Release();
						rt = rt.Where(value => value != rt[i]).ToArray();
						tex = tex.Where(value => value != tex[i]).ToArray();
					}
				}
				cameras = cameras.Where(value => value != null).ToArray();
			}
		}

		EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				EditorGUIUtility.SetIconSize(Vector2.one * 100);

				for (int i = 0; i < rt.Length; i++)
				{
					tex[i] = (Texture)rt[i];
					EditorGUIUtility.SetIconSize(Vector2.one * 100);

				//EditorGUILayout.LabelField(string.Format("{0}", names[i]));
				EditorGUILayout.LabelField("Plane");
				if (GUILayout.Button((Texture)rt[i], GUILayout.Width(100), GUILayout.Height(100)))
					{
						Camera.main.transform.position = cameras[i].transform.position;
						Camera.main.transform.rotation = cameras[i].transform.rotation;
					}
				}
			}
			EditorGUILayout.EndVertical();

			EditorGUIUtility.SetIconSize(Vector2.zero);

			GUILayout.EndScrollView();
	}
}