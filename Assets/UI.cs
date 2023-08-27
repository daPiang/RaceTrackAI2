using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;
    public Canvas[] carCanvases;

    private void Start()
    {
        SwitchToCamera(0);
    }

    public void SwitchToCamera(int cameraIndex)
    {
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].Priority = (i == cameraIndex) ? 10 : 0;
            carCanvases[i].enabled = (i == cameraIndex);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
