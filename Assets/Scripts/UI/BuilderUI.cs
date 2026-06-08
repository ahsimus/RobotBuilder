using UnityEngine;

public class BuilderUI : MonoBehaviour
{
    [SerializeField] private PartButton[] partButtons;
    [SerializeField] private ColorPicker[] coloPicker;

    public event System.Action<PartType, int> PartSelected;
    public event System.Action<PartType, Color> ColorSelected;

    private void Awake()
    {
        foreach (var button in partButtons)
        {
            button.Clicked += OnPartClicked;
        }

        foreach (var button in coloPicker)
        {
            button.OnColorChanged += OnColorClicked;
        }
    }

    private void OnDestroy()
    {
        foreach (var button in partButtons)
        {
            button.Clicked -= OnPartClicked;
        }

        foreach (var button in coloPicker)
        {
            button.OnColorChanged -= OnColorClicked;
        }
    }

    private void OnPartClicked(PartType type, int index)
    {
        PartSelected?.Invoke(type, index);
    }

    private void OnColorClicked(PartType type, Color color)
    {
        Debug.Log($"BuilderUI: {type}");
        ColorSelected?.Invoke(type, color);
    }
}