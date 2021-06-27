using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BattleshipsServer.Helpers
{
    public static class JsonListHelper
    {
        public static string AddItem<TItem>(string serialisedItems, Func<TItem, bool> isMatch, TItem item)
        {
            var items = DeserialiseItems<TItem>(serialisedItems);

            if (!items.Any(isMatch))
            {
                items.Add(item);
            }

            return SerialiseItems(items);
        }

        public static string RemoveItem<TItem>(string serialisedItems, Func<TItem, bool> isMatch)
        {
            var items = DeserialiseItems<TItem>(serialisedItems);

            var item = items.SingleOrDefault(isMatch);
            if (item != null)
            {
                items.Remove(item);
            }

            return SerialiseItems(items);
        }

        private static IList<TItem> DeserialiseItems<TItem>(string serialisedItems)
        {
            return string.IsNullOrWhiteSpace(serialisedItems)
                ? new List<TItem>()
                : JsonSerializer.Deserialize<List<TItem>>(
                    serialisedItems,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
                );
        }

        private static string SerialiseItems<TItem>(IEnumerable<TItem> deserialisedItems)
        {
            return JsonSerializer.Serialize(
                deserialisedItems,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
            );
        }
    }
}
