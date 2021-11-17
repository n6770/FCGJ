using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
     public PlayerScript playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerScript.gravity = !playerScript.gravity;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerScript.movement = !playerScript.movement;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerScript.dodge = !playerScript.dodge;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerScript.shooting = !playerScript.shooting;
        }
    }
}
