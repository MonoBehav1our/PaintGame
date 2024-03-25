using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _paintCanvas;
    [SerializeField] private GameObject[] _brushes;

    [SerializeField] private LoadPictureButton[] _loadPictureButtons;
    [SerializeField] private Texture2D[] _paintTextures;
    [SerializeField] private Texture2D[] _visibleTextures;

    private void Start()
    {
        for (int i = 0; i < _loadPictureButtons.Length; i++)
        {
            _loadPictureButtons[i].Init(_paintTextures[i], _visibleTextures[i], i);
        }
    }

    public void ChangeState(bool inMainMenu)
    {
        _menuCanvas.SetActive(inMainMenu);
        _paintCanvas.SetActive(!inMainMenu);
        foreach (GameObject brush in _brushes) 
        {
            brush.SetActive(!inMainMenu);
        }
    }

    public void UpdatePicture(int pictureNumber)
    {
        _loadPictureButtons[pictureNumber].UpdateTexture();
    }
}
