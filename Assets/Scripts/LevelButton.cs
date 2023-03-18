using UnityEngine;
using DG.Tweening;

public class LevelButton : MonoBehaviour
{
    [SerializeField] int level;

    public int GetButtonLevel()
    {
        return level;
    }
    
    public void LoadLevel()
    {
        this.transform.DOScale(2.5f, 0.5f).SetLoops(1).SetEase(Ease.OutSine);
        //FindObjectOfType<LevelManager>().LoadLevel(level);
    }
}
