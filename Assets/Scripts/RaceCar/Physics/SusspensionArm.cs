using UnityEngine;

public class SusspensionArm : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float factor;

    private float baseOffset;

    void Start()
    {
        baseOffset = target.localPosition.y;
    }

    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, -(target.localPosition.y - baseOffset) * factor);
    }
}
