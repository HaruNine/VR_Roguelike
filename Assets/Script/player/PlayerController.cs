using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Start_game SG;
    public Doors Doors;
    
    public DoorName[] AllDoors;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 오른쪽 컨트롤러 검지 트리거 클릭 감지
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // 검지 트리거 클릭 시 동작 수행
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            // Ray와 충돌한 경우
            if (Physics.Raycast(ray, out hit))
            {
                // 충돌한 객체의 태그가 "Door"인지 확인
                if (hit.collider.CompareTag("Door"))
                {
                    // 클릭한 문의 이름
                    string clickedDoorName = hit.collider.gameObject.name;

                    // 이름에 따라 동작 구분
                    if (clickedDoorName == SG.AllDoors[0].Name)
                    {
                        selectDoor(0);
                    }
                    else if (clickedDoorName == SG.AllDoors[1].Name)
                    {
                        selectDoor(1);
                    }
                }
            }
        }
    }


    void selectDoor(int i)
    {
        // 선택된 문의 이름 출력
        Debug.Log("Click to Selected Door: " + SG.AllDoors[i].Name + " | Tag = " + SG.AllDoors[i].Tag);
        SG.doorGenerator.DestroyAllDoors();
        
        Doors.MovePlayer(i);
        
        Doors.ChangeDoorLocation(SG.AllDoors, i);
    }
}
