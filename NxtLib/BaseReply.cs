namespace NxtLib
{
    public interface IBaseReply
    {
        string RequestUri { get; set; }
        int RequestProcessingTime { get; set; }
        string RawJsonReply { get; set; }

        void PostProcess();
    }

    public abstract class BaseReply : IBaseReply
    {
        public string RequestUri { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RawJsonReply { get; set; }

        public virtual void PostProcess()
        {
        }
    }
}