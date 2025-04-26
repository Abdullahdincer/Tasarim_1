using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Level_3_Karakter_Kodu : MonoBehaviour
{
    private float moveAmount = 10f; // Her hareketin mesafesi
    private Transform karakterTransform; // Karakterin Transformu
    public TextMeshProUGUI mesaj;
    Level_3_zemin level_3_Zemin;
    private bool isMoving = false; // Hareket sýrasýnda baþka bir giriþe izin verilmez
    Vector3 gecici;
    int pervanekontrol = 0;
    public TextMeshProUGUI OyunPaneliMesaji;
    public GameObject panel;//panel türü obje cinsinden yazýldý
    public GameObject AdimSayisiGoster; //panel türü obje cinsinden yazýldý
    public TextMeshProUGUI AdimSayisiMesaji;
  Vector3  SonPozisyonuYakala;
    int AdimSayisi = 0;
    int TebrikMesajSayisi = 0;
    string panelmesajý = "Adým Sayýsý"; // Adým Sayýsý deðiþkenini burada saklayacam AdimSayisiMesajin içine yazmak için
    int kontrol = 1;
    public AudioSource[] sesler;
     GameObject secilenKarakter;
    Rigidbody2D rb;
    [System.Obsolete]
    void Start()
    {
        AdimSayisiMesaji.enableAutoSizing = true;
       panel.SetActive(false);
        level_3_Zemin = FindObjectOfType<Level_3_zemin>();

          
        secilenKarakter = GameObject.FindWithTag("Karakter");
        rb=secilenKarakter.GetComponent<Rigidbody2D>();
        karakterTransform = secilenKarakter.transform;
        if (secilenKarakter == null)
        {
            Debug.LogError("Seçilen karakter bulunamadý!");
            return;
        }
        else
        {
            
            karakterTransform.position = new Vector3(-45, -45f, 0); // Baþlangýç pozisyonunu kaydet
            karakterTransform.localScale = new Vector3(30, 25, 1);
            sesler = secilenKarakter.GetComponents<AudioSource>();
            sesler[1].enabled = false;
            SonPozisyonuYakala = secilenKarakter.transform.position;
            StartCoroutine(MesajVer());
        }
    }


    IEnumerator MesajVer()
    {
        yield return new WaitForSeconds(0.1f);
        mesaj.text = "Oyuna Hoþgeldiniz! Kýrmýzý kareye ulaþmaya çalýþýnýz.";
        mesaj.enableAutoSizing = true;
        mesaj.color = Color.red;
        yield return new WaitForSeconds(1.0f);
        mesaj.text = null;
    }
    IEnumerator YeniSahneBilgilendirme()
    {

        yield return new WaitForSeconds(1f);
        mesaj.text = null;
        panel.SetActive(true);
        panel.GetComponent<RectTransform>().position = new Vector3(-108, -470);
        OyunPaneliMesaji.enableAutoSizing = true;
        OyunPaneliMesaji.color = Color.red;
        OyunPaneliMesaji.text = "Level Completed";
        panel.GetComponent<Transform>().position = new Vector3(-18f, 7f);
        sesler[0].enabled = false;
    }

    void Update()
    {
        if (karakterTransform != null && pervanekontrol==0)
        {
            // Pozisyon sýnýrlarýný kontrol et
            Vector3 sinirKontrol = karakterTransform.position;
            sinirKontrol.x = Mathf.Clamp(sinirKontrol.x, -45, 45);
            sinirKontrol.y = Mathf.Clamp(sinirKontrol.y, -44.3f, 45.7f);
            karakterTransform.position = sinirKontrol;
            
            // Kýrmýzý kareye ulaþýldý mý kontrol et
             gecici = karakterTransform.position;
            gecici = new Vector3(Mathf.Round(karakterTransform.position.x), Mathf.Floor(karakterTransform.position.y), karakterTransform.position.z);
            if (gecici == level_3_Zemin.vector)
            {             
                //int SahneDeger = SceneManager.GetActiveScene().buildIndex;//buildIndex ile tam sayýya dönüþtürülmesini saðladým.
                if (TebrikMesajSayisi == 0)
                {
                    mesaj.enableAutoSizing = true;
                    mesaj.text = "Tebrikler Kýrmýzý Kareye ulaþtýnýz..";
                    mesaj.color = Color.green;
                    TebrikMesajSayisi++;
                }
                StartCoroutine(YeniSahneBilgilendirme());
                kontrol = 0;
               
            }
           
           
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("çarpýþma baþladý...." + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Duvar") || collision.gameObject.CompareTag("Duvar1"))
        {
            Debug.Log(collision.gameObject);
            karakterTransform.position = SonPozisyonuYakala; // Karakteri en baþa gönder
            Debug.Log(SonPozisyonuYakala);
            Debug.Log("duvara çarpmadan ilerle");

        }
        else if (collision.gameObject.CompareTag("Pervane"))
        {
            pervanekontrol++;
            secilenKarakter.GetComponent<Rigidbody2D>().freezeRotation = false;
            secilenKarakter.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            secilenKarakter.GetComponent<Rigidbody2D>().gravityScale = 5;
            panel.SetActive(true);
            panel.GetComponent<RectTransform>().position = new Vector3(-108, -470);
            OyunPaneliMesaji.enableAutoSizing = true;
            OyunPaneliMesaji.color = Color.red;
            OyunPaneliMesaji.text = "FAILED";
            panel.GetComponent<Transform>().position = new Vector3(-18f, 7f);
            Button[] butonlar = panel.GetComponentsInChildren<Button>();
            foreach (Button b in butonlar)
            {
                if (b.name == "BeforeLevel")
                {
                    b.interactable = false;
                }
            }
            sesler[0].enabled = false;
            sesler[1].enabled = true;
        }

    }

    // Coroutine ile hareket fonksiyonunu yavaþlatýyorum
    IEnumerator MoveCharacter(Vector3 direction)
    {
        if (kontrol != 0)
        {

       
        if (isMoving) yield break; // Eðer hareket halindeyse yeni hareket baþlatma
        isMoving = true;

            // Hareket öncesi pozisyonu kaydet
           
            Vector3 startPosition = karakterTransform.position;
    Vector2 targetPosition = rb.position + (Vector2)(direction * moveAmount);   //  Vector3 targetPosition = startPosition + direction * moveAmount;
        float elapsedTime = 0f;
        float duration = 0.25f; // Hareketin tamamlanma süresi

        while (elapsedTime < duration)
        {
rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, elapsedTime / duration)); // karakterTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
            yield return null; // Bir sonraki frame'e kadar bekle
        }

            rb.MovePosition(targetPosition); //karakterTransform.position = targetPosition; // Hedef pozisyona kesin olarak yerleþtir
            isMoving = false; // Hareket tamamlandý
        if (secilenKarakter.transform.position.x >= -45 && secilenKarakter.transform.position.x <= 45 && 
            secilenKarakter.transform.position.y >= -44.3f && secilenKarakter.transform.position.y <= 45.7f)
        {
       
            AdimSayisi++;
            AdimSayisiMesaji.text = "    " + panelmesajý + "     " + AdimSayisi;
        }
        }
    }
    public void TryLevelButon()
    {
        SceneManager.LoadScene(2);
    }
    public void BeforeLevelButon()
    {
        SceneManager.LoadScene(1);
    }
    public void MoveRight()
    {
        if (karakterTransform != null && !isMoving)
        {
            StartCoroutine(MoveCharacter(Vector3.right));
        }
    }

    public void MoveUp()
    {
        if (karakterTransform != null && !isMoving)
        {
            StartCoroutine(MoveCharacter(Vector3.up));
        }
    }

    public void MoveLeft()
    {
        if (karakterTransform != null && !isMoving)
        {
            StartCoroutine(MoveCharacter(Vector3.left));
        }
    }

    public void MoveDown()
    {
        if (karakterTransform != null && !isMoving)
        {
            StartCoroutine(MoveCharacter(Vector3.down));
        }
    }
}
