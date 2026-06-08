using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{   
    private Scrollbar scrollbar;
    [SerializeField] private Image previewImage;
    [SerializeField] private PartType partType;
    public System.Action<PartType,Color> OnColorChanged;

    private void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.onValueChanged.AddListener(OnScrollChanged);
        OnScrollChanged(scrollbar.value);
    }

    private void OnDestroy()
    {
        scrollbar.onValueChanged.RemoveListener(OnScrollChanged);
    }

    private void OnScrollChanged(float value)
    {
        Color color = Color.HSVToRGB(value, 1f, 1f);

        previewImage.color = color;

        OnColorChanged?.Invoke(partType,color);
    }
}
