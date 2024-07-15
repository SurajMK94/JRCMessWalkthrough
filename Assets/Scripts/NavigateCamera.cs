using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateCamera : MonoBehaviour
{
    private Touch initTouch = new Touch();
    public Camera cam;

    private float rotX = 0f;
    private float rotY = 0f;
    private Vector3 origRot;

    public float rotSpeed = 0.2f;
    public float dir = 1;

    float touchDist = 0;
    float lastDist = 0;
    private Vector3 previousMousePosition = Vector3.zero;

    public void Initialise()
    {
        origRot = cam.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;
    }


    void FixedUpdate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidNavigation();
#elif UNITY_EDITOR || Unity_Win
        WindowsNavigation();
#endif
    }

    private void AndroidNavigation()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initTouch = touch;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                //swiping
                float deltaX = initTouch.position.x - touch.position.x;
                float deltaY = initTouch.position.y - touch.position.y;
                rotX -= deltaY * Time.deltaTime * rotSpeed * dir;
                rotY += deltaX * Time.deltaTime * rotSpeed * dir;
                cam.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                initTouch = new Touch();
            }
        }

        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
            {
                lastDist = Vector2.Distance(touch1.position, touch2.position);
            }

            if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
            {
                float newDist = Vector2.Distance(touch1.position, touch2.position);
                touchDist = lastDist - newDist;
                lastDist = newDist;
                cam.fieldOfView += touchDist * 0.02f;
            }

            if (cam.fieldOfView > 110)
            {
                cam.fieldOfView = 110;
            }
            else if (cam.fieldOfView < 30)
            {
                cam.fieldOfView = 30;
            }
        }
    }

    private void WindowsNavigation()
    {
        // Check for mouse drag input
        if (Input.GetMouseButton(0))
        {
            // Calculate the mouse position delta
            Vector3 mousePosDelta = Input.mousePosition - previousMousePosition;

            if (mousePosDelta.magnitude > 0.1f)
            {
                // Use the X-axis delta to calculate the Y-rotation
                rotY += mousePosDelta.x * -rotSpeed;
                rotX += mousePosDelta.y * rotSpeed;

                cam.transform.Rotate(Vector3.right * rotX, Space.Self);
                cam.transform.Rotate(-Vector3.up * rotY, Space.Self);

                // Apply the rotation to the camera, only rotating around the Y-axis
                transform.rotation = Quaternion.Euler(rotX, rotY, 0f);
            }
        }

        // Store the current mouse position for the next frame
        previousMousePosition = Input.mousePosition;
    }
}
