namespace NxtLib.TaggedData
{
    public class TaggedData : ITaggedData
    {
        public string Channel { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public bool IsText { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
    }
}