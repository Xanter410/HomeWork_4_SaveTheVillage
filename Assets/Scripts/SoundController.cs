using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] Sprite _iconMuteSound;
    [SerializeField] Sprite _iconUnmuteSound;
    [SerializeField] Image _switchSoundsButtonIcon;

    [SerializeField] AudioSource _clickButton;
    [SerializeField] AudioSource _spawnWarrior;
    [SerializeField] AudioSource _spawnFermer;
    [SerializeField] AudioSource _productionWheat;
    [SerializeField] AudioSource _attack;

    [SerializeField] private bool _isPlaying = true;

    public void SwitchSound()
    {
        if (_isPlaying)
        {
            _switchSoundsButtonIcon.sprite = _iconMuteSound;
            _isPlaying = false;
        }
        else
        {
            _switchSoundsButtonIcon.sprite = _iconUnmuteSound;
            _isPlaying = true;
        }
    }

    public void PlayAudioClickButton()
    {
        if (_isPlaying)
            _clickButton.Play();
    }
    public void PlayAudioSpawnWarrior()
    {
        if (_isPlaying)
            _spawnWarrior.Play();
    }
    public void PlayAudioSpawnFermer()
    {
        if (_isPlaying)
            _spawnFermer.Play();
    }
    public void PlayAudioProductWheat()
    {
        if (_isPlaying)
            _productionWheat.Play();
    }
    public void PlayAudioAttack()
    {
        if (_isPlaying)
            _attack.Play();
    }
}
