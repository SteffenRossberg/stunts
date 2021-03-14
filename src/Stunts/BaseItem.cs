using System.Linq;
using System.Reflection;

namespace Stunts
{
    public class BaseItem
    {
        protected readonly string Id;

        protected BaseItem(string id)
        {
            Id = id;
        }

        protected const string Separator = ".";
        protected const string North = "N";
        protected const string South = "S";
        protected const string East = "E";
        protected const string West = "W";

        public static implicit operator string(BaseItem item) => item.Id;

        protected static TItem FindItem<TItem>(string item) where TItem : BaseItem
            => typeof(TItem)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => field.GetValue(null))
                .Cast<TItem>()
                .FirstOrDefault(field => field == item);

        public override string ToString() => this;
    }
}
