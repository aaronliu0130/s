using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsON : MonoBehaviour
{
    Light l;
    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButton("Jump"))
		{
            l.enabled = false;
		} else
		{
            l.enabled = true;
		}
    }
}
