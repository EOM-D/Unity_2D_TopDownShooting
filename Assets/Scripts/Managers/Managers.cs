using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    static Managers Instance{ get { Init(); return _instance; } }

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers"};
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();
        }
    }
}
