using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrdinateurTexteInput : MonoBehaviour
{
    public static bool texteSupp;
    public Text textInputed;
    public TextMeshProUGUI textOutputed;
    public bool enterClique;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texteSupp = false;
    }

    // Update is called once per frame
    void Update()
    {
        textOutputed.text = textInputed.text;

        if (textOutputed.text == "")
        {
            texteSupp = true;
        }
    }

    public void detectEnter()
    {
        enterClique = true;
        print("Enter pressed");
    }
}
