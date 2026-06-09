using System;
using UnityEngine;

public class RobotBuilderController : MonoBehaviour
{
    [SerializeField] private DB_RobotParts db;
    [SerializeField] private RobotModelView modelView;
    [SerializeField] private StatisticUI statUi;
    [SerializeField] private BuilderUI ui;

    private RobotModel model;

    private void Awake()
    {
        CreateDefaultModel();
        modelView.Initialize(model);
        statUi.Initialize(model);
        ui.PartSelected += OnPartSelected;
        ui.ColorSelected += OnColorSelected;
       
    }
    private void OnPartSelected(PartType type,int index)
    {
        SetPart(type, index);
    }
    private void OnColorSelected(PartType type, Color color)
    {
        SetColor(type, color);
    }
    private void CreateDefaultModel() 
    {
        model = new RobotModel(
              CreatePart(db.Legs[0]),
              CreatePart(db.Torso[0]),
              CreatePart(db.Head[0])
          );
    }

    public void SetPart(PartType type, int index)
    {
        switch (type)
        {
            case PartType.Legs:
                model.Legs.Part = db.Legs[Clamp(index, db.Legs.Count)];
                break;

            case PartType.Torso:
                model.Torso.Part = db.Torso[Clamp(index, db.Torso.Count)];
                break;

            case PartType.Head:
                model.Head.Part = db.Head[Clamp(index, db.Head.Count)];
                break;
        }
    }

    public void SetColor(PartType type, Color color)
    {
        switch (type)
        {
            case PartType.Legs:
                model.Legs.Color = color;
                break;

            case PartType.Torso:
                model.Torso.Color = color;
                break;

            case PartType.Head:
                model.Head.Color = color;
                break;
        }
    }


    private int Clamp(int index, int max)
    {
        if (max <= 0) return 0;
        return Mathf.Clamp(index, 0, max - 1);
    }
    RobotPartState CreatePart(RobotPart part)
    {
        return new RobotPartState(part,Color.white);
    }
    private void OnDestroy()
    {
        ui.PartSelected -= OnPartSelected;
        ui.ColorSelected -= OnColorSelected;
    }
}
