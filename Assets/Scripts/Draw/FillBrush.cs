using UnityEngine;

public class FillBrush : Brush
{
    protected override void Draw(Color color)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (PaintPlane.Instance.Collider.Raycast(ray, out hit, 100000))
            {
                int pointX = (int)(hit.textureCoord.x * PaintPlane.Instance.Texture.width);
                int pointY = (int)(hit.textureCoord.y * PaintPlane.Instance.Texture.height);

                //if (PaintPlane.Instance.GetStartColor(pointX, pointY) != Color.black
                //    && PaintPlane.Instance.PaintZone == Color.clear) PaintPlane.Instance.PaintZone = PaintPlane.Instance.GetStartColor(pointX, pointY);

                for (int y = 0; y < PaintPlane.Instance.Texture.height; y++)
                {
                    for (int x = 0; x < PaintPlane.Instance.Texture.width; x++)
                    {
                        PaintPlane.Instance.SetTexturePixel(x, y, color);
                    }
                }
                PaintPlane.Instance.Texture.Apply();
            }
        }
    }
}
