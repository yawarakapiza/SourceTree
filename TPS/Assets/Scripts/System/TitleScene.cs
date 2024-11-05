using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    // Startボタンが押されたときに呼び出されるメソッド
    public void StartGame()
    {
        // GameSceneをロードする
        SceneManager.LoadScene("MapScene");
    }
    public void EndGame()
    {
        Debug.Log("END");
    }
    public void RestartGame()
    {
        // GameSceneをロードする
        SceneManager.LoadScene("MapScene");
    }
}
