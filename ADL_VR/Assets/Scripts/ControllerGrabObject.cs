using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // 스크립트가 실행되면 컨트롤러에 삽입되어 있는 스팀VR 오브젝트컴포넌트의 레퍼런스를 TRACKEDOBJECT로 전달
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // 콜라이더가 부착된 게임오브젝트를 사용하여 물체를 잡거나 놓아주기 위한 목적으로 사용
    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }

    // 콜라이더에 진입했을떄 다른 콜라이더를 움켜쥘수 있게 잠재적 타겟으로 설정
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    //두개의 콜라이더가 겹쳐진 상태로 유지되었을때, Set~호출 
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    //콜라이더가 트리거 콜라이더로부터 떨어져있다면 콜딩오브젝트가 아닌것으로 설정
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }

    // 플레이어 손에 게임오브젝트를 옮기고, 콜딩오브젝트는 초기화
    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }
    //현재 접촉한 오브젝트를 컨트롤러에 연결
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    // 손에 쥐고있던 오브젝트 놓기
    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        objectInHand = null;
    }

    // Update is called once per frame
    void Update () {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
		
	}
}

