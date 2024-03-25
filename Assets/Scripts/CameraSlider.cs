using UnityEngine;
using UnityEngine.UI;

public class CameraSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Update() => Camera.main.orthographicSize = _slider.value;
}
