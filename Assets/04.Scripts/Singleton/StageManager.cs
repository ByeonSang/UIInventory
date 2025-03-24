using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{

    public void ChanageScene(int index)
    {
        // 오브젝트 풀, 팝업, 팩토리 딕셔너리들 클리어
        // TODO::

        GameManager.Instance.PlayerController = null;
        ObjectPoolManager.Instance.ClearObjectPool();
        FactoryManager.Instance.ClearPath();

        SceneManager.LoadScene(index);
    }
}
