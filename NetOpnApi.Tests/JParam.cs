namespace NetOpnApi.Tests
{
    public class JParam
    {
        public string Description { get; }
        public string Json        { get; }

        public JParam(string d, string j)
        {
            Description = d;
            Json        = j;
        }

        public override string ToString() => Description;
    }
}
