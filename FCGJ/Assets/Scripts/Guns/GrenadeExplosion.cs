using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.3f);
    }

}
