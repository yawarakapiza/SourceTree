using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    // Start�{�^���������ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    public void StartGame()
    {
        // GameScene�����[�h����
        SceneManager.LoadScene("MapScene");
    }
    public void EndGame()
    {
        Debug.Log("END");
    }
    public void RestartGame()
    {
        // GameScene�����[�h����
        SceneManager.LoadScene("MapScene");
    }
}
