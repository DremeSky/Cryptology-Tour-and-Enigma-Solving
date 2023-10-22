using UnityEngine;
using TMPro ;
using UnityEngine.EventSystems ;

public class ChooseLabelController : MonoBehaviour ,IPointerClickHandler ,IPointerEnterHandler ,IPointerExitHandler
{
    public Color defaultColor ;
    public Color hoverColor ;
    private StoryScene scene ;
    private TextMeshProUGUI textMesh ;
    private ChooseController controller ;

    void Awake()
    {
        textMesh =GetComponent<TextMeshProUGUI>() ;
        textMesh.color =defaultColor ;
    }

    public float GetHeight()
    {
        return textMesh.rectTransform.sizeDelta.y *textMesh.rectTransform.localScale.y ;
    }

    public void Setup(ChooseScene.ChooseLabel label ,ChooseController controller ,float y)
    {
        scene =label.nextScene ;
        textMesh.text =label.text ;
        this.controller =controller ;

        Vector3 position =textMesh.rectTransform.localPosition ;
        position.y =y ;
        textMesh.rectTransform.localPosition =position ;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.return_ChooseText(textMesh.text);
        controller.PerformChoose(scene) ;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.color =hoverColor ;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.color =defaultColor ;
    }
}
