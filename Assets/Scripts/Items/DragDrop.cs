using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    [SerializeField] private LayerMask draggableLayer;

    private Camera cam;
    private RaycastHit2D hit;
    private Vector3 position;
    private Vector3 mousePosition;
    Transform focus;
    bool isDrag;
    Bounds curentRoomBounds;

    private void Start()
    {
        cam = Camera.main;
        isDrag = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Input.mousePosition));

            if (hit.collider != null && (draggableLayer.value & (1 << hit.collider.gameObject.layer)) > 0)
            {
                focus = hit.transform;
                isDrag = true;
                curentRoomBounds = Globals.RoomBounds;
            }
        }
        else if (isDrag && Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }
        else if (isDrag)
        {
            // This is to avoid dragging objects after they are dragged a certain distance
            EnableColliderOnDrag enableColliderOnDrag = focus.gameObject.GetComponent<EnableColliderOnDrag>();
            if (enableColliderOnDrag != null && enableColliderOnDrag.RbEnabled)
            {
                return;
            }

            mousePosition = Input.mousePosition;
            mousePosition.z = -cam.transform.position.z;
            position = cam.ScreenToWorldPoint(mousePosition);

            // Avoid that the object can be dragged outside of the current room
            if (position.x < curentRoomBounds.min.x)
            {
                position.x = curentRoomBounds.min.x;
            }
            else if (position.x > curentRoomBounds.max.x)
            {
                position.x = curentRoomBounds.max.x;
            }

            focus.position = new Vector3(position.x, focus.position.y, focus.position.z);
        }
    }
}
