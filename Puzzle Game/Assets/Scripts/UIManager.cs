using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject Win_Panel;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.currentState==GameManager.GameState.Win)
        {
            Win_Panel.SetActive(true);
            GameManager.Instance.currentState = GameManager.GameState.GameEnded;
        }
    }
    public void Play_Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
