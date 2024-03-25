using UnityEngine;

public class CircleBrush : Brush
{
    [SerializeField] private int _brushSize;

    protected override void Draw(Color color)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (PaintPlane.Instance.Collider.Raycast(ray, out hit, 100000))
            {
                int pointX = (int)(hit.textureCoord.x * PaintPlane.Instance.Texture.width);
                int pointY = (int)(hit.textureCoord.y * PaintPlane.Instance.Texture.height);

                //if (PaintPlane.Instance.GetStartColor(pointX, pointY) != Color.black 
                //    && PaintPlane.Instance.PaintZone == Color.clear) PaintPlane.Instance.PaintZone = PaintPlane.Instance.GetStartColor(pointX, pointY);

                for (int y = -_brushSize; y <= _brushSize; y++)
                {
                    for (int x = -_brushSize; x <= _brushSize; x++)
                    {
                        float sqrtX = Mathf.Pow(x, 2);
                        float sqrtY = Mathf.Pow(y, 2);
                        float sqrtRadius = Mathf.Pow(_brushSize - 0.5f, 2);

                        int coordX = pointX + x;
                        int coordY = pointY + y;

                        if (sqrtX + sqrtY < sqrtRadius)
                        {
                            if (coordX > 0 && coordX < (int)PaintPlane.Instance.Texture.width 
                                && coordY > 0 && coordY < (int)PaintPlane.Instance.Texture.height)
                                PaintPlane.Instance.SetTexturePixel(coordX, coordY, color);
                        }
                    }
                }
                PaintPlane.Instance.Texture.Apply();
            }
        }
    }
}
