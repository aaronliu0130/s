using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flag : MonoBehaviour
{
    readonly Vector3[] pos = { new Vector3(-1.04f, 1.55f, -2.096f),
        new Vector3(-3.47f, 0.179f, -4.98f),
        new Vector3(-9.96f, -0.43f, -4.38f),
        new Vector3(-10.76f, -0.43f, -11.17f),
        new Vector3(-0.03f, 26f, 15.68f),
        new Vector3(37.5f, 48f, -43.4f),
        new Vector3(0.23f, 13.58f, -33.18f),
        new Vector3(-1.04f, 1.55f, -2.096f)
    };
    int i = 1;

    [SerializeField] GameObject flaggy, tinkerlle;
    TextMeshProUGUI flags;
    // Start is called before the first frame update
    void Start()
    {
        flags = flaggy.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        if (i == 1)
		{
            other.GetComponent<Movement>().started = true;
            tinkerlle.GetComponent<Tinkerller>().JumpStart();
		}
        if (i < pos.Length)
        {
            transform.position = pos[i++];
            flags.text = "" + (i - 1);
        }
        else
        {
            flags.text = "" + (i - 1);
            i = 1;
            transform.position = pos[0];
            other.GetComponent<Movement>().Stop();
        }
	}
}
