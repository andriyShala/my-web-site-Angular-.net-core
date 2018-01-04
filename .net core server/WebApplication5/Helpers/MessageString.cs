using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Helpers
{
    [TypeConverter(typeof(ImageStringConverter))]
    public class MessageString
    {
        public int RomId { get; set; }
        public string Username { get; set; }

        public static bool TryParse(string s, out MessageString result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }
            int imagekey;
            if (int.TryParse(parts[0], out imagekey))
            {
                result = new MessageString() { RomId = imagekey, Username = parts[1] };
                return true;
            }
            return false;
        }
    }

    class MessageStringConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                MessageString point;
                if (MessageString.TryParse((string)value, out point))
                {
                    return point;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

    }
}
