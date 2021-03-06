﻿using UnityEngine;
using System.Collections;

public class CameraBehaviour2D : MonoBehaviour {

	

	/*A 2D camera controller that uses the mouse and mousewheel to navigate the world.
	 *Uses an upper bound, the top left of the map, and a lower bound, the bottom right, to restrict camera movement within the game level.
	 *upperHeightBound and lowerHeightBound are used to control the camera zoom level.
	 */



	//upperBound is the top left corner of the map.
	//lowerBound is the bottom right corner of the map.
	public Vector2 upperBound;
	public Vector2 lowerBound;



	//upperHeightBound is the furthest distance away possible.
	//lowerHeightBound is the closest distance possible.
	public float upperHeightBound;
	public float lowerHeightBound;
	public float panSpeed;
	public float zoomLevel;

	// Use this for initialization
	void Start () {
		//Use this space on initialization to receive any specific camera boundary requirements from a Game Manager or Game World object.
		upperHeightBound = 8.0f;
		lowerHeightBound = 3.0f;
		zoomLevel = Camera.main.orthographicSize;
		panSpeed = 0.15f;
	}
	
	// Update is called once per frame
	void Update () {

		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			//Android and IPhone API controller.

			/*Unfinished Code, determine positions using finger touches.
			if (Input.touchCount == 2) {

			}
			else if (Input.touchCount == 1) {
				if (Input.GetTouch(0).phase == TouchPhase.Moved) {
					//Pan the camera if finger is moved across the screen

					Camera.main.GetComponent<CameraFocus>().UnFocus();
				}
			}*/

		}
		else {


			/*Height camera controller to determine the zoom level of the camera.
			 *
			 *If the mouse wheel is scrolled, move the camera in or out.
			 */
			if (Input.GetAxis("Mouse ScrollWheel") < 0 && zoomLevel < upperHeightBound) {
				//Moves the camera further away.
				zoomLevel++;
			}

			if (Input.GetAxis("Mouse ScrollWheel") > 0 && zoomLevel > lowerHeightBound) {
				//Moves the camera closer.
				zoomLevel--;
			}

			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomLevel, 10.0f * Time.deltaTime);


			/*Map panning controller to determine the position of the camera
			 *
			 *If the right mouse button is held down, and the mouse is moved, pan the camera across the scene.
			 *Establish the end position and lerp to it? Might be too clunky for camera. Possibly use velocity to push the camera by force?
			 *
			 *The effect is to try and keep the cursor in the same position, and have it drag the map around.
			 */

			if (Input.GetMouseButton(1) == true && gameObject.GetComponent<OnClickMenu>().GetMenuOpen() != true) {
				Camera.main.GetComponent<CameraFocus>().UnFocus();

				//If mouse moves right, move camera right, if camera moves left, move camera right.
				if (Input.GetAxis("Mouse X") != 0) {
					Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - (Input.GetAxis("Mouse X") * panSpeed), Camera.main.transform.position.y, -5);
				}

				//If mouse moves up, move camera up, if mouse moves down, move camera down
				if (Input.GetAxis("Mouse Y") != 0) {
					Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - (Input.GetAxis("Mouse Y") * panSpeed), -5);
				}



			}

		}
	}
}
