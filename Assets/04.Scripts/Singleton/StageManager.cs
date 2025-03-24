using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{

    


    public void ChanageScene(int index)
    {
        // ¿ÀºêÁ§Æ® Ç®, ÆË¾÷, ÆÑÅä¸® µñ¼Å³Ê¸®µé Å¬¸®¾î
        // TODO::

        GameManager.Instance.PlayerController = null;
        ObjectPoolManager.Instance.ClearObjectPool();
        FactoryManager.Instance.ClearPath();

        SceneManager.LoadScene(index);
    }
}
