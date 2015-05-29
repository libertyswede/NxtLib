namespace NxtLib
{
    public interface ITaggedData
    {
        string Channel { get; set; }
        string Data { get; set; }
        string Description { get; set; }
        string Filename { get; set; }
        bool IsText { get; set; }
        string Name { get; set; }
        string Tags { get; set; }
        string Type { get; set; }
    }
}