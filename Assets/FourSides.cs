using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

public class FourSides : EditorWindow
{
    private GameObject avater;
    private Camera cam;
    private Camera view;
    private Camera[] camera = new Camera[4];
    private Camera front;
    private Camera back;
    private Camera right;
    private Camera left;
    private GameObject[] pl = new GameObject[4];
    private GameObject plane;
    private GameObject front_plane;
    private GameObject back_plane;
    private GameObject right_plane;
    private GameObject left_plane;

    private Material[] material=new Material[4];
    private String[] place = new String[4];

    private RenderTexture[] rt=new RenderTexture[4];
    private RenderTexture front_rt;
    private RenderTexture back_rt;
    private RenderTexture right_rt;
    private RenderTexture left_rt;

    private Vector3 center = new Vector3(5, 0, 0);

    private float height = 2.5f;
    private float cam_size = 2.5f;
    private int render_size=1080;
    private int current_render_size ;
    private bool under = false;


    [MenuItem("MyMenu/Create/FourSides")]
    static void init()
    {
        EditorWindow.GetWindow<FourSides>("FourSides");
    }

    void Awake()
    {
        place[0] = "back";
        place[1] = "right";
        place[2] = "front";
        place[3] = "left";

        camera[0] = back;
        camera[1] = right;
        camera[2] = front;
        camera[3] = left;

        rt[0] = back_rt;
        rt[1] = right_rt;
        rt[2] = front_rt;
        rt[3] = left_rt;

        pl[0] = back_plane;
        pl[1] = right_plane;
        pl[2] = front_plane;
        pl[3] = left_plane;
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            avater = EditorGUILayout.ObjectField("avater", avater, typeof(GameObject), true) as GameObject;
            plane = EditorGUILayout.ObjectField("plane", plane, typeof(GameObject), true) as GameObject;
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.LabelField("Chara_Camera");

        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            height = EditorGUILayout.FloatField("*height", height);
            cam_size = EditorGUILayout.FloatField("*cam_size", cam_size);
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.LabelField("Plane");
        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            center = EditorGUILayout.Vector3Field("*center", center);
            render_size = EditorGUILayout.IntField("*render_size", render_size);
        }
        EditorGUILayout.EndVertical();
        under = EditorGUILayout.Toggle("*under", under);

        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        {
            if (GUILayout.Button("set"))
            {
                current_render_size = render_size;

                //RenderTexture
                for (int i=0;i<4;i++)
                {
                    rt[i]= new RenderTexture(render_size, render_size, 0);
                    rt[i].Create();
                    rt[i].name = string.Format("{0}_rt", place[i]);
                }

                //Camera�����Ɛݒ�
                GameObject obj = UnityEditor.EditorUtility.CreateGameObjectWithHideFlags("Camera", HideFlags.None, typeof(Camera));
                cam = obj.GetComponent<Camera>();
                cam.orthographic = true;
                cam.farClipPlane = 5;
                cam.orthographicSize = cam_size;

                //�e�q�ݒ�
                var parent = new GameObject("CharaCamera").transform;

                /*camera[0] = cam;
                camera[0].targetTexture = rt[0];
                camera[0].transform.parent = parent.transform;
                camera[0].name = string.Format("{0}_cam", place[0]);*/

                //Main�J����
                view = Camera.main;
                view.depth = 10;
                view.farClipPlane = 10;
                view.transform.position = center + new Vector3(0, (float)2.62, 0);
                view.transform.rotation = Quaternion.Euler(90, 180, 180);
                view.orthographicSize = 1.51f;

                //�V�����}�e���A�����쐬
                Material front_mat = new Material(Shader.Find("Diffuse"));
                Material back_mat = new Material(Shader.Find("Diffuse"));
                Material right_mat = new Material(Shader.Find("Diffuse"));
                Material left_mat = new Material(Shader.Find("Diffuse"));

                material[0] = front_mat;
                material[1] = right_mat;
                material[2] = back_mat;
                material[3] = left_mat;

                //Plane�̐����Ɛݒ�
                plane.transform.position = center;

                for (int i = 0; i < 4; i++)
                {
                    //�J����
                    camera[i] = Instantiate(cam);
                    camera[i].name = string.Format("{0}_cam", place[i]);
                    camera[i].targetTexture = rt[i];
                    camera[i].transform.parent = parent.transform;
                    camera[i].backgroundColor = Color.black;
                    //��
                    pl[i]= Instantiate(plane);
                    pl[i].name = string.Format("{0}_plane", place[i]);
                    pl[i].transform.rotation = Quaternion.Euler(0, i*90, 0);
                    material[i].SetTexture("_MainTex", rt[i]);
                    pl[i].GetComponent<MeshRenderer>().material = material[i];
                }

                DestroyImmediate(obj);
                Under();

                //�e�q�ݒ�
                var parent2 = new GameObject("Plane").transform;
                for (int i = 0; i < 4; i++)
                {
                    pl[i].transform.parent = parent2.transform;
                }

                //�e�q�ݒ�
                var parent3 = new GameObject("FourSides").transform;
                parent.transform.parent = parent3.transform;
                parent2.transform.parent = parent3.transform;
            }

            //�u������
            if (GUILayout.Button("*replace"))
            {
                Under();

                for (int i = 0; i < 4; i++)
                {
                    pl[i].transform.position = center;
                    camera[i].orthographicSize = cam_size;
                }

                if (current_render_size != render_size)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        rt[i].Release();
                        rt[i] = new RenderTexture(render_size, render_size, 0);
                        rt[i].Create();
                        rt[i].name = string.Format("{0}_rt", place[i]);
                        camera[i].targetTexture = rt[i];
                        material[i].SetTexture("_MainTex", rt[i]);
                    }
                    current_render_size = render_size;
                }
                view.transform.position = center + new Vector3(0, (float)2.62, 0);
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    void Under()
    {
        //�ォ����
        if (!under)
        {
            for (int i = 0; i < 4; i++)
            {
                camera[i].transform.rotation = Quaternion.Euler(0, i * 90, 0);
            }
            camera[0].transform.position = avater.transform.position + new Vector3(0, height, -2);
            camera[1].transform.position = avater.transform.position + new Vector3(-2, height, 0);
            camera[2].transform.position = avater.transform.position + new Vector3(0, height, 2);
            camera[3].transform.position = avater.transform.position + new Vector3(2, height, 0);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                camera[i].transform.rotation = Quaternion.Euler(0, i * 90, 180);
            }
            camera[0].transform.position = avater.transform.position + new Vector3(0, height - 3.3f, -2);
            camera[1].transform.position = avater.transform.position + new Vector3(-2, height - 3.3f, 0);
            camera[2].transform.position = avater.transform.position + new Vector3(0, height - 3.3f, 2);
            camera[3].transform.position = avater.transform.position + new Vector3(2, height - 3.3f, 0);
        }
    }
    private float FloatField(string v, float distance)
    {
        throw new NotImplementedException();
    }
}