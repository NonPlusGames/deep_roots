using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour
{

    public float turnSpeed = 25f;
    public bool clicked = false;
    // Update is called once per frame
    void Update()
    {

        transform.eulerAngles += new Vector3(0f, 0f, -turnSpeed * Time.deltaTime);
        
    }
}