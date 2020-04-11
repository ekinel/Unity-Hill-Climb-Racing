using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
	WheelJoint2D[] wheelJoints;
	JointMotor2D frontWheel, backWheel;

	public float maxSpeed = -1000f;
	private float maxBackSpeed = 1500f;
	private float acceleration = 250f, deacceleration = -100f;
	public float brakeForce = 3000f;
	private float gravity = 9.8f, angle = 0;

	public ClickScript[] CarControl;

	private bool grounded = false;
	public LayerMask map;
	public Transform bWheel;

    void Start()
    {
		wheelJoints = gameObject.GetComponents<WheelJoint2D>();
		frontWheel = wheelJoints[0].motor;
		backWheel = wheelJoints[1].motor;
    }

	void Update()
	{
		grounded = Physics2D.OverlapCircle(bWheel.transform.position, 0.35f, map);
	}

	void FixedUpdate()
    {
		frontWheel.motorSpeed = backWheel.motorSpeed;
		angle = transform.localEulerAngles.z;

		if(angle >= 180)
		{
			angle = angle - 360;
		}

		if(grounded == true)
		{
			if (CarControl[0].isClicked == true)
			{
				backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (acceleration - gravity * Mathf.PI * (angle / 180) * 80) * Time.deltaTime, maxSpeed, maxBackSpeed);
			}

			if ((CarControl[0].isClicked == false && backWheel.motorSpeed < 0) || (CarControl[0].isClicked == false && backWheel.motorSpeed == 0 && angle < 0))
			{
				backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (deacceleration - gravity * Mathf.PI * (angle / 180) * 80) * Time.deltaTime, maxSpeed, 0);
			}
			else if ((CarControl[0].isClicked == false && backWheel.motorSpeed > 0) || (CarControl[0].isClicked == false && backWheel.motorSpeed == 0 && angle > 0))
			{
				backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - (-deacceleration - gravity * Mathf.PI * (angle / 180) * 80) * Time.deltaTime, 0, maxBackSpeed);
			}
		}
		else if(CarControl[0].isClicked == false && backWheel.motorSpeed < 0)
		{
			backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - deacceleration * Time.deltaTime, maxSpeed, 0);
		}
		else if(CarControl[0].isClicked == false && backWheel.motorSpeed > 0)
		{
			backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + deacceleration * Time.deltaTime, 0, maxSpeed);
		}

		if (CarControl[1].isClicked == true && backWheel.motorSpeed > 0)
		{
			backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed - brakeForce * Time.deltaTime, 0, maxBackSpeed);
		}
		else if(CarControl[1].isClicked == true && backWheel.motorSpeed < 0)
		{
			backWheel.motorSpeed = Mathf.Clamp(backWheel.motorSpeed + brakeForce * Time.deltaTime, maxBackSpeed, 0);
		}

		wheelJoints[1].motor = backWheel;
		wheelJoints[0].motor = frontWheel;
	}
}
