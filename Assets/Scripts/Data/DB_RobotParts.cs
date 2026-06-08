using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DB_RobotParts", menuName = "Scriptable Objects/DB_RobotParts")]
public class DB_RobotParts : ScriptableObject
{
    [SerializeField] private List<RobotPart> head;
    [SerializeField] private List<RobotPart> torso;
    [SerializeField] private List<RobotPart> legs;

    public List<RobotPart> Head => head;
    public List <RobotPart> Torso => torso;
    public List <RobotPart> Legs => legs;
    
}
