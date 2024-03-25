using UnityEngine;
using UnityEngine.UI;
using YG;

public class InterfaceColor : MonoBehaviour
{
    [SerializeField] private GameObject[] _recolorElements;
    [SerializeField] private Image[] _referenceColors;
    [SerializeField] private RectTransform _birdMark;

    private int currentNumberColor = 0;
    private Color currentColor;

    private void Start()
    {
        currentNumberColor = YandexGame.savesData.interfaceColor;
        currentColor = _referenceColors[currentNumberColor].color;
        _birdMark.position = _referenceColors[currentNumberColor].GetComponent<RectTransform>().position;

        Set();
    }

    public void SetInterfaceColor(Image callerImage)
    {
        currentColor = callerImage.color;
        _birdMark.position = callerImage.GetComponent<RectTransform>().position;

        Set();

        for(int i = 0; i < _referenceColors.Length; i++)
            if (_referenceColors[i].color == currentColor) currentNumberColor = i;
        print(currentNumberColor);

        YandexGame.savesData.interfaceColor = currentNumberColor;
        YandexGame.SaveProgress();
    }

    private void Set()
    {
        foreach (GameObject gameObject in _recolorElements)
        {
            if (gameObject.GetComponent<Image>())
                gameObject.GetComponent<Image>().color = currentColor;

            if (gameObject.GetComponent<SpriteRenderer>())
                gameObject.GetComponent<SpriteRenderer>().color = currentColor;
        }
    }
}
