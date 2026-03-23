using UnityEngine;
using UnityEngine.EventSystems;

public class RouletteWheel : MonoBehaviour, IDropHandler
{
    DragDrop dragDrop;
    public Animator anim;
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            if (dragDrop.isChip)
            {
                Destroy(dragDrop.gameObject);
                RotateWheel();
            }
        }
    }

    private void RotateWheel()
    {
        int redOrBlack = Random.Range(0, 2);
        anim.SetTrigger("spin");
        if(redOrBlack == 0)
        {

        }
    }
}
