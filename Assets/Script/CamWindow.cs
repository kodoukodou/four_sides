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

	// �S�J�������i�[���锠
	private Camera[] cameras;
	private Camera[] cameras4;
	List<Camera> cameras2;
	List<Camera> cameras3;
	// �J�����p�̃e�[�u��
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

	void OnEnable()
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
		N = new List<int>();
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

		if (GUILayout.Button("CameraLoad"))
		{
			current = Camera.allCameras.Length;
			Main.enabled = false;
			count = Camera.allCameras.Length;
			rt = new RenderTexture[count];
			//tex = new Texture[count];

			// �V�[���Ȃ��̑S�ẴJ�����̐���z��̒����ɂ���
			//List<Camera> cameras = new List<Camera>();
			cameras = new Camera[count];
			names = new String[count];
			// �V�[���Ȃ��̑S�ẴJ�������擾(enable=true�̂�)
			Camera.GetAllCameras(cameras);
			// �e�[�u���̏�����
			//table = new Dictionary<int, Camera>();

			// �e�[�u���ɑS�ẴJ������o�^
			for (int i = 0; i < count; i++)
			{
				// �J�����̖��O���擾
				names[i] = cameras[i].gameObject.name;
				// �e�[�u���ɓo�^
				//table.Add(i, cameras[i]);
			}

			for (int i = 0; i < count; i++)
			{
				rt[i] = new RenderTexture(540, 540, 0);
				rt[i].Create();
				//rt[i].name = string.Format("{0}_rt", place[i]);
				cameras[i].targetTexture = rt[i];
			}
			cameras4 = cameras;
		}
		Main.enabled = true;

		if (GUILayout.Button("Reset"))
		{
			//cameras2=new List<Camera> ();
			//cameras2 = new List(cameras) ;
			//���ʂ�����R���N�V����
			//.NET Framework 2.0�ȍ~�Ȃ�΁AList<int>���g���������ǂ�
			List<int> resultList =new List<int>();

			cameras4 = cameras4.Where(value => value != null).ToArray();

			foreach (var i in cameras4)
			{
				//Debug.Log(i);
			}

			for (int i = 0; i < cameras.Length; i++)
            {
				//ary2�Ɋ܂܂�Ă��Ȃ����m�F����
				if (Array.IndexOf(cameras4, cameras[i]) < 0)
				{
					//�܂܂�Ă��Ȃ���΁A���X�g�ɉ�����
					resultList.Add(i);
				}
			}

			//���ʂ�z��ɕϊ�����
			int[] resultArray = resultList.ToArray(); ;

			foreach (var i in resultArray)
			{
				Debug.Log(i);
			}

			int k = 0;

			for (int i = 0; i < resultArray.Length; i++)
			{
				rt[resultArray[i]-k].Release();
				rt = rt.Where(value => value != rt[resultArray[i] - k]).ToArray();
				k++;
			}

			cameras = cameras.Where(value => value != null).ToArray();

			Array.Clear(resultArray, 0, resultArray.Length);

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