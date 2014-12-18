using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform target;
	public bool x;
	public float xDistance;
	public bool y;
	public float yDistance;
	public bool z;
	public float zDistance;
	private Vector3 thisPosition;

	void Start () {
		thisPosition = transform.position;
	}

	void Update () {
	
		if (x)
			thisPosition.x = target.position.x + xDistance;
		if (y)
			thisPosition.y = target.position.y + yDistance;
		if (z)
			thisPosition.y = target.position.z + zDistance;

		transform.position = thisPosition;
	}
}