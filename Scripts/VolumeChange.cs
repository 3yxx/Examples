using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    public AudioSource music;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeVolume()
    {
        music.volume = slider.value;
    }
}
