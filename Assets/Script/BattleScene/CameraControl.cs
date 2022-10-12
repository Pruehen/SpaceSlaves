using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    bool isClicked = false;
    Vector2 clickPoint;
    float dragSpeed = 15f;
    float WheelSpeed = 10f;

    private void Update()
    {
        Vector3 position = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);

        ClickCheck();

        if(isClicked == true)
        {
            CamMove(position);
        }

        ZoomIN_Ont(position);
    }

    void ZoomIN_Ont(Vector3 Pos)
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //Camera.main.fieldOfView += WheelSpeed; // fov로 이동
            Pos.z = Pos.y;
            Pos.y = 0f;

            Vector3 move = Pos * (Time.deltaTime * dragSpeed);

            float x = transform.position.x;

            transform.Translate(move);
            transform.transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //Camera.main.fieldOfView -= WheelSpeed; //fov로 이동
            Pos.z = Pos.y;
            Pos.y = 0f;

            Vector3 move = -Pos * (Time.deltaTime * dragSpeed);

            float x = transform.position.x;
            float z = transform.position.z;

            transform.Translate(move);
            transform.transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }

    void CamMove(Vector3 Pos)
    {
        Pos.z = Pos.y;
        Pos.y = 0f;

        Vector3 move = -Pos * (Time.deltaTime * dragSpeed);

        float y = transform.position.y;

        transform.Translate(move);
        transform.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPoint = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            isClicked = true;
        }
        else
        {
            isClicked = false;
        }
    }
}
