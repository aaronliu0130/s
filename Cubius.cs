using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubius : MonoBehaviour
{
    Vector3 initpos;
	int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        initpos = transform.position;
		if (!this.transform.parent)
			InvokeRepeating(nameof(Clone), 0.4f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
		if (transform.parent)
		{
            transform.Translate(Time.deltaTime * Vector3.right);
		}
    }

    public void Clone()
	{
		if (count < 4 && !transform.parent)
		{
			count++;
			GameObject newcc = Instantiate(gameObject, gameObject.transform);
			Cubius newc = newcc.GetComponent<Cubius>();
			newc.CancelInvoke(nameof(Clone));
		}
	}

	private void OnBecameInvisible()
	{
		if (transform.parent)
		{
            Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{
		transform.parent.gameObject.GetComponent<Cubius>().count--;
	}
}
