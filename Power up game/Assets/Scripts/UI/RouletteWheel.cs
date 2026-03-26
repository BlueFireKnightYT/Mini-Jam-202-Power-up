using UnityEngine;
using UnityEngine.EventSystems;

public class RouletteWheel : MonoBehaviour, IDropHandler
{
    DragDrop dragDrop;
    public GameObject choosePanel;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            if (dragDrop.isChip)
            {
                Destroy(dragDrop.gameObject);
                choosePanel.SetActive(true);
            }
        }
    }

    public void ClickRed()
    {
        RotateWheel(1);
        choosePanel.SetActive(false);
    }

    public void ClickBlack()
    {
        RotateWheel(2);
        choosePanel.SetActive(false);
    }

    private void RotateWheel(int color)
    {
        int redOrBlack = Random.Range(0, 2);
        anim.SetTrigger("spin");
        anim.SetBool("red", false);
        anim.SetBool("black", false);
        if (redOrBlack == 0)
        {
            anim.SetBool("red", true);
            if(color == 1)
            {
                print("win");
            }
            else
            {
                print("lose");
            }
        }
        if(redOrBlack == 1)
        {
            anim.SetBool("black", true);
            if (color == 2)
            {
                print("win");
            }
            else
            {
                print("lose");
            }
        }
    }
}
