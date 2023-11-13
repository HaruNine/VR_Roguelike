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
        // ������ ��Ʈ�ѷ� ���� Ʈ���� Ŭ�� ����
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // ���� Ʈ���� Ŭ�� �� ���� ����
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            // Ray�� �浹�� ���
            if (Physics.Raycast(ray, out hit))
            {
                // �浹�� ��ü�� �±װ� "Door"���� Ȯ��
                if (hit.collider.CompareTag("Door"))
                {
                    // Ŭ���� ���� �̸�
                    string clickedDoorName = hit.collider.gameObject.name;

                    // �̸��� ���� ���� ����
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
        // ���õ� ���� �̸� ���
        Debug.Log("Click to Selected Door: " + SG.AllDoors[i].Name + " | Tag = " + SG.AllDoors[i].Tag);
        SG.doorGenerator.DestroyAllDoors();
        
        Doors.MovePlayer(i);
        
        Doors.ChangeDoorLocation(SG.AllDoors, i);
    }
}
