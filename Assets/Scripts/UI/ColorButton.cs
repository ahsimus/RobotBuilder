using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
   [SerializeField] private List<GameObject> pickers;

    public void Show(int index)
    {
        for (int i = 0; i < pickers.Count; i++)
        {
            pickers[i].SetActive(i == index);
        }
    }

    public void HideAll()
    {
        foreach (var picker in pickers)
        {
            picker.SetActive(false);
        }
    }
}