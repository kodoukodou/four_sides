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

	// �S�J�������i�[���锠
	private Camera[] cameras;
	// �J�����p�̃e�[�u��
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

			// �V�[���Ȃ��̑S�ẴJ�����̐���z��̒����ɂ���
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