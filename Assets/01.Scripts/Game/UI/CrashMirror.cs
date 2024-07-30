using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;
public class CrashMirror : MonoBehaviour
{
    public float breakDuration;
    public Transform cam;
    public Transform mirrorParent;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Crash();
    }

    private void Crash()
    {
        for (int i = 0; i < mirrorParent.childCount; i++)
        {
            mirrorParent.GetChild(i).DOLocalRotate(new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20)), breakDuration);
            mirrorParent.GetChild(i).DOScale(/*mirrorParent.GetChild(i).localScale / 1.1f*/Vector3.zero, breakDuration);
        }

        cam.DOShakePosition(breakDuration, .5f, 20, 90, false, true);
    }
}
