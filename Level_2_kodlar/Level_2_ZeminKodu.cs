using UnityEngine;
public class OyunZemini : MonoBehaviour
{
    public GameObject tilePrefab; // Karelerin prefab'�
    public int rows = 10;         // Sat�r say�s�
    public int columns = 10;      // S�tun say�s�
    public Color color1 = Color.white;   // Birinci renk (beyaz)
    public Color color2 = Color.black;   // �kinci renk (kahverengi)
    public float tileSize = 10f;   // Her bir karenin boyutu
    int sayi;
    public Vector3 vector;

    void Start()
    {
        sayi = Random.Range(40, 81);
        // sayi = Random.Range(0, 81);

        Debug.Log(sayi);
        CreateChessBoard();
       
    }

    void CreateChessBoard()
    {
       
        // Ba�lang�� pozisyonu
        Vector2 startPos = new Vector2(-(columns / 2f) * tileSize + tileSize / 2, -(rows / 2f) * tileSize + tileSize / 2);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Prefab'� olu�tur
                Vector2 tilePosition = new Vector2(startPos.x + col * tileSize, startPos.y + row * tileSize);
                GameObject newTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);

                // Kareye renk atama
                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>();
                if ((row + col) % 2 == 0)
                {
                    renderer.color = color1; // Beyaza


                }
                else
                {
                    renderer.color = color2; // siyah
                }

                if ((row * 10 + col) == sayi)
                {
                    renderer.color = Color.red;
                    vector=newTile.transform.position;
                    Debug.Log(newTile.transform.position);
                    Debug.Log(vector);
                   
                    
                }
             

                // Karelerin boyutunu ayarla
                newTile.transform.localScale = new Vector3(tileSize, tileSize, 1);

                // Karelerin ismini d�zenleme (Debug i�in)
                newTile.name = $"Tile_{row}_{col}";
            }
        }
    }
}

