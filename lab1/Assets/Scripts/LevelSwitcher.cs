using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    public void LoadTrack1()
    {
        SceneManager.LoadScene("Track1");
    }

    public void LoadTrack2()
    {
        SceneManager.LoadScene("Track2");
    }
}
