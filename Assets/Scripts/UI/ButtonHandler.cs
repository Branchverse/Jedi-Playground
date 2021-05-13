/* using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonHandler : MonoBehaviour {
	public Button menuButton;
    public GameObject MainMenu;

	void Start () {
		menuButton.GetComponent<Button>();
		menuButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		Debug.Log ("You have clicked the button!");
        MainMenu = GameObject.Find("MainMenu");
        
        if(!MainMenu.active == false) {
            MainMenu.SetActive(true);
        } else {
            MainMenu.SetActive(false);
        }
        
	}
}

*/