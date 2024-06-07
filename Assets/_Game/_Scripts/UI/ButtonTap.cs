using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTap : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDropHandler
{
    [SerializeField] private GameObject buttonSell;
    [SerializeField] private GameObject buttonTap;

    [SerializeField] private Camera mainCamera;

    private void Start() {
        Debug.Log("ButtonTap");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Khi người dùng nhấn vào ButtonTap, chuyển nó thành ButtonSell
        // buttonTap.SetActive(false);
        // buttonSell.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Khi người dùng thả ButtonTap, chuyển nó trở lại thành ButtonTap
        // buttonSell.SetActive(false);
        // buttonTap.SetActive(true);
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Khi một đối tượng được kéo vào ButtonSell, kiểm tra xem nó có phải là PointSpawn.Gun không
        PointSpawn pointSpawn = eventData.pointerDrag.GetComponent<PointSpawn>();
        if (pointSpawn != null && pointSpawn.Gun != null)
        {
            // Nếu đúng, xóa PointSpawn.Gun
            //Destroy(pointSpawn.Gun);
            Debug.Log("Destroy PointSpawn.Gun");
        }
        else {
            Debug.Log("Not PointSpawn.Gun");}
    }
}