using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    // Destroy ���� Ȯ�ο�
    private static bool _ShuttingDown = false;
    private static T _Instance;
    public static T Instance
    {
        get
        {
            // ���� ���� �� Object ���� �̱����� OnDestroy �� ���� ���� �� ���� �ִ�. 
            // �ش� �̱����� gameObject.Ondestory() ������ ������� �ʰų� ����Ѵٸ� null üũ�� ������
            if (_ShuttingDown)
            {
                Debug.Log("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                return null;
            }

            if (_Instance == null)
            {
                // �ν��Ͻ� ���� ���� Ȯ��
                _Instance = (T)FindObjectOfType(typeof(T));

                // ���� �������� �ʾҴٸ� �ν��Ͻ� ����
                if (_Instance == null)
                {
                    // ���ο� ���ӿ�����Ʈ�� ���� �̱��� Attach
                    var singletonObject = new GameObject();
                    _Instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _Instance;

        }
    }

    protected virtual void Awake()
    {
        if (transform.root != null || transform.parent != null)
        {
            DontDestroyOnLoad(transform.root);
        }
    }

    private void OnApplicationQuit()
    {
        _ShuttingDown = true;
    }
    private void OnDestroy()
    {
        _ShuttingDown = true;
    }
}
