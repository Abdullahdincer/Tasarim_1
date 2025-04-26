using Unity.VisualScripting;
using UnityEngine;
public class Level_3_zemin : MonoBehaviour
{
    public GameObject tilePrefab; // Karelerin prefab'ý
    public int rows = 10;         // Satýr sayýsý
    public int columns = 10;      // Sütun sayýsý
    public Color color1 = Color.white;   // Birinci renk (beyaz)
    public Color color2 = Color.black;   // Ýkinci renk (kahverengi)
    public float tileSize = 10f;   // Her bir karenin boyutu
    int sayi;
    public Vector3 vector;
   public GameObject Duvar;
  public  GameObject Duvar1;
public    GameObject Pervane;

    void Start()
    {
   
        sayi = Random.Range(40, 81);
        // sayi = Random.Range(0, 81);

        Debug.Log(sayi);
        CreateChessBoard();

    }

    void CreateChessBoard()
    {
       
        Duvar.GetComponent<Transform>().localScale = new Vector3(40, 60f,1);
        Duvar1.GetComponent<Transform>().localScale = new Vector3(40, 60f,1);
        Pervane.GetComponent<Transform>().localScale = new Vector3(27f, 24.3f,1);
        // Baþlangýç pozisyonu
        Vector2 startPos = new Vector2(-(columns / 2f) * tileSize + tileSize / 2, -(rows / 2f) * tileSize + tileSize / 2);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Prefab'ý oluþtur
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
                    vector = newTile.transform.position;
                 
                    Debug.Log(vector);


                }
                int yersatir2 = row + 3;
                int yersutun2 = col +2;
                if (yersatir2 * 10 + col == sayi)
                {
                    Pervane.GetComponent<Transform>().position=newTile.transform.position;

                }
                int yersatýr1 = row - 1;
                int yersutun1 = col - 1;
                if( yersatýr1*10+yersutun1==sayi)
                {
                    Duvar1.transform.position=newTile.transform.position;

                }
                int yersatir = row +1;
                int yersutun=col +1;
                if (yersatir*10+yersutun==sayi)
                {
                    Duvar.transform.position = newTile.transform.position;

                }


                // Karelerin boyutunu ayarla
                newTile.transform.localScale = new Vector3(tileSize, tileSize, 1);

                // Karelerin ismini düzenleme (Debug için)
                newTile.name = $"Tile_{row}_{col}";
            }
        }
    }
}

