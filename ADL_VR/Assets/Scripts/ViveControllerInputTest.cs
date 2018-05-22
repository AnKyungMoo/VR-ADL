using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInputTest : MonoBehaviour {
    // 추적할 오브젝트의 레퍼런스를 선언, 컨트롤러에 쉽게 접근학 우해 추적한 오브젝트으 인덱스를 사용하여 컨트롤러에 입력전다
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // 스크립트가 실행되면 컨트롤러에 삽입되어 있는 스팀VR 오브젝트컴포넌트의 레퍼런스를 TRACKEDOBJECT로 전달
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    //컨트롤러 움직임 알아보기
    void Update()
    {
        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }
        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + "Trigger Press");
        }
        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + "Trigger Release");
        }
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + "Grip Press");
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + "Grip Release");
        }
    }
}

