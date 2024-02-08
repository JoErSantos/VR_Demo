using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Canvas pauseMenu;
    
    private bool isMenuOpen;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.enabled = false;
        isMenuOpen = false;
    }

    // Update is called once per frame
    public void UpdateText(string promptMessage){
        promptText.text = promptMessage;
    }

    private void Update(){
        pauseMenu.enabled = isMenuOpen;
    }

    public void ToogleIsMenuOpen(){
        isMenuOpen = !isMenuOpen;
    }

}
