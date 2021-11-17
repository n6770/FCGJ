using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementMalfunction", menuName = "Movement Malfunction", order = 1)]
public class MovementMalfunctionSO : ScriptableObject
{
    public bool gravity, movement, dodge;
    public float movementSpeed, malfunctionTime = 15f;
    public Sprite malfunctionSprite;
    public string malfunctionText;
}
