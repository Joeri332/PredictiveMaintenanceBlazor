using System.ComponentModel;
using System.Reflection;

namespace PredictiveMaintenance.Enums
{
    public static class EnumExtensions
    {
        public static FailuresEnums GetEnumById(int id)
        {
            if (Enum.IsDefined(typeof(FailuresEnums), id))
            {
                return (FailuresEnums)id;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid enum ID");
            }
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
