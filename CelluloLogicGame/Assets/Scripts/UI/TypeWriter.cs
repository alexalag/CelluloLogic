using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    private string fullText;
    public string currentText = "";
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        fullText = GetComponent<TextMeshProUGUI>().text;
        Constantes.writing = true;
        StartCoroutine(ShowText());
    }

    // Affiche les lettres du texte une à une avec le son du typing
    IEnumerator ShowText() {
        for(int i = 0; i < fullText.Length; ++i) {
            if(!audioSource.isPlaying) {
                audioSource.Play();
            }
            if(!Constantes.writing) {
                i = fullText.Length -1;
            }
            currentText = fullText.Substring(0,i);
            GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(Constantes.delayTyping);
        }
        Constantes.writing = false;
    }
}
