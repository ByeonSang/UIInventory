using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISlot : MonoBehaviour
{
    // Color Set
    private Color dontAcitveColor = Color.gray;
    private Color AcitveColor = Color.white;

    public ItemData itemData;
    public bool IsActive;

    private Image img;

    // Children Image data
    private Image itemIcon;
    private Color alphaZero = Color.white * 0f;
    private Color alphaOrigin = Color.white;

    public void ActiveSlot(bool active)
    {
        IsActive = active;

        if(img)
        {
            if (active)
                img.color = AcitveColor;
            else
                img.color = dontAcitveColor;
        }
        
    }

    private void Awake()
    {
        // slot
        img = GetComponent<Image>();

        // itemIcon
        itemIcon = transform.GetChild(0).GetComponent<Image>();

        ActiveSlot(IsActive);
        itemIcon.color = alphaZero;
    }

    public void IconDraw()
    {
        itemIcon.color = alphaOrigin;
        itemIcon.sprite = itemData.Image;
    }


}
