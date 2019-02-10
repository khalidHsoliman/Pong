using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadScene : MonoBehaviour {

	
	void Start () {
        Invoke("Load", 3.0f);	
	}

    private void Load()
    {
        SceneManager.LoadScene(1);
    }
	

}
