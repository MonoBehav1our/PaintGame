using UnityEngine;

public abstract class Brush : MonoBehaviour
{
    public Sprite Icon;

    public void Painting(Color color)
    {
        SetPaintZone();
        Draw(color);
    }

    protected abstract void Draw(Color color);

    private void SetPaintZone()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (PaintPlane.Instance.Collider.Raycast(ray, out hit, 100000))
            {
                int pointX = (int)(hit.textureCoord.x * PaintPlane.Instance.Texture.width);
                int pointY = (int)(hit.textureCoord.y * PaintPlane.Instance.Texture.height);

                if (PaintPlane.Instance.GetStartColor(pointX, pointY) != Color.black
                    && PaintPlane.Instance.PaintZone == Color.clear) PaintPlane.Instance.PaintZone = PaintPlane.Instance.GetStartColor(pointX, pointY);
            }
            }
        if (Input.GetKeyUp(KeyCode.Mouse0)) PaintPlane.Instance.PaintZone = Color.clear;
    }
}
