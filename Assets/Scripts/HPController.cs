using System.Collections;
using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] private int HP = 3;
    private int currentHP;
    [SerializeField] private GameObject prefabHP;

    private void initHP()
    {
        currentHP = HP;
        int countSideEl = HP / 2;
        int[] massOffset = new int[HP];
        for (int i = -countSideEl, j = 0; i <= countSideEl; i++, j++)
        {
            massOffset[j] = i;
        }
        for (int i = 0; i < HP; i++)
        {
            GameObject hp = Instantiate(prefabHP, gameObject.transform);
            hp.transform.localPosition = new Vector3(massOffset[i] * 0.1f, 0.2f, 0);
        }
    }

    private void clearHP()
    {
        int countHP = gameObject.transform.childCount - 1;
        for (int i = 0; i < countHP;i++)
        {
            Destroy(gameObject.transform.GetChild(1 + i).gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initHP();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Traps")
        {
            GameObject lastHP = gameObject.transform.GetChild(currentHP).gameObject;
            lastHP.GetComponent<HPAnimationController>().DestroyAnim();
            StartCoroutine(destroyHp(lastHP));
        }
        if (currentHP == 0 || collision.gameObject.name == "DeadZone")
        {
            clearHP();
            transform.position = gameObject.GetComponent<PlayerCheckpointController>().LastCheckpoint.transform.position + Vector3.up;
            initHP();
        }
    }

    IEnumerator destroyHp(GameObject obj)
    {
        currentHP--;
        yield return new WaitForSeconds(0.2f);
        Destroy(obj);
    }
}
