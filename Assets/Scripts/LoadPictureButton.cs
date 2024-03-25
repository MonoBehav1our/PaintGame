using UnityEngine;
using UnityEngine.UI;
using YG;


public class LoadPictureButton : MonoBehaviour
{
    private int _pictureNumber;
    private Texture2D _paintedTexture;
    private Texture2D _basePictureTexture;

    private Texture2D _cashTexture;

    private SceneManager _sceneManager;

    public void Init(Texture2D paintTexture, Texture2D visibleTexture, int number)
    {
        _paintedTexture = paintTexture;
        _basePictureTexture = visibleTexture;
        _pictureNumber = number;

        _cashTexture = new Texture2D(_basePictureTexture.width, _basePictureTexture.height);
        _cashTexture.SetPixels(_basePictureTexture.GetPixels());
        _cashTexture.Apply();
        GetComponent<RawImage>().texture = _cashTexture;

        transform.localScale = new Vector3(transform.localScale.y * ((float)_basePictureTexture.width / (float)_basePictureTexture.height), transform.localScale.y, transform.localScale.z);
        _sceneManager = FindAnyObjectByType<SceneManager>();
        UpdateTexture();
    }

    private void Update()
    {
        if (GetComponent<RectTransform>().position.y > Screen.height || GetComponent<RectTransform>().position.y < -300)
        {
            GetComponent<RawImage>().enabled = false;
        }
        else GetComponent<RawImage>().enabled = true;
    }

    public void LoadTexture()
    {
        YandexGame.FullscreenShow();
        PaintPlane.Instance.OpenNewTexture(_paintedTexture, _pictureNumber);
        _sceneManager.ChangeState(false);
    }

    public void UpdateTexture()
    {
        if (YandexGame.savesData.savesPreviously[_pictureNumber] ) ImageConversion.LoadImage(_cashTexture, YandexGame.savesData.pictures[_pictureNumber]);
    }
}
