using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KarakterKodu : MonoBehaviour
{
    private float moveAmount = 10f; // Her hareketin mesafesi
    private Transform karakterTransform; // Karakterin Transformu
    public TextMeshProUGUI mesajim;
    private GameObject Karakter;
    OyunZemini oyunZemini;
    public TextMeshProUGUI OyunPaneliMesaji;
    public GameObject panel;
    int TebrikMesajSayisi = 0;
    private bool isMoving = false; // Hareket s�ras�nda ba�ka bir giri�e izin verilmez
    int kontrol = 1; //oyunu ge�tikten sonra butonlar� pasifle�tirmek i�in tan�mland�
    public GameObject AdimSayisiGoster; //panel t�r� obje cinsinden yaz�ld�
    public TextMeshProUGUI AdimSayisiMesaji;
    int AdimSayisi = 0;
    string panelmesaj� = "Ad�m Say�s�";
    AudioSource ses;
    int selectedIndex;
    [System.Obsolete]
    void Start()
    {
        AdimSayisiMesaji.enableAutoSizing = true;
        panel.SetActive(false);
        oyunZemini = FindObjectOfType<OyunZemini>();


      
        StartCoroutine(GiveMessage());
        Karakter = GameObject.FindWithTag("Karakter");

        if (Karakter == null)
        {
            Debug.Log("Karakter bulunamad�!");
        }
        else
        {
            ses = Karakter.GetComponent<AudioSource>();
            ses.loop = true;

            karakterTransform = Karakter.transform;

            // Karakterin ba�lang�� pozisyonu ve boyutu
            karakterTransform.position = new Vector3(-45, -44.3f, 0);
            karakterTransform.localScale = new Vector3(30, 25, 1);
        }

    }
    IEnumerator GiveMessage()
    {
        mesajim.text = "K�rm�z� kareye ula�maya �al���n�z";
        mesajim.enableAutoSizing = true;
        mesajim.color = Color.red;
        yield return new WaitForSeconds(1.5f);
        mesajim.text = null;
    }

    IEnumerator YeniSahneBilgilendirme()
    {

        yield return new WaitForSeconds(1.5f);
        mesajim.text = null;
        panel.SetActive(true);
        panel.GetComponent<RectTransform>().position = new Vector3(-108, -470);
        OyunPaneliMesaji.enableAutoSizing = true;
        OyunPaneliMesaji.color = Color.red;
        OyunPaneliMesaji.text = "Level Completed";
        panel.GetComponent<Transform>().position = new Vector3(-18f, 7f);

    }

    void Update()
    {
        // Pozisyon s�n�rlar�n� kontrol et
        if (karakterTransform != null)
        {
            Vector3 sinirKontrol = karakterTransform.position;
            sinirKontrol.x = Mathf.Clamp(sinirKontrol.x, -45, 45);
            sinirKontrol.y = Mathf.Clamp(sinirKontrol.y, -44.3f, 45.7f);

            karakterTransform.position = sinirKontrol;

        }
        karakterTransform.position = new Vector3(Mathf.Round(karakterTransform.position.x), Mathf.Floor(karakterTransform.position.y), karakterTransform.position.z);
        if (karakterTransform.position == oyunZemini.vector)
        {
            if (TebrikMesajSayisi == 0)
            {
                mesajim.enableAutoSizing = true;
                mesajim.text = "Tebrikler K�rm�z� Kareye ula�t�n�z..";
                mesajim.color = Color.green;
                TebrikMesajSayisi++;
            }
            //int SahneDeger = SceneManager.GetActiveScene().buildIndex;//buildIndex ile tam say�ya d�n��t�r�lmesini sa�lad�m.
            StartCoroutine(YeniSahneBilgilendirme());

            kontrol = 0;
            ses.enabled = false; 
        }
    }
    public void NextLevelButon()
    {

        SceneManager.LoadScene(3);

    }
    public void TryLevelButon()
    {

        SceneManager.LoadScene(2);

    }
    public void BeforeLevelButon()
    {

        SceneManager.LoadScene(1);

    } 
    IEnumerator MoveCharacter(Vector3 direction)
    {
        if (kontrol != 0) { 
        if (isMoving) yield break; // E�er hareket halindeyse yeni hareket ba�latma
        isMoving = true;

        // Hareket �ncesi pozisyonu kaydet
        

        Vector3 startPosition = karakterTransform.position;
        Vector3 targetPosition = startPosition + direction * moveAmount;
        float elapsedTime = 0f;
        float duration = 0.25f; // Hareketin tamamlanma s�resi

        while (elapsedTime < duration)
        {
            karakterTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Bir sonraki frame'e kadar bekle
        }

        karakterTransform.position = targetPosition; // Hedef pozisyona kesin olarak yerle�tir
        isMoving = false; // Hareket tamamland�
            if (Karakter.transform.position.x >= -46 && Karakter.transform.position.x <= 46 &&
             Karakter.transform.position.y >= -45.3f && Karakter.transform.position.y <= 46.7f)
            {

                AdimSayisi++;
                AdimSayisiMesaji.text = "    " + panelmesaj� + "     " + AdimSayisi;
            }
        }
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
