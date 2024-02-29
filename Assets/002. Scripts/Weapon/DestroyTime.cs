using UnityEngine;

public enum DeleteType
{
    Destory,
    Inactive,
}
public class DestroyTime : MonoBehaviour
{
    public DeleteType DeleteType;
    public float DeleteTime = 1.5f;
    private float _timer = 0;

    public void Init()
    {
        _timer = 0;
    }

    private void OnDisable()            //사용가능하지 못할 때
    {
        Init();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= DeleteTime)
        {
            if (DeleteType == DeleteType.Destory)
            {
                Destroy(this.gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}