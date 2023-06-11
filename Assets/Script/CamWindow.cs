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
	int count = 0;
	int current;
	//Texture[] tex;
	RenderTexture[] rt;

	// 全カメラを格納する箱
	private Camera[] cameras;
	// カメラ用のテーブル
	//public Dictionary<int, Camera> table;

	String[] names;

	Vector2 scrollPosition;

	int size = 100;
	//int tate = 1;
	int yoko = 3;

	List<Camera> camList = new List<Camera>();
	List<RenderTexture> rtList ;
	//List<Texture> texList;
	List<int> N;

	[MenuItem("MyMenu/Create/CamWindow")]
	public static void Init()
	{
		CamWindow.GetWindow<CamWindow>(false, "CamWindow");
	}

	void Awake()
	{
		Main = Camera.main;
		Main.enabled = true;
		N = new List<int>();
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
			current = Camera.allCameras.Length;
			Main.enabled = false;
			count = Camera.allCameras.Length;
			rt = new RenderTexture[count];
			//tex = new Texture[count];

			// シーンないの全てのカメラの数を配列の長さにする
			//List<Camera> cameras = new List<Camera>();
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
			foreach (Camera camItem in cameras)
			{
				Debug.Log(camItem);
			}

			for (int i = 0; i < Camera.allCameras.Length ; i++)
			{
				//Debug.Log(cameras[i]);
				if (cameras[i] == null)
				{
					Debug.Log("OK");
					N.Add(i);
				}
			}
			//Debug.Log(N.Count);
			foreach (int nItem in N)
			{
				//Debug.Log(nItem);
			}
			for (int i = 0; i < N.Count; i++)
			{
				rt[N[i]].Release();
				rt = rt.Where(value => value != rt[N[i]]).ToArray();
			}
			cameras = cameras.Where(value => value != null).ToArray();
			N.Clear();

			for (int i = 0; i < count; i++)
			{
				cameras[i].targetTexture = rt[i];
			}

			/*N = new List<int>();

			/*foreach (Camera camItem in cameras)
			{
				Debug.Log(camItem);
			}
			foreach (RenderTexture rtItem in rt)
			{
				Debug.Log(rtItem);
			}//
			Debug.Log(Camera.allCameras.Length);
			Debug.Log(current);

			if (current != Camera.allCameras.Length)
			{
			//current = Camera.allCameras.Length-1;

			/*foreach (Camera camItem in cameras)
			{
				Debug.Log(camItem);
			}//

			for (int i = 0; i < Camera.allCameras.Length - 1; i++)
			{
				if (cameras[i] == null)
				{
					N.Add(i);
					//rt[i].Release();
					//rt = rt.Where(value => value != rt[i]).ToArray();

					//tex = tex.Where(value => value != tex[i]).ToArray();
				}
			}

			foreach (int nItem in N)
			{
				//Debug.Log(nItem);
			}

			for (int i = 0; i < N.Count; i++)
			{
				rt[N[i]].Release();
				rt = rt.Where(value => value != rt[N[i]]).ToArray();
			}

			//rt = rt.Where(value => value != null).ToArray();
			cameras = cameras.Where(value => value != null).ToArray();
			}*/
		}
		using (new EditorGUILayout.VerticalScope())
		{
			size = EditorGUILayout.IntField("size", size);
		}

		if (rt != null)
		{
			int tate = rt.Length / yoko;
			int amari = rt.Length % yoko;
			int k = 0;

			using (new EditorGUILayout.VerticalScope())
			{
				for (int i = 0; i < tate; i++)
				{
					using (new EditorGUILayout.HorizontalScope())
					{
						EditorGUIUtility.SetIconSize(Vector2.one * size);
						for (int j = 0; j < yoko; j++)
						{
							using (new EditorGUILayout.VerticalScope())
							{
								EditorGUIUtility.SetIconSize(Vector2.one * size);

								//EditorGUILayout.LabelField(string.Format("{0}", names[i]));
								EditorGUILayout.LabelField("Plane");
								if (GUILayout.Button((Texture)rt[k], GUILayout.Width(size), GUILayout.Height(size)))
								//if (GUILayout.Button((Texture)rt[k], GUILayout.Width(100), GUILayout.Height(100)))
								{
									Camera.main.transform.position = cameras[k].transform.position;
									Camera.main.transform.rotation = cameras[k].transform.rotation;
								}
							}
							k++;
						}

					}
				}
				using (new EditorGUILayout.HorizontalScope())
				{
					EditorGUIUtility.SetIconSize(Vector2.one * size);
					for (int j = 0; j < amari; j++)
					{
						using (new EditorGUILayout.VerticalScope())
						{
							EditorGUIUtility.SetIconSize(Vector2.one * size);

							//EditorGUILayout.LabelField(string.Format("{0}", names[i]));
							EditorGUILayout.LabelField("Plane");
							if (GUILayout.Button((Texture)rt[k], GUILayout.Width(size), GUILayout.Height(size)))
							//if (GUILayout.Button((Texture)rt[k], GUILayout.Width(100), GUILayout.Height(100)))
							{
								Camera.main.transform.position = cameras[k].transform.position;
								Camera.main.transform.rotation = cameras[k].transform.rotation;
							}
						}
						k++;
					}

				}
				EditorGUIUtility.SetIconSize(Vector2.zero);
			}
			GUILayout.EndScrollView();
		}
	}
}