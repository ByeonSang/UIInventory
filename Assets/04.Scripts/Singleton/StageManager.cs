using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{

    


    public void ChanageScene(int index)
    {
        // ������Ʈ Ǯ, �˾�, ���丮 ��ųʸ��� Ŭ����
        // TODO::

        GameManager.Instance.PlayerController = null;
        ObjectPoolManager.Instance.ClearObjectPool();
        FactoryManager.Instance.ClearPath();

        SceneManager.LoadScene(index);
    }
}
