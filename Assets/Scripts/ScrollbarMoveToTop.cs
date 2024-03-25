using UnityEngine.UI;
using UnityEngine;

public class ScrollbarMoveToTop : MonoBehaviour
{
    private Scrollbar scrollbar;

    private void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.value = 1;
    }
}
