using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveButton : MonoBehaviour
{
    public void Leave()
    {
        SceneManager.LoadScene(Constants.Scenes.Game);
    }
}
