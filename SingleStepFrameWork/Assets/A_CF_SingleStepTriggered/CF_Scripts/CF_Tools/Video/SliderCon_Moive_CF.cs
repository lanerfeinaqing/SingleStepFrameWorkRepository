
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderCon_Moive_CF : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    MoiveController_CF _moiveController;
    void Start()
    {
        _moiveController = transform.parent.parent.parent.GetComponent<MoiveController_CF>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _moiveController.isSelfCon = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _moiveController.isSelfCon = true;
    }
}
