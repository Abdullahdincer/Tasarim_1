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
            Debug.Log("Karakter bulunamadý!");
        }
        else
        {
            
          
            // Karakterin baþlangýç pozisyonu ve boyutunu ayarladým
            karakterTransform.position = new Vector3(-45, -44.3f, 0);
            karakterTransform.localScale = new Vector3(40, 35, 1);
        }
       
    }
    IEnumerator GiveMessage()
    {
        
        mesajim.text = "Oyuna Hoþgeldiniz";
        mesajim.enableAutoSizing = true;
        mesajim.color = Color.red;

        yield return new WaitForSeconds(1.5f);
        
        mesajim.enableAutoSizing = true;
        mesajim.color = Color.red;
        mesajim.text = "Söylenilen Adýmlarý Takip Ediniz";
        yield return new WaitForSeconds(1.5f);

        mesajim.enableAutoSizing = true;
        mesajim.color = Color.red;
        mesajim.text = "Kýrmýzý Kareye ulaþmaya çalýþýnýz";
        yield return new WaitForSeconds(1.5f);

        mesajim.enableAutoSizing = true;
        mesajim.color = Color.red;
        mesajim.text = "5 Birim Saða Ýlerleyiniz";
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
        // Pozisyon sýnýrlarýný kontrol et
        if (karakterTransform != null)
        {
            Vector3 sinirKontrol = karakterTransform.position;
            sinirKontrol.x = Mathf.Clamp(sinirKontrol.x, -45, 45);
            sinirKontrol.y = Mathf.Clamp(sinirKontrol.y, -44.3f, 45.7f);

            karakterTransform.position = sinirKontrol;
        }
        int SahneDeger=SceneManager.GetActiveScene().buildIndex;//buildIndex ile tam sayýya dönüþtürülmesini saðladým.
        if (karakterTransform.position == new Vector3(-15, 5.7f))
        {
            StartCoroutine(YeniSahneBilgilendirme());
           
        }
       
    }


    // Sað buton fonksiyonu
    public void MoveRight()
    {
        if (karakterTransform != null)
        {
            if(mesajim.text== "5 Birim Saða Ýlerleyiniz")
            karakterTransform.position += Vector3.right * moveAmount; // X ekseninde pozitif yönde hareket
       
        }
       if(karakterTransform.position == new Vector3(5,-44.3f))
        {

            mesajim.text = "Þimdi de 5 birim yukarýya ilerleyiniz";
          
        }
        

    }
    public void MoveUp()
    {
        if (karakterTransform != null)
        {
            if (mesajim.text == "Þimdi de 5 birim yukarýya ilerleyiniz")
                karakterTransform.position += Vector3.up * moveAmount; // Y ekseninde pozitif yönde hareket
        }
        if (karakterTransform.position == new Vector3(5,5.7f)) 
        {
            mesajim.text= "Þimdi de 2 birim sola ilerleyiniz ";
         
        }
       

    }

    // Sol buton fonksiyonu
    public void MoveLeft()
    {
        if (karakterTransform != null)
        {
            if (mesajim.text == "Þimdi de 2 birim sola ilerleyiniz ")
                karakterTransform.position += Vector3.left * moveAmount; // X ekseninde negatif yönde hareket
        }
        if (karakterTransform.position == new Vector3(-15, 5.7f)) 
        {
            mesajim.color = Color.green;
            mesajim.text = "Tebrikler Kýrmýzý Kareye Ulaþtýnýz";
        }
    }

    // Yukarý buton fonksiyonu
   

    // Aþaðý buton fonksiyonu
    public void MoveDown()
    {
        if (karakterTransform != null)
        {
            if (mesajim.text == null && 0==1)
                karakterTransform.position += Vector3.down * moveAmount; // Y ekseninde negatif yönde hareket
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
