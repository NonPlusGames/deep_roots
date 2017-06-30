using UnityEngine;
using System.Collections;

public class FastFwd : MonoBehaviour {

    public GameObject sun;
    
    public void FastForward()
    {
       
        if (sun.GetComponent<Sun>().clicked == false)
        {
            sun.GetComponent<Sun>().turnSpeed = 55;
            sun.GetComponent<Sun>().clicked = true;
        }
        else if (sun.GetComponent<Sun>().clicked == true)
        {
            sun.GetComponent<Sun>().turnSpeed = 25;
            sun.GetComponent<Sun>().clicked = false;
        }
    }
}
