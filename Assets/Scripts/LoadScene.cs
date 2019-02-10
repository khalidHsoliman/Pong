using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// This class loads the game after 3 seconds 
/// </summary>
public class LoadScene : MonoBehaviour {

	
	void Start () {
        // invoke is calling the Load function after 3 seconds
        Invoke("Load", 3.0f);	
	}

    private void Load()
    {
        // 1 is the index of the main scene in the build settings 
        SceneManager.LoadScene(1);
    }
	

}
