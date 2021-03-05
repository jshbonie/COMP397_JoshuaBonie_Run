using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {




    }
}
