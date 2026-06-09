using UnityEngine;
using UnityEngine.UI;

public class StatisticUI : MonoBehaviour
{
    [SerializeField] private Text weightText;
    [SerializeField] private Text powerText;

    private RobotModel model;

    public void Initialize(RobotModel robotModel)
    {
        if (model != null)
        {
            model.ModelChanged -= UpdateStats;
        }

        model = robotModel;

        model.ModelChanged += UpdateStats;

        UpdateStats();
    }

    private void OnDestroy()
    {
        if (model != null)
        {
            model.ModelChanged -= UpdateStats;
        }
    }

    private void UpdateStats()
    {
        float totalWeight =
            model.Head.Part.Weight +
            model.Torso.Part.Weight +
            model.Legs.Part.Weight;

        float totalPower =
            model.Head.Part.Power +
            model.Torso.Part.Power +
            model.Legs.Part.Power;

        weightText.text = $"Weight: {totalWeight:F1}";
        powerText.text = $"Power: {totalPower:F1}";
    }
}
