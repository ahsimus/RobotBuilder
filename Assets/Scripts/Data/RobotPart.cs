using UnityEngine;

[CreateAssetMenu(fileName = "RobotPart",menuName = "Robot Builder/Robot Part")]
public class RobotPart : ScriptableObject
{
    [SerializeField] private PartType partType;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float weight, power;


    public PartType PartType => partType;
    public GameObject Prefab => prefab;
    public float Weight => weight;
    public float Power => power;
}
public enum PartType 
{
    Legs,
    Torso,
    Head
}