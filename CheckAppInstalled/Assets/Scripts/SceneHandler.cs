using NativeSupports;
using UnityEngine;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private Button findButton;
    [SerializeField] private Text resultText;

    private void OnEnable()
    {
        findButton?.onClick.AddListener(FindApp);
    }

    private void FindApp()
    {
        
        if (AppChecker.IsDiscordInstalled())
        {
            resultText.text = "INSTALLED";
        }
        else
        {
            resultText.text = "NOT Found";
        }
    }
}
