using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //setting the pan speed
    //serialising field to access it in inspector
    [SerializeField]
    private float panSpeed = 40f;
    //setting the mouse zoom speed
    [SerializeField]
    private float zoomSpeedMouse = 20f;

    //bounds of the x and z positions - the min and max amount of offset is stored in these arrays
    //x = left and right, z = forward and backward
    private float[] boundsX = new float[] { -45f, 35f };
    private float[] boundsZ = new float[] { -60f, 58f };
    //sets the bounds (max and min) that our mouse can zoom - determined by the fov slider of camera
    private float[] zoomBounds = new float[] { 25f, 53f };

    //creating an instance of the camera
    private Camera cam;
    //vector 3 to find the position of the mouse when it was last panned
    private Vector3 lastPanPos;

    //awake initialises any variables before the game starts
    //using awake to set cam (which is the instance of the camera) to the camera component of the game obj
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //call panorzoom() - checks if panning or zooming and behaves accordingly
        PanOrZoom();
    }

    void PanOrZoom()
    {
        //on mouse down, capture its position
        //otherwise, if the mouse is still down, pan the camera
        if (Input.GetMouseButtonDown(0))
        {
            lastPanPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }
        //check for scrolling to zoom the camera
        //getting scrollwheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //calling zoomcamera() that allows it to zoom
        ZoomCamera(scroll, zoomSpeedMouse);
    }

    void PanCamera(Vector3 newPanPos)
    {
        //determine how much to move the camera
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPos - newPanPos);
        Vector3 move = new Vector3(offset.x * panSpeed, 0, offset.y * panSpeed);

        //perform the movement
        transform.Translate(move, Space.World);

        //making sure the camera remains within bounds.
        Vector3 pos = transform.position;
        //clamping the camera within the x and z bounds in the arrays above so that it doesnt move outside these bounds
        pos.x = Mathf.Clamp(transform.position.x, boundsX[0], boundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, boundsZ[0], boundsZ[1]);
        transform.position = pos;
        
        //now the new pan pos becomes the last pan pos and the method is repeated with this new pos if called again
        lastPanPos = newPanPos;
    }

    void ZoomCamera(float offset, float speed)
    {
        //using the fov of the camera and clamping it between the min and max values set in the array at the top
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), zoomBounds[0], zoomBounds[1]);
    }
}
