using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMover : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x <= -10f)
        {
            transform.Translate(Vector3.right * 20f);
        }
    }
}
