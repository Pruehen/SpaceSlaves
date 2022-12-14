using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraControl : MonoBehaviour
{
    //public TimeScaleCon TimeScale;

    bool isClicked = false;
    bool IsRClicked = false;
    Vector2 clickPoint;
    Vector2 UpPoint;
    Vector2 MovePos;
    public float dragSpeed = 10f;
    public float defaultdragSpeed = 10f;
    public float defaultRotateSpeed = 300f;
    float WheelSpeed = 10f;
    public GameObject Cam;
    private float xRotateMove;
    private float yRotateMove;
    Vector3 defaultCamPos;


    Rigidbody rb;
    private void Start()
    {
        defaultCamPos = this.transform.position;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //CamSpeedCon(TimeScale.count, TimeScale.isClicked);

        Vector3 CamPos = Cam.transform.position;

        ClickCheck();
        DragPos(clickPoint);
        CamMove(MovePos);

        ZoomIN_Ont(CamPos);
        CamRotate(CamPos);

        //ZoomTarget();

        AutoRotate();
    }

    /*void CamSpeedCon(float count, bool isClick)
    {
        if (isClick == true)
        {
            dragSpeed /= count;
            defaultRotateSpeed /= count;

            Debug.Log(defaultRotateSpeed);

            TimeScale.isClicked = false;
        }
    }*/

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
                Debug.Log("????~~~");
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
    float zoomPower = 2.7f;

    public Scrollbar scrollbar;

    void ZoomIN_Ont(Vector3 Pos)
    {
        Vector3 move = Vector3.zero;
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && scrollbar.value < 1)//???????? ????
        {
            scrollbar.value += Time.deltaTime * WheelSpeed;
            this.transform.position += transform.forward * Time.deltaTime * zoomPower;
            //transform.Rotate(transform.right, -0.5f);
            //Camera.main.fieldOfView += WheelSpeed; // fov?? ????
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && scrollbar.value > 0)//???????? ??????
        {
            scrollbar.value -= Time.deltaTime * WheelSpeed;
            this.transform.position -= transform.forward * Time.deltaTime * zoomPower;
            //transform.Rotate(transform.right, 0.5f);
            //Camera.main.fieldOfView -= WheelSpeed; //fov?? ????
        }

        this.transform.position = new Vector3(transform.position.x, defaultCamPos.y - scrollbar.value * zoomPower, transform.position.z);
        //Debug.Log(zoomValue);
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

            Vector3 move = Vector3.ClampMagnitude(Pos * (Time.deltaTime * dragSpeed * (1/Time.timeScale)), 1);

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

            xRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * dragSpeed * (1 / Time.timeScale);
            yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * dragSpeed * (1 / Time.timeScale);
            //yRotateMove = Input.GetAxis("Mouse Y") * Time.deltaTime * dragSpeed;

            //transform.RotateAround(Pos, Vector3.right, -yRotateMove);
            transform.RotateAround(Pos, Vector3.up, xRotateMove);
            transform.RotateAround(Pos, this.transform.right, -yRotateMove);

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

    void AutoRotate()
    {
        float yPos = this.transform.position.y;

        rb.AddTorque(this.transform.forward * -this.transform.right.y * 5, ForceMode.Force);
        //rb.AddTorque(this.transform.right * (yPos * 0.25f + this.transform.forward.y) * 5, ForceMode.Force);
    }
}
