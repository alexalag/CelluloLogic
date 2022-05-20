using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    private string fullText;
    private string currentText = "";
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        fullText = GetComponent<TextMeshProUGUI>().text;
        ConstantsUI.writing = true;
        StartCoroutine(ShowText());
    }

    // Affiche les lettres du texte une à une avec le son du typing
    IEnumerator ShowText() {
        for(int i = 0; i < fullText.Length; ++i) {
            if(!audioSource.isPlaying) {
                audioSource.Play();
            }
            if(!ConstantsUI.writing) {
                i = fullText.Length -1;
            }
            currentText = fullText.Substring(0,i);
            GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(ConstantsUI.delayTyping);
        }
        ConstantsUI.writing = false;
    }
}
