using UnityEngine;
using System;

public class RobotModel
{
    public event Action ModelChanged;
    public event Action<PartType, Color> ColorChanged;

    public RobotPartState Legs { get; }
    public RobotPartState Torso { get; }
    public RobotPartState Head { get; }

    public RobotModel(
        RobotPartState legs,
        RobotPartState torso,
        RobotPartState head)
    {
        Legs = legs;
        Torso = torso;
        Head = head;

        Subscribe(Legs, PartType.Legs);
        Subscribe(Torso, PartType.Torso);
        Subscribe(Head, PartType.Head);
    }

    private void Subscribe(
        RobotPartState state,
        PartType type)
    {
        state.PartChanged += () =>
        {
            ModelChanged?.Invoke();
        };

        state.ColorChanged += () =>
        {
            ColorChanged?.Invoke(type, state.Color);
        };
    }
}