using System;
using UnityEngine;

[Serializable]
public class RobotPartState
{
    public event Action ColorChanged;
    public event Action PartChanged;

    private RobotPart part;
    private Color color;

    public RobotPart Part
    {
        get => part;
        set
        {
            if (part == value)
                return;

            part = value;
            PartChanged?.Invoke();
        }
    }

    public Color Color
    {
        get => color;
        set
        {
            if (color == value)
                return;

            color = value;
            ColorChanged?.Invoke();
        }
    }

    public RobotPartState(RobotPart part, Color color)
    {
        this.part = part;
        this.color = color;
    }
}