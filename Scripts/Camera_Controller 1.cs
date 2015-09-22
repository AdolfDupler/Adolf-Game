using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {


	float x;
	float z;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		//x=(Input.mousePosition.x - Screen.width/2)/20;
		//y=(Input.mousePosition.y - Screen.height/2)/20;
		x += Input.GetAxis ("Horizontal")/3;
		z += Input.GetAxis ("Vertical")/3;

		transform.position = new Vector3(x,10,z);

		/*if (Input.GetButton("Fire1")) {
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cube.transform.position = new Vector3(x, 0, z);
            cube.AddComponent<changify>();
		}*/

	}
}
