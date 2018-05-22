using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // 스크립트가 실행되면 컨트롤러에 삽입되어 있는 스팀VR 오브젝트컴포넌트의 레퍼런스를 TRACKEDOBJECT로 전달
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // 콜라이더에 진입했을떄 다른 콜라이더를 움켜쥘수 있게 잠재적 타겟으로 설정
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other);
        collidingObject = other.gameObject;
    }

    //콜라이더가 트리거 콜라이더로부터 떨어져있다면 콜딩오브젝트가 아닌것으로 설정
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }
        Debug.Log("null!");
        collidingObject = null;
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject.name.Equals("ToiletButton"))
            {
                SceneManager.LoadScene("Toilet");
            }
            else if (collidingObject.name.Equals("RoomButton"))
            {
                SceneManager.LoadScene("Room");
            }
        }
	}
}
