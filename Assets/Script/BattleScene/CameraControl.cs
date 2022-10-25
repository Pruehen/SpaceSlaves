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
    float dragSpeed = 5f;
    float defaultdragSpeed = 5f;
    float defaultRotateSpeed = 200f;
    float WheelSpeed = 4f;
    public GameObject Cam;
    private float xRotateMove;
    Vector3 defaultCamPos;

    private void Start()
    {
        defaultCamPos = this.transform.position;
    }

    private void Update()
    {
        Vector3 CamPos = Cam.transform.position;

        ClickCheck();
        DragPos(clickPoint);
        CamMove(MovePos);

        ZoomIN_Ont(CamPos);
        CamRotate(CamPos);

        //ZoomTarget();
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

    RaycastHit hit;

    /*void ZoomTarget()
    {
        if(Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 1000))
        {
            Debug.Log(hit.collider.name);
            //Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * hit.distance, Color.red);
            if (hit.distance <= 1f)
            {
                Debug.Log("ø¿øπ~~~");
                //yRotateMove = dragSpeed * Time.deltaTime;

                //Cam.transform.localEulerAngles = 
                //new Vector3(transform.rotation.x, transform.rotation.y, hit.transform.rotation.z);

                Vector3 dir = new Vector3(hit.transform.rotation.x, defaultCamPos.y, defaultCamPos.z);

                transform.rotation =
                    Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 0.1f);
                //defaultdragSpeed /= 2;
            }
            else if(hit.distance > 1f)
            {
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, Quaternion.Euler(defaultCamPos), Time.deltaTime * 0.1f);
                //defaultdragSpeed *= 2;
            }
        }
    }*/

    float zoomValue = 0; //0~1
    float zoomPower = 0.7f;

    void ZoomIN_Ont(Vector3 Pos)
    {
        Vector3 move = Vector3.zero;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)//»Ÿ≥ª∏±ãö ¡‹¿Œ
        {
            if(zoomValue < 1)
            {
                zoomValue += Time.deltaTime * WheelSpeed;
                this.transform.position += transform.forward * Time.deltaTime * zoomPower;
                transform.Rotate(transform.right, -0.5f);
            }
            //Camera.main.fieldOfView += WheelSpeed; // fov∑Œ ¿Ãµø
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)//»Ÿ¥Á±Êãö ¡‹æ∆øÙ
        {
            if (zoomValue > 0)
            {
                zoomValue -= Time.deltaTime * WheelSpeed;
                this.transform.position -= transform.forward * Time.deltaTime * zoomPower;
                transform.Rotate(transform.right, 0.5f);
            }
            //Camera.main.fieldOfView -= WheelSpeed; //fov∑Œ ¿Ãµø
        }

        this.transform.position = new Vector3(transform.position.x, defaultCamPos.y - zoomValue * zoomPower, transform.position.z);
        Debug.Log(zoomValue);
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

            //transform.LookAt(Pos);
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
