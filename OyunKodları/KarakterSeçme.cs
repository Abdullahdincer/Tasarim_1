



using UnityEngine;
using UnityEngine.SceneManagement;

public class KodDosyası : MonoBehaviour
{
    private GameObject[] karakterlist; // Karakterlerin listesi
    public int index;
    public int indexitut;// Seçili karakterin indeksi
    private GameObject secilenkaraktr;
    void Start()
    {
        // Daha önce kaydedilmiş indeks varsa yükle, yoksa 0
        index = PlayerPrefs.GetInt("CharacterSelected", 0);


        // Tüm karakterleri listeye yükle
        karakterlist = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            karakterlist[i] = transform.GetChild(i).gameObject;

        }

        // Tüm karakterleri pasif yap, yalnızca seçili karakteri aktif yap

        for (int i = 0; i < transform.childCount; i++)
        {
            karakterlist[i].SetActive(false);

        }
        if (karakterlist.Length > 0)
        {
            karakterlist[index].SetActive(true);
            secilenkaraktr = karakterlist[index];
            secilenkaraktr.gameObject.name = "secilenkarakter";

        }

    }


    public void ToggleLeft()
    {
        karakterlist[index].SetActive(false);
        index = (index - 1 + karakterlist.Length) % karakterlist.Length;
        karakterlist[index].SetActive(true);
    }

    public void ToggleRight()
    {
        karakterlist[index].SetActive(false);
        index = (index + 1) % karakterlist.Length;
        karakterlist[index].SetActive(true);
    }

    public void ConfirmSelection()
    {
        // Seçili karakterin indeksini kaydet
        PlayerPrefs.SetInt("CharacterSelected", index);
        PlayerPrefs.Save();
        indexitut = index;

        SceneManager.LoadScene(1);



    }
}
