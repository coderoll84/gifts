namespace Mvc.Utilities
{
    public class ActionResponse<T>
    {
        public T Object { get; set; }
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string Message { get; set; }
    }
}
