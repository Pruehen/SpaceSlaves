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
    float WheelSpeed = 4f;
    public GameObject Cam;

    private void Update()
    {
        Vector3 CamPos = Cam.transform.position;

        ClickCheck();
        DragPos(clickPoint);
        CamMove(MovePos);

        ZoomIN_Ont(CamPos);
    }

    void DragPos(Vector2 Pos)
    {
        Vector2 Value = Pos - UpPoint;
        Value = Value.normalized;

        MovePos = 
            new Vector2(Value.x * 100 * Time.deltaTime, 
            Value.y * 100 * Time.deltaTime);
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
            Pos.z = Pos.y;
            Pos.y = 0f;

            Vector3 move = Pos * (Time.deltaTime * dragSpeed);

            float y = transform.position.y;

            transform.Translate(move);
            transform.transform.position = 
                new Vector3(transform.position.x, y, transform.position.z);
        }
    }

    void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
            clickPoint = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isClicked = false;
            UpPoint = Input.mousePosition;
        }
        else if(Input.GetMouseButtonDown(1))
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
