
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectionImage_Moive_CF : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    MoiveController_CF _moiveController;
    void Start()
    {
        _moiveController = transform.parent.parent.GetComponent<MoiveController_CF>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _moiveController.SliderState(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _moiveController.SliderState(false);
    }
}
