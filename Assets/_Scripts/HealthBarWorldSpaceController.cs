using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarWorldSpaceController : MonoBehaviour
{
    public Transform playerCamera;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        playerCamera = GameObject.Find("PlayerCamera").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + playerCamera.forward);
    }
}
