namespace Engine.Transfer
{
    public class PublicClass
    {
        public DictionaryNode? Node;
    }

    public class DictionaryNode
    {
        public int NodeId { get; set; }
        public string? NodeDisPlayName { get; set; }
        public int NodeParentId { get; set; }
        public bool IsChecked { get; set; }
    }
}