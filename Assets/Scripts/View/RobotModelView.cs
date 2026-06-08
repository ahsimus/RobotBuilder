using System.Collections.Generic;
using UnityEngine;


public class RobotModelView : MonoBehaviour
{
    private RobotModel model;
    [SerializeField] Transform root;
    private readonly Dictionary<PartType, GameObject> parts = new();

  
    public void Initialize(RobotModel model)
    {
        this.model = model;

        model.ModelChanged += OnModelChanged;
        model.ColorChanged += OnColorChanged;

        Build();
    }
    private void OnModelChanged()
    {
        Build();
    }
    private void OnColorChanged(PartType type, Color color)
    {
        ApplyColor(type, color);
    }









    public void Build() 
    {
        Clear();

        SpawnPart(PartType.Legs, model.Legs);
        SetBottomAt(parts[PartType.Legs],root.position.y);//allign to the floor

        SpawnPart(PartType.Torso, model.Torso);
        SetBottomAt(parts[PartType.Torso], GetTopY(parts[PartType.Legs]));

        SpawnPart(PartType.Head, model.Head);
        SetBottomAt(parts[PartType.Head], GetTopY(parts[PartType.Torso]));

        ApplyColors(model);
    }

    void Clear() 
    {
        foreach (var kv in parts)
        {
            if (kv.Value)
                Destroy(kv.Value);
        }

        parts.Clear();
    }
    void SpawnPart(PartType type, RobotPartState partState) 
    {
        GameObject instance = Instantiate(partState.Part.Prefab, root);
        instance.transform.localPosition = Vector3.zero;
        parts[type] = instance;
    }

    float GetTopY(GameObject obj) 
    {
        Bounds bounds = GetBounds(obj);
        return bounds.max.y;
    }
    void SetBottomAt(GameObject obj, float Ypos) 
    {
        Bounds bounds = GetBounds(obj);
        float offset = Ypos - bounds.min.y;
        obj.transform.position += new Vector3(0f, offset, 0f);
    }
    Bounds GetBounds(GameObject obj) 
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0)
            return new Bounds(obj.transform.position, Vector3.zero);

        Bounds bounds = renderers[0].bounds;

        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        return bounds;

    }


    #region COLOR API
    public void ApplyColors(RobotModel model)
    {
        ApplyColor(PartType.Legs, model.Legs.Color);
        ApplyColor(PartType.Torso, model.Torso.Color);
        ApplyColor(PartType.Head, model.Head.Color);
    }
    public void ApplyColor(PartType type, Color color)
    {
        if (!parts.TryGetValue(type, out var obj))
            return;

        SetColor(obj, color);
    }
    private void SetColor(GameObject obj, Color color)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (var r in renderers)
        {
            r.material.color = color;
        }
    }

    #endregion
}
