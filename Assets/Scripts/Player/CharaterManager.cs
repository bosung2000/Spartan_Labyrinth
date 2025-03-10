using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterManager : MonoBehaviour
{
    private static CharaterManager _instance;
    public static CharaterManager Instance
    {
        get
        {
            if (_instance == null)
            {
               // _instance = new CharaterManager(); //이방식도 좋은데 이건 gameobject가 존재해야만해 
                _instance = new GameObject("CharaterManger").AddComponent<CharaterManager>();
            }
            return _instance;
        }
    }

    public Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if (_instance ==null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance ==this)
            {
                Destroy(gameObject); // ?삭제시킬필요가 있나 ? 그냥 패스하면디는거 아닌가?
            }
        }
    }
}
