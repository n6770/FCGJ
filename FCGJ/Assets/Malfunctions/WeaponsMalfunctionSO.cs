using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsMalfunction", menuName = "Weapons Malfunction", order = 1)]
public class WeaponsMalfunctionSO: ScriptableObject
{
    public bool shooting, multishot;
    public float firingSpeed, malfunctionTime = 15f;
    public Sprite malfunctionSprite;
    public string malfunctionText;
}
