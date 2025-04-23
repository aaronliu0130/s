using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MPack;
using System.IO;

public class Movement : MonoBehaviour
{
    Rigidbody rib;
    [SerializeField] float jump = 814;
    Animator anim;
    [SerializeField] Orphanage orbius;
    [System.NonSerialized] public bool started;
    [System.NonSerialized] public bool z; // rotate z on camera rotate

    float time;
    [SerializeField] GameObject stopwatch;
    TextMeshProUGUI stopwatcher;
    List<float> posxs = new(), posys = new(), poszs = new();
    List<float> rotws = new(), rotxs = new(), rotys = new(), rotzs = new();
    [System.NonSerialized] public MDict dict;

    [SerializeField] RotPow rotter;

    // Start is called before the first frame update
    void Start()
    {
        rib = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        started = false;
        z = false;

        time = 0f;
        stopwatcher = stopwatch.GetComponent<TextMeshProUGUI>();
        stopwatcher.text = "00:00"; //string.Format("{0:D2}:{1,-5:G5}", time / 100, time % 100);

        Debug.Log(Directory.GetCurrentDirectory());
        //poss = new List<Vector3>();
        //rots = new List<Quaternion>();
        try
        {
            dict = (MDict)MToken.ParseFromBytes(File.ReadAllBytes("ghost.mpack"));
        } catch (FileNotFoundException)
        {
            Debug.Log("Regenerated. I am born again");
            dict = new MDict{ 
                { "time", 9999f},

                { "posxs", MToken.From(posxs.ToArray()) },
                { "posys", MToken.From(posxs.ToArray()) },
                { "poszs", MToken.From(posxs.ToArray()) },

                { "rotws", MToken.From(rotws.ToArray()) },
                { "rotxs", MToken.From(rotxs.ToArray()) },
                { "rotys", MToken.From(rotys.ToArray()) },
                { "rotzs", MToken.From(rotzs.ToArray()) },
            };
		}
    }

    // Update is called once per frame
    void Update()
    {
        float w = Input.GetAxis("Vertical");
        if (w < 0)
        {
            anim.SetTrigger("back");
        } else if (w > 0)
		{
            anim.SetTrigger("forward");
		}

        float h = Input.GetAxis("Horizontal");
		if (h < 0)
		{
            anim.SetTrigger("left");
		} else if (h > 0)
		{
            anim.SetTrigger("right");
		}

        transform.position += Time.deltaTime * h * transform.right + Time.deltaTime * w * transform.forward;

        if (Input.GetButtonDown("Jump"))
        {
            rib.AddForce(transform.up * jump);
            anim.SetTrigger("jump");
        }

        if (!z)
            transform.eulerAngles = new Vector3(0, orbius.rotation.y, 0);
        else
            transform.eulerAngles = orbius.rotation;
    }

    public void Add(Vector3 pos, Quaternion rot)
	{
        posxs.Add(transform.position.x); posys.Add(transform.position.y); poszs.Add(transform.position.z);
        rotws.Add(transform.rotation.w); rotxs.Add(transform.rotation.x); rotys.Add(transform.rotation.y); rotzs.Add(transform.rotation.z);
    }

	private void FixedUpdate()
	{
		if (started)
		{
            time += 0.024f;
            stopwatcher.text = string.Format("{0:D2}:{1,-5:G5}", Mathf.FloorToInt(time) / 100, time % 100);

            Add(transform.position, transform.rotation);

            if (time > 9999f) Stop();
        }
	}

	public void Stop()
	{
        started = false;

        if (time <= dict["time"].To<float>()) // new highscore
        {
            dict["time"] = time;

            dict["posxs"] = MToken.From(posxs.ToArray());
            dict["posys"] = MToken.From(posys.ToArray());
            dict["poszs"] = MToken.From(poszs.ToArray());

            dict["rotws"] = MToken.From(rotws.ToArray());
            dict["rotxs"] = MToken.From(rotxs.ToArray());
            dict["rotys"] = MToken.From(rotys.ToArray());
            dict["rotzs"] = MToken.From(rotzs.ToArray());

            File.WriteAllBytes("ghost.mpack", dict.EncodeToBytes());
        }

        time = 0f;
        transform.position = new Vector3(0, -0.46f, 0);
        transform.eulerAngles = new Vector3(0, 180f, 0);
        posxs.Clear();
        posys.Clear();
        poszs.Clear();
        rotws.Clear();
        rotxs.Clear();
        rotys.Clear();
        rotzs.Clear();
    }
}
