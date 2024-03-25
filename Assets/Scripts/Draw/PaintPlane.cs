using UnityEngine;
using UnityEngine.UI;
using YG;

public class PaintPlane : MonoBehaviour
{
    public static PaintPlane Instance;

    [Header("Initialize Texture")] 
    public Texture2D Texture;
    public Color[] _pictureColors;
    private int _currentPictureNumber;

    private Color[] _startPixelsColor;
    private float _scaleXCoef;

    [Header("Brush")]
    [SerializeField] private Brush _activeBrush;
    public Brush ActiveBrush { get { return _activeBrush; } set { _activeBrush = value; UpdateUI(); } }
    [SerializeField] private Image _currentBrushImage;

    [Header("Color")]
    private Color _brushColor;
    public Color BrushColor { get { return _brushColor; } set { _brushColor = value; UpdateUI(); } }
    public Color PaintZone;
    [SerializeField] private Image _currentColorIcon;

    [Header("New Texture Settings")]
    [SerializeField] private TextureWrapMode _wrapMode;
    [SerializeField] private FilterMode _filterMode;
    private int resolutionX;
    private int resolutionY;

    public Collider Collider { get { return _collider; } private set { } }
    private Collider _collider;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        BrushColor = Color.red;
        _collider = GetComponent<Collider>();
        UpdateUI();
    }

    private void Update()
    {
        if(Texture != null) ActiveBrush.Painting(BrushColor);

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F3))
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
    }

    #region OpenNewTexture
    public void OpenNewTexture(Texture2D texture, int pictureNumber) 
    {
        Texture = texture;
        _currentPictureNumber = pictureNumber;

        GetDataTexture();
        ManagePaintZones();
        CreateNewTexture();

        if (YandexGame.savesData.savesPreviously[pictureNumber])
            ImageConversion.LoadImage(Texture, YandexGame.savesData.pictures[pictureNumber]);
        else RefillTexture();
    }

    private void GetDataTexture()
    {
        resolutionX = Texture.width;
        resolutionY = Texture.height;

        _scaleXCoef = (float)Texture.width / (float)Texture.height;
        _startPixelsColor = Texture.GetPixels();
    }

    private void ManagePaintZones()
    {
        for (int i = 0; i < _startPixelsColor.Length; i++)
        {
            bool allowedColor = false;
            foreach (Color pictureColor in _pictureColors)
            {
                if (_startPixelsColor[i] == pictureColor) 
                    allowedColor = true;
            }

            if (!allowedColor) _startPixelsColor[i] = Color.black;
        }
    }

    private void CreateNewTexture()
    {
        Texture = new Texture2D(resolutionX, resolutionY);
        Texture.wrapMode = _wrapMode;
        Texture.filterMode = _filterMode;
        GetComponent<Renderer>().material.mainTexture = Texture;
        transform.localScale = new Vector3(transform.localScale.y * _scaleXCoef, transform.localScale.y, transform.localScale.z);
    }
    #endregion

    private void UpdateUI()
    {
        _currentColorIcon.color = _brushColor;
        _currentBrushImage.sprite = _activeBrush.Icon;
    }

    #region PublicFunc
    public void RefillTexture()
    {
        Texture.Reinitialize(resolutionX, resolutionY);

        for (int x = 0; x < resolutionX; x++)
        {
            for (int y = 0; y < resolutionY; y++)
            {
                if (_startPixelsColor[x + (y * resolutionX)] != Color.black) Texture.SetPixel(x, y, Color.white);
                else Texture.SetPixel(x, y, Color.black);
            }
        }
        Texture.Apply();
    }

    public Color GetStartColor(int coordX, int coordY)
    {
        if (coordX + (coordY * resolutionX) < _startPixelsColor.Length)
            return _startPixelsColor[coordX + (coordY * resolutionX)];
        else return Color.clear;
    }

    public void SetTexturePixel(int coordX, int coordY, Color color)
    {
        if (coordX + (coordY * resolutionX) < _startPixelsColor.Length)
            if (_startPixelsColor[coordX + (coordY * resolutionX)] == PaintZone)
                Texture.SetPixel(coordX, coordY, color);
    }

    public void SavePicture()
    {
        YandexGame.savesData.pictures[_currentPictureNumber] = ImageConversion.EncodeToJPG(Texture);
        YandexGame.savesData.savesPreviously[_currentPictureNumber] = true;
        YandexGame.SaveProgress();
        FindAnyObjectByType<SceneManager>().UpdatePicture(_currentPictureNumber);
    }
    #endregion
}
