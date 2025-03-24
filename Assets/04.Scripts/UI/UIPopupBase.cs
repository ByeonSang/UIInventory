using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupBase : MonoBehaviour
{
    protected bool isEnable = false;

    public void SwitchOn()
    {
        isEnable = !isEnable;
        gameObject.SetActive(isEnable);

        if(isEnable)
        {
            
        }
    }

    public void SetActive(bool trigger)
    {
        gameObject.SetActive(trigger);
    }
}
