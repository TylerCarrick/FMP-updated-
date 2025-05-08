using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManger : MonoBehaviour
{
    Button button;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        SceneManager.LoadScene(1);
    }

    public void start()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }
}
