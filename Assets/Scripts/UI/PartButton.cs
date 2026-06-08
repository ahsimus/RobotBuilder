using System;
using UnityEngine;
using UnityEngine.UI;

public class PartButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private PartType partType;
    [SerializeField] private int index;

    public event Action<PartType, int> Clicked;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Clicked?.Invoke(partType, index);
    }
}