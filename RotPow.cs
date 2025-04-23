using System.Collections;
using UnityEngine;

public class RotPow : MonoBehaviour
{
	Movement move;
	[SerializeField] GameObject powerpan;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		move = other.GetComponent<Movement>();
		move.z = true;
		powerpan.SetActive(true);
	}

}