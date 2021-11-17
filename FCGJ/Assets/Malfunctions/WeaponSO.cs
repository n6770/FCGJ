using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 1)]
public class WeaponSO: ScriptableObject
{
    public float firingSpeedMultiplier;
    public GameObject projectile;
}
