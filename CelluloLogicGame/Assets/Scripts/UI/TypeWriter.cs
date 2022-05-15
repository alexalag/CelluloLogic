using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    public float delay = 0.01f;
    private string fullText;
    public string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        fullText = GetComponent<TextMeshProUGUI>().text;
        Constantes.writing = true;
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText() {
        for(int i = 0; i < fullText.Length; ++i) {
            if(!Constantes.writing) {
                i = fullText.Length -1;
            }
            currentText = fullText.Substring(0,i);
            GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Constantes.writing = false;
    }
}
