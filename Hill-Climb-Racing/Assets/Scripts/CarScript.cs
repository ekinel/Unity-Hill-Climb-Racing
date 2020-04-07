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

    void Start()
    {
		wheelJoints = gameObject.GetComponents<WheelJoint2D>();
		frontWheel = wheelJoints[0].motor;
		backWheel = wheelJoints[1].motor;
    }

    void Update()
    {
		angle = transform.localEulerAngles.z;
        
    }
}
