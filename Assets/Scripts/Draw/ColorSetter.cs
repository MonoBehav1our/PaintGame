using UnityEngine;
using UnityEngine.UI;

public class ColorSetter : MonoBehaviour
{
    [SerializeField] private Color _color;

    private void Start()
    {
        if (GetComponent<Image>())
            _color = GetComponent<Image>().color;
    }

    public void SetColor() => PaintPlane.Instance.BrushColor = _color;
}
