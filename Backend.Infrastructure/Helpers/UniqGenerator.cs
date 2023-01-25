namespace Backend.Infrastructure.Helpers
{
    public static class UniqGenerator
    {
        public static string Generate(int lenght = 16)
        {
            StringBuilder builder = new StringBuilder();
            Enumerable.Range(65, 26)
                                    .Select(e => ((char)e).ToString())
                                    .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                                    .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                                    .OrderBy(e => Guid.NewGuid())
                                    .Take(lenght)
                                    .ToList().ForEach(e => builder.Append(e));

            return builder.ToString();
        }

    }
}
