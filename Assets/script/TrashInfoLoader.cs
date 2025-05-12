using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrashInfoLoader : MonoBehaviour
{
    public GameObject trashInfoItemPrefab;
    public Transform organikContentParent;
    public Transform anorganikContentParent;

    void Start()
    {
        LoadTrashInfo("Trash/Organik", organikContentParent);
        LoadTrashInfo("Trash/Anorganik", anorganikContentParent);
    }

    void LoadTrashInfo(string path, Transform parent)
    {
        GameObject[] trashPrefabs = Resources.LoadAll<GameObject>(path);

        foreach (GameObject trash in trashPrefabs)
        {
            GameObject item = Instantiate(trashInfoItemPrefab, parent);

            // Ambil Sprite dari komponen SpriteRenderer (pastikan prefab punya ini)
            Sprite sprite = trash.GetComponent<SpriteRenderer>()?.sprite;

            if (sprite != null)
            {
                item.transform.Find("TrashImage").GetComponent<Image>().sprite = sprite;
            }

            // Nama berdasarkan prefab
            item.transform.Find("TrashName").GetComponent<TextMeshProUGUI>().text = trash.name;
        }
    }
}
