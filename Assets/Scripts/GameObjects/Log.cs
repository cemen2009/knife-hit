using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    // this class created for modifying rotation of an Log Motor, especially wheel joint
    [System.Serializable] private class RotationElement
    {
        #pragma warning disable 0649
        public float Speed; // speed of rotation
        public float Duration;  // duration of rotation
        #pragma warning restore 0649
    }

    [SerializeField]
    private RotationElement[] rotationPattern = new RotationElement[20];  // in Unity Inspector we'll put values for duration and speed of log's rotation

    private WheelJoint2D wheelJoint;
    private JointMotor2D motor; // via motor we can control speed, spring, bracking and motor torque. We need motor for wheelJoint

    private void Awake()
    {
        for (int i = 0; i < rotationPattern.Length; i++)
        {
            if (rotationPattern.Length - i == 0 || i == 19)
                rotationPattern[i].Speed = 0f;
            if (i < rotationPattern.Length)
                rotationPattern[i].Speed = Random.Range(50f + i * 20, 100f + i * 50);
            else
            {
                rotationPattern[i].Speed = -Random.Range(50f + i * 20, 100f + i * 50);
            }
            rotationPattern[i].Duration = 0.4f;
        }
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine(PlayRotationPatter());
    }

    // Playing rotation of log using speed and duration from array rotationPattern
    private IEnumerator PlayRotationPatter()
    {
        int rotationIndex = 0;  // index of element from array. Each element has .Speed and .Duration
        while (true)    // just infinity loop
        {
            yield return new WaitForFixedUpdate();  // we work with physics, so we wait for fixed update

            motor.motorSpeed = rotationPattern[rotationIndex].Speed;    // set a speed
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;   // TODO: I need to learn about this

            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].Duration);   // motor rotates log during time taken from array
            rotationIndex++;    // rotation loop continues;

            //  if index is out of range, than index = 0, if else: everthing is cool
            rotationIndex = rotationIndex >= rotationPattern.Length ? 0 : rotationIndex;
        }
    }
}
