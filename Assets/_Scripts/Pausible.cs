using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Pausible : MonoBehaviour
{

    public List<MonoBehaviour> scripts;
    public List<NavMeshAgent> agents;
    public bool isGamePaused;

    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;   
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;


        foreach(var script in scripts)
        {
            script.enabled = !isGamePaused;
        }

        foreach (var script in agents)
        {
            script.enabled = !isGamePaused;
        }
    }
}
