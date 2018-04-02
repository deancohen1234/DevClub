using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum Direction {Forward, Backward, Left, Right}
    public Direction m_Direction = Direction.Forward;

    public float m_SmoothSpeed = 5f;
    public float m_MoveSpeed = 5f;

    private CharacterController m_CharacterController;
    // Use this for initialization
    void Start () {
        m_CharacterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveCharacter();
        //RotateCharacter();
        ManageInput();


    }

    private void ManageInput()
    {
        if (Input.GetKeyDown("joystick button 0")) //A
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
            m_Direction = Direction.Backward;
        }
        else if(Input.GetKeyDown("joystick button 1")) //B
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
            m_Direction = Direction.Right;
        }
        else if (Input.GetKeyDown("joystick button 2")) //X
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 270f, 0));
            m_Direction = Direction.Left;
        }
        else if (Input.GetKeyDown("joystick button 3")) //Y
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
            m_Direction = Direction.Forward;
        }
    }

    private void MoveCharacter()
    {
        float horizontalVal = Input.GetAxis("Horizontal");
        float verticalVal = Input.GetAxis("Vertical");

        float moveAngle = (Mathf.Atan2(verticalVal, horizontalVal) * 180 / Mathf.PI) - 90f; //-90 because it seems to be off by 90 degrees 
        moveAngle *= -1f;

        float f = 1f;
        if (horizontalVal == 0 && verticalVal == 0)
        {
            f = 0;
        }

        Vector3 direction = new Vector3(Mathf.Sin(moveAngle * Mathf.Deg2Rad), 0, Mathf.Cos(moveAngle * Mathf.Deg2Rad));


        //transform.Translate(direction * f * Time.deltaTime * m_MoveSpeed, Space.World);
        m_CharacterController.Move(direction * f * Time.deltaTime * m_MoveSpeed);
    }

    private void RotateCharacter()
    {
        float rightHorizontalVal = Input.GetAxis("Horizontal 2");
        float rightVerticalVal = Input.GetAxis("Vertical 2");

        float rotateAngle = (Mathf.Atan2(rightVerticalVal, rightHorizontalVal) * 180 / Mathf.PI) - 90f; //-90 because it seems to be off by 90 degrees 
        rotateAngle *= -1f;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, rotateAngle, 0)), Time.deltaTime * m_SmoothSpeed);
    }
}
