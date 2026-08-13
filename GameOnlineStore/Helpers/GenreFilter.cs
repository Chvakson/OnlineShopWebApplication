namespace GameOnlineStore.Helpers
{
    public static class GenreFilter
    {
        public static List<string> Parse(params string?[] values)
        {
            var tags = new List<string>();
            foreach (var value in values)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                foreach (var part in value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    if (!tags.Contains(part, StringComparer.OrdinalIgnoreCase))
                    {
                        tags.Add(part);
                    }
                }
            }

            return tags;
        }

        public static string? Toggle(string? current, string tag)
        {
            var tags = Parse(current);
            var existing = tags.FirstOrDefault(item => item.Equals(tag, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                tags.Remove(existing);
            }
            else
            {
                tags.Add(tag);
            }

            return tags.Count == 0 ? null : string.Join(",", tags);
        }

        public static bool IsSelected(IEnumerable<string> selected, string tag)
        {
            return selected.Contains(tag, StringComparer.OrdinalIgnoreCase);
        }

        public static string? Join(IEnumerable<string> selected)
        {
            var tags = selected.Where(tag => !string.IsNullOrWhiteSpace(tag)).ToList();
            return tags.Count == 0 ? null : string.Join(",", tags);
        }
    }
}
