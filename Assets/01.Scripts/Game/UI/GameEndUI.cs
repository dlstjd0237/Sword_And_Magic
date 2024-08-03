using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameEndUI : MonoBehaviour
{
    [SerializeField] private string _reTryScreenName;
    [SerializeField] private Button _reTryBtn, _exitBtn;

    private void Awake()
    {
        _reTryBtn.onClick.AddListener(HandleReTryEvent);
        _exitBtn.onClick.AddListener(HandleExitEvent);
    }

    private void HandleExitEvent()
    {
        SceneControlManager.FadeOut(() => Application.Quit());
    }

    private void HandleReTryEvent()
    {
        SceneControlManager.FadeOut(() => SceneManager.LoadScene(_reTryScreenName));
    }
}
