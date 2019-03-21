using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    protected Transform playerTransform;

    protected Vector3 destPos;

    //list of patrolling points
    protected GameObject[] pointList;

    protected virtual void Initialize() { }

    protected virtual void FSMUpdate() { }

    protected virtual void FSMFixedUpdate() { }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        FSMUpdate();
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
