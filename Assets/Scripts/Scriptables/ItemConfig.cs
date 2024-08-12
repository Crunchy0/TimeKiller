using UnityEngine;

[CreateAssetMenu(fileName = "New Item Config", menuName = "ScriptableObject/Item Config")]
public class ItemConfig : ScriptableObject
{
    public ItemId Id { get => _id; }
    public ItemClass Class { get => _class; }
    public int StackSize { get => _stackSize; }
    public string Name { get => _name; }
    public string Description { get => _description; }
    public GameObject Prefab { get => _itemPrefab; }
    public GameObject EquipmentPrefab { get => _equipmentPrefab; }
    public Sprite Icon { get => _icon; }
    

    [SerializeField] private ItemId _id;
    [SerializeField] private ItemClass _class;
    [SerializeField] [Min(1)] private int _stackSize;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private GameObject _equipmentPrefab;
    [SerializeField] private Sprite _icon;
}