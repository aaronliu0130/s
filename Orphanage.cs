using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orphanage : MonoBehaviour
{
    Vector3 thirpos;
    readonly Vector3 firpos = new(0, 1.8f, 0);
    bool thir;
    [System.NonSerialized] public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        thirpos = transform.localPosition;
        thir = true;
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.C))
		{
            Debug.Log("camera");
            if (thir) transform.localPosition = firpos;
            else transform.localPosition = thirpos;
            thir = !thir;
        }
		if (thir)
		{
            thirpos.z += Input.mouseScrollDelta.y;
            transform.localPosition = thirpos;
		}
        if (Input.GetMouseButton(1))
        {
            rotation.x -= Input.mousePositionDelta.y;
            rotation.y += Input.mousePositionDelta.x;
            transform.eulerAngles = rotation;
		}
    }
}
