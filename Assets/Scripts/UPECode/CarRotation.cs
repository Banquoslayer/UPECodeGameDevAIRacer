using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotation : MonoBehaviour
{

    Vector3 velo;
    Vector3 desiredVel;
    Vector3 lookDir;

    bool isMarking = false, onGround;

    [SerializeField]
    GameObject velTarg;

    [SerializeField, Range(0f, 1f)]
    float turnAroundLerp = 0.7f;

    [SerializeField, Range(0f, 1f)]
    float lerp = 0.7f;

    CarEffects carFX;

    [SerializeField]
    LayerMask probeMask = -1;

    [SerializeField, Range(0f, 0.9f)]
    float tireMarkThreshhold = 0.8f;

    AudioSource myAudio;

    private void Awake()
    {
        carFX = GetComponentInChildren<CarEffects>();
        myAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float dotTest = Vector3.Dot(velo.normalized, desiredVel.normalized);

        if (Physics.Raycast(velTarg.transform.position, Vector3.down, out RaycastHit hit, 20f,probeMask))
        {
            Quaternion targetRotation = Quaternion.LookRotation((Vector3.Cross(velTarg.transform.TransformDirection(Vector3.right), hit.normal)));

            if (!onGround)
            {
                lookDir = Vector3.Slerp(velTarg.transform.forward, velo.normalized, 0.6f);
                //Turn off tire marks
                return;
            }

            if (dotTest <= -0.98f)
            {
                lookDir = Vector3.Slerp(velTarg.transform.forward, desiredVel, turnAroundLerp);
                isMarking = true;
                isMarking &= onGround;
                velTarg.transform.forward = lookDir;
            }
            else if (dotTest >= -tireMarkThreshhold && dotTest <= tireMarkThreshhold)
            {
                lookDir = Vector3.Slerp(velTarg.transform.forward, desiredVel, lerp);
                isMarking = true;
                isMarking &= onGround;
                velTarg.transform.forward = lookDir;
            }
            else
            {
                lookDir = Vector3.Slerp(velTarg.transform.forward, desiredVel, lerp);
                isMarking = false;
                isMarking &= onGround;
                velTarg.transform.forward = lookDir;
            }

            if(velo.magnitude <= 0.05f)
            {
                isMarking = false;
                myAudio.volume = Mathf.Lerp(myAudio.volume, 0f, 0.3f);
            }
            else
            {
                myAudio.volume = Mathf.Lerp(myAudio.volume, 0.1f, 0.3f);
                if (!myAudio.isPlaying)
                {
                    myAudio.Play();
                }
            }

            velTarg.transform.rotation = Quaternion.Slerp(velTarg.transform.rotation, targetRotation, 0.4f);
        }

        //Set tiremarks
    }

    public void GetVel(Vector3 vel)
    {
        velo = vel;
    }

    public void GetDesVel(Vector3 desiredVelo)
    {
        desiredVel = desiredVelo;
    }

    public void IsTireMarking(bool tireMark)
    {
        onGround = tireMark;
    }
}
