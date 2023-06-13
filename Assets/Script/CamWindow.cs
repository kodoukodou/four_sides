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
	RenderTexture[] rt;

	// ëSÉJÉÅÉâÇäiî[Ç∑ÇÈî†
	private Camera[] cameras;
	private Camera[] cameras4;

	String[] names;

	Vector2 scrollPosition;

	int size = 200;
	int column = 3;
	int col;

	List<Camera> camList = new List<Camera>();
	List<RenderTexture> rtList ;
	List<int> N;
	bool[] check;
	bool button=false;
	string[] nam;

	[MenuItem("MyMenu/Create/CamWindow")]
	public static void Init()
	{
		CamWindow.GetWindow<CamWindow>(false, "CamWindow");
	}

	void OnEnable()
	{
		Main = Camera.main;
		Main.enabled = true;
		System.GC.Collect();
		Resources.UnloadUnusedAssets();
	}

	private void Update()
	{
		//Repaint();
	}
	void OnGUI()
	{
		N = new List<int>();
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

		using (new EditorGUILayout.VerticalScope(GUI.skin.box, GUILayout.Width(100), GUILayout.Height(100)))
        {
			GUILayout.Space(10);

			using (new EditorGUILayout.HorizontalScope())
			{
				if (GUILayout.Button("CameraLoad", GUILayout.Width(150), GUILayout.Height(50)))
				{
					current = Camera.allCameras.Length;
					Main.enabled = false;
					count = Camera.allCameras.Length;
					rt = new RenderTexture[count];
					check = new bool[count];
					nam = new string[count];

					cameras = new Camera[count];
					names = new String[count];

					Camera.GetAllCameras(cameras);

					for (int i = 0; i < count; i++)
					{
						names[i] = cameras[i].gameObject.name;
					}

					for (int i = 0; i < count; i++)
					{
						rt[i] = new RenderTexture(540, 540, 0);
						rt[i].Create();
						cameras[i].targetTexture = rt[i];
					}
					cameras4 = cameras;
					button = true;
				}
				Main.enabled = true;

				if (GUILayout.Button("Reset", GUILayout.Width(150), GUILayout.Height(50)))
				{
					List<int> resultList = new List<int>();

					cameras4 = cameras4.Where(value => value != null).ToArray();

					for (int i = 0; i < cameras.Length; i++)
					{
						if (Array.IndexOf(cameras4, cameras[i]) < 0)
						{
							resultList.Add(i);
						}
					}

					//åãâ ÇîzóÒÇ…ïœä∑Ç∑ÇÈ
					int[] resultArray = resultList.ToArray(); ;

					int k = 0;

					for (int i = 0; i < resultArray.Length; i++)
					{
						rt[resultArray[i] - k].Release();
						rt = rt.Where(value => value != rt[resultArray[i] - k]).ToArray();
						check = check.Where(value => value != true).ToArray();
						names = names.Where(value => value != names[resultArray[i] - k]).ToArray();
						k++;
					}

					cameras = cameras.Where(value => value != null).ToArray();

					Array.Clear(resultArray, 0, resultArray.Length);
				}
				GUILayout.FlexibleSpace();

				if (GUILayout.Button("Delete", GUILayout.Width(150), GUILayout.Height(50)))
                {
					int k = 0;
					for (int i=0;i<rt.Length;i++)
                    {
                        if (check[i])
                        {
							Debug.Log(i);
							rt[i].Release();
							rt = rt.Where(value => value != rt[i-k]).ToArray();
							names= names.Where(value => value != names[i-k]).ToArray();
							k++;
						}
                    }
					check = check.Where(value => value != true).ToArray();
				}
			}
            if (rt!=null)
            {
				using (new EditorGUILayout.VerticalScope())
				{
					GUILayout.Space(10);
					using (new EditorGUILayout.HorizontalScope())
					{
						size = EditorGUILayout.IntField("size", size);
						GUILayout.FlexibleSpace();
					}

					using (new EditorGUILayout.HorizontalScope())
					{
						column = EditorGUILayout.IntSlider("column", column, 1, rt.Length);
						GUILayout.FlexibleSpace();
					}
					GUILayout.Space(10);
				}
			}

		}
		GUILayout.Space(20);

		if (button)
		{
			col = column;
			int tate = rt.Length / col;
			int amari = rt.Length % col;
			int k = 0;

            if (col>rt.Length)
            {
				col = rt.Length;
            }
			using (new EditorGUILayout.HorizontalScope())
            {
				using (new EditorGUILayout.VerticalScope())
				{
					for (int i = 0; i < tate; i++)
					{
						using (new EditorGUILayout.HorizontalScope())
						{
							EditorGUIUtility.SetIconSize(Vector2.one * size);
							for (int j = 0; j < col; j++)
							{
								using (new EditorGUILayout.VerticalScope(GUI.skin.box))
								{
									EditorGUIUtility.SetIconSize(Vector2.one * size);

									using (new EditorGUILayout.HorizontalScope())
                                    {
										Debug.Log(rt.Length);
										Debug.Log(check.Length);
										Debug.Log(k);
										check[k] = EditorGUILayout.Toggle(check[k], GUILayout.Width(10));
										GUILayout.Space(10);
										EditorGUILayout.LabelField(string.Format("{0}", names[k]), GUILayout.Width(70));
									}
									if (GUILayout.Button((Texture)rt[k], GUILayout.Width(size), GUILayout.Height(size)))
									{
										Camera.main.transform.position = cameras[k].transform.position;
										Camera.main.transform.rotation = cameras[k].transform.rotation;
									}
								}
									k++;
							}
						}
					}
                    if (amari!=0)
                    {
						Debug.Log("amari");
						using (new EditorGUILayout.HorizontalScope())
						{
							EditorGUIUtility.SetIconSize(Vector2.one * size);
							for (int j = 0; j < amari; j++)
							{
								using (new EditorGUILayout.VerticalScope(GUI.skin.box))
								{
									EditorGUIUtility.SetIconSize(Vector2.one * size);

									using (new EditorGUILayout.HorizontalScope())
									{
										Debug.Log(check.Length);
										Debug.Log(k);
										check[k] = EditorGUILayout.Toggle(check[k], GUILayout.Width(10));
										GUILayout.Space(10);
										EditorGUILayout.LabelField(string.Format("{0}", names[k]), GUILayout.Width(70));
									}
									if (GUILayout.Button((Texture)rt[k], GUILayout.Width(size), GUILayout.Height(size)))
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
				}
			}
		}
		GUILayout.EndScrollView();
	}
}