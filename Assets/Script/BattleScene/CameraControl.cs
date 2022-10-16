using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    bool isClicked = false;
    bool IsRClicked = false;
    Vector2 clickPoint;
    Vector2 UpPoint;
    Vector2 MovePos;
    float dragSpeed = 15f;
    float defaultdragSpeed = 15f;
    float defaultRotateSpeed = 500f;
    float WheelSpeed = 4f;
    public GameObject Cam;
    private float xRotateMove;
    private float yRotateMove;

    private void Update()
    {
        Vector3 CamPos = Cam.transform.position;

        ClickCheck();
        DragPos(clickPoint);
        CamMove(MovePos);

        ZoomIN_Ont(CamPos);
        CamRotate(CamPos);
    }

    float camControlSpeed = 10f;
    Vector2 valueTemp;

    void DragPos(Vector2 Pos)
    {
        //Vector2 Value = Pos - UpPoint;
        //Value = Value.normalized;

        Vector2 value = Pos;
        Vector2 value_dif = value - valueTemp;
        valueTemp = value;

        //MovePos = 
        //    new Vector2(value.x * 100 * Time.deltaTime,
        //    value.y * 100 * Time.deltaTime);
        if (value_dif.magnitude < 30)
        {
            MovePos = -value_dif * camControlSpeed * Time.deltaTime;
        }
    }

    void ZoomIN_Ont(Vector3 Pos)
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)//ÈÙ³»¸±‹š ÁÜÀÎ
        {
            //Camera.main.fieldOfView += WheelSpeed; // fov·Î ÀÌµ¿

            Vector3 move = Pos * (Time.deltaTime * WheelSpeed);

            float z = transform.position.z;
            //float z = transform.position.z;

            transform.Translate(move);
            transform.transform.position = 
                new Vector3(transform.position.x, transform.position.y, z);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)//ÈÙ´ç±æ‹š ÁÜ¾Æ¿ô
        {
            //Camera.main.fieldOfView -= WheelSpeed; //fov·Î ÀÌµ¿

            Vector3 move = -Pos * (Time.deltaTime * WheelSpeed);

            float z = transform.position.z;
            //float z = transform.position.z;

            transform.Translate(move);
            transform.transform.position = 
                new Vector3(transform.position.x, transform.position.y, z);

            Debug.Log(transform.position.y);
        }
    }

    void CamMove(Vector3 Pos)
    {
        if (isClicked == true)
        {
            if(Input.GetAxis("Mouse X") == 0)
            {
                dragSpeed = 0;
            }
            else if(Input.GetAxis("Mouse Y") == 0)
            {
                dragSpeed = 0;
            }
            else
            {
                dragSpeed = defaultdragSpeed;
            }          

            Pos.z = Pos.y;
            Pos.y = 0f;

            Vector3 move = Pos * (Time.deltaTime * dragSpeed);

            float y = transform.position.y;

            transform.Translate(move);
            transform.position = 
                new Vector3(transform.position.x, y, transform.position.z);
        }
    }

    void CamRotate(Vector3 Pos)
    {
        if (IsRClicked == true)
        {
            if (Input.GetAxis("Mouse X") == 0 || Input.GetAxis("Mouse Y") == 0)
            {
                dragSpeed = 0;
            }
            else
            {
                dragSpeed = defaultRotateSpeed;
            }

            xRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * dragSpeed;
            //yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * dragSpeed;

            //transform.RotateAround(Pos, Vector3.right, -yRotateMove);
            transform.RotateAround(Pos, Vector3.up, xRotateMove);

            transform.LookAt(Pos);
        }
    }

    void ClickCheck()
    {
        if (Input.GetMouseButton(0))
        {
            isClicked = true;
            clickPoint = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isClicked = false;
            UpPoint = Input.mousePosition;
        }
        else if(Input.GetMouseButton(1))
        {
            IsRClicked = true;
            clickPoint = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            IsRClicked = false;
            UpPoint = Input.mousePosition;
        }
    }
}
