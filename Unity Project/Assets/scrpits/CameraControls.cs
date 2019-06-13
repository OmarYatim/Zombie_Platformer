using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
	
	[SerializeField]
	GameObject player;
	private float xMin = -173;
	private float xMax = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
		gameObject.transform.position = new Vector3(x,transform.position.y, -10);
	}
}
