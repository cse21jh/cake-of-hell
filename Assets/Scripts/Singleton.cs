using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // ����� �Ǵ� ���� ������� 

    protected static T instance = null;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if(instance == null)
                {
                    GameObject singletonObject = new GameObject($"{typeof(T)} (Singleton)");
                    instance = singletonObject.AddComponent<T>();
                    DontDestroyOnLoad(instance);
                }
            }
            return instance;
        }
    }

}
