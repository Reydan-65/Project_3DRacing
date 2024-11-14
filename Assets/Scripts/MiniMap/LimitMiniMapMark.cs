using UnityEngine;

public class LimitMiniMapMark : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        rectTransform.rotation = Quaternion.Euler(90,0,0);
    }
}