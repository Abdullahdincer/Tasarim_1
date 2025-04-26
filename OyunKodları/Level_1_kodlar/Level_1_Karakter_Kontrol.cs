using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterController2D : MonoBehaviour
{
     GameObject secilenKarakter;
    public float moveAmount; // Her hareketin mesafesi
    private Transform karakterTransform; // Karakterin Transformu
    public TextMeshProUGUI mesajim;
    public TextMeshProUGUI OyunPaneliMesaji;
    public GameObject panel;
    AudioSource ses;

    void Start()
    {
        panel.SetActive(false); 
       ses = GetComponent<AudioSource>();
        secilenKarakter = GameObject.FindWithTag("Karakter");
        karakterTransform = secilenKarakter.transform; 
        StartCoroutine(GiveMessage());
        // Karakteri etiketiyle bul
        if (secilenKarakter == null)
        {
            Debug.Log("Karakter bulunamad�!");
        }
        else
        {
            
          
            // Karakterin ba�lang�� pozisyonu ve boyutunu ayarlad�m
            karakterTransform.position = new Vector3(-45, -44.3f, 0);
            karakterTransform.localScale = new Vector3(40, 35, 1);
        }
       
    }
    IEnumerator GiveMessage()
    {
        
        mesajim.text = "Oyuna Ho�geldiniz";
        mesajim.enableAutoSizing = true;
        mesajim.color = Color.red;

        yield return new WaitForSeconds(1.5f);
        
        mesajim.enableAutoSizing = true;
        mesajim.color = Color.red;
        mesajim.text = "S�ylenilen Ad�mlar� Takip Ediniz";
        yield return new WaitForSeconds(1.5f);

        mesajim.enableAutoSizing = true;
        mesajim.color = Color.red;
        mesajim.text = "K�rm�z� Kareye ula�maya �al���n�z";
        yield return new WaitForSeconds(1.5f);

        mesajim.enableAutoSizing = true;
        mesajim.color = Color.red;
        mesajim.text = "5 Birim Sa�a �lerleyiniz";
        yield return new WaitForSeconds(1.5f);

        
    }
    IEnumerator YeniSahneBilgilendirme()
    {

        yield return new WaitForSeconds(1f);
        mesajim.text = null;
        panel.SetActive(true);
        panel.GetComponent<RectTransform>().position = new Vector3(-108, -470);
      
        OyunPaneliMesaji.enableAutoSizing = true;
        OyunPaneliMesaji.color = Color.red;
        OyunPaneliMesaji.text= "Level Completed";
        panel.GetComponent<Transform>().position = new Vector3(-18f,7f);
        ses.enabled = false;
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
        int SahneDeger=SceneManager.GetActiveScene().buildIndex;//buildIndex ile tam say�ya d�n��t�r�lmesini sa�lad�m.
        if (karakterTransform.position == new Vector3(-15, 5.7f))
        {
            StartCoroutine(YeniSahneBilgilendirme());
           
        }
       
    }


    // Sa� buton fonksiyonu
    public void MoveRight()
    {
        if (karakterTransform != null)
        {
            if(mesajim.text== "5 Birim Sa�a �lerleyiniz")
            karakterTransform.position += Vector3.right * moveAmount; // X ekseninde pozitif y�nde hareket
       
        }
       if(karakterTransform.position == new Vector3(5,-44.3f))
        {

            mesajim.text = "�imdi de 5 birim yukar�ya ilerleyiniz";
          
        }
        

    }
    public void MoveUp()
    {
        if (karakterTransform != null)
        {
            if (mesajim.text == "�imdi de 5 birim yukar�ya ilerleyiniz")
                karakterTransform.position += Vector3.up * moveAmount; // Y ekseninde pozitif y�nde hareket
        }
        if (karakterTransform.position == new Vector3(5,5.7f)) 
        {
            mesajim.text= "�imdi de 2 birim sola ilerleyiniz ";
         
        }
       

    }

    // Sol buton fonksiyonu
    public void MoveLeft()
    {
        if (karakterTransform != null)
        {
            if (mesajim.text == "�imdi de 2 birim sola ilerleyiniz ")
                karakterTransform.position += Vector3.left * moveAmount; // X ekseninde negatif y�nde hareket
        }
        if (karakterTransform.position == new Vector3(-15, 5.7f)) 
        {
            mesajim.color = Color.green;
            mesajim.text = "Tebrikler K�rm�z� Kareye Ula�t�n�z";
        }
    }

    // Yukar� buton fonksiyonu
   

    // A�a�� buton fonksiyonu
    public void MoveDown()
    {
        if (karakterTransform != null)
        {
            if (mesajim.text == null && 0==1)
                karakterTransform.position += Vector3.down * moveAmount; // Y ekseninde negatif y�nde hareket
        }
    }
    public void NextLevel()
    {

        SceneManager.LoadScene(2);

    }
    public void TryAgain()
    {

        SceneManager.LoadScene(1);

    }


}
