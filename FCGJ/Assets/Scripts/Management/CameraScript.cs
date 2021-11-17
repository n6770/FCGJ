using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rbPlayer;
    public PlayerScript playerScript;
    public float followSpeed;
    public float offsetMultiplier;
    private Rect screenRect;
    Vector3 movePos;
    Vector3 movePosOffset;
    Vector3 mousePos;
    Vector3 targetPos;
    
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        screenRect = new Rect(0f, 0f, Screen.width, Screen.height);

        float cameraSens = 4f;
        float hor = rbPlayer.velocity.x / 60;
        float ver = rbPlayer.velocity.y / 60;
        hor = Mathf.Clamp(hor, -1f, 1f);
        ver = Mathf.Clamp(ver, -1f, 1f);

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = new Vector3(hor, ver);
        movePos = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        movePosOffset = new Vector3(movePos.x + (offset.x * offsetMultiplier), movePos.y + (offset.y * offsetMultiplier), -10f);
        
        if (screenRect.Contains(Input.mousePosition)) 
        {
            targetPos = (mousePos - player.transform.position);
            targetPos.z = -10f;
            targetPos.x *= 0.7f;
            transform.position = Vector3.Lerp(transform.position, movePosOffset + (targetPos.normalized * cameraSens), followSpeed * Time.fixedDeltaTime);
        }
        else
        {
            targetPos = player.transform.position;
            transform.position = Vector3.Lerp(transform.position, movePosOffset, followSpeed * Time.fixedDeltaTime);
        }
    }
}
