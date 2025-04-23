using System.Collections;
using UnityEngine;

public class Tinkerller : MonoBehaviour
{

	[SerializeField] GameObject kart;
	Movement kartic;
	MPack.MArray posxs, posys, poszs;
	MPack.MArray rotws, rotxs, rotys, rotzs;

	Animator anim;
	int i;

	// Use this for initialization
	void Start()
	{
		kartic = kart.GetComponent<Movement>();
		posxs = kartic.dict["posxs"] as MPack.MArray;
		posys = kartic.dict["posys"] as MPack.MArray;
		poszs = kartic.dict["poszs"] as MPack.MArray;

		rotws = kartic.dict["rotws"] as MPack.MArray;
		rotxs = kartic.dict["rotxs"] as MPack.MArray;
		rotys = kartic.dict["rotys"] as MPack.MArray;
		rotzs = kartic.dict["rotzs"] as MPack.MArray;

		anim = GetComponent<Animator>();
	}

	void SetGhost()
	{
		transform.SetPositionAndRotation(new Vector3(posxs[i].To<float>(), posys[i].To<float>(), poszs[i].To<float>()),
			new Quaternion(rotxs[i].To<float>(), rotys[i].To<float>(), rotzs[i].To<float>(), rotws[i].To<float>()));
		transform.Rotate(-90f, 0, 0);
		if (++i >= posxs.Count)
		{
			anim.enabled = false;
			i = 0;
		}
	}

	public void JumpStart()
	{
		anim.enabled = true;
		i = 0;
		SetGhost();
	}

	private void FixedUpdate()
	{
		if (anim.enabled)
			SetGhost();
	}
}