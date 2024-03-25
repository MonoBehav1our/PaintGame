using UnityEngine;
using UnityEngine.UI;

public class BrushSetter : MonoBehaviour
{
    [SerializeField] private Brush brush;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = brush.Icon;
    }

    public void SetBrush()
    {
        PaintPlane.Instance.ActiveBrush = brush;
    }
}
