using System.Web.UI.WebControls;

namespace SS.SMS.Model
{
    public enum ESmsProviderType
    {
        None,
        AliYun,
        Yunpian
    }

    public class ESmsProviderTypeUtils
    {
        public static string GetValue(ESmsProviderType type)
        {
            if (type == ESmsProviderType.AliYun)
            {
                return "AliYun";
            }

            if (type == ESmsProviderType.Yunpian)
            {
                return "Yunpian";
            }
            return "None";
        }

        public static string GetText(ESmsProviderType type)
        {
            if (type == ESmsProviderType.AliYun)
            {
                return "阿里云";
            }
            if (type == ESmsProviderType.Yunpian)
            {
                return "云片";
            }
            return "无";
        }

        public static string GetUrl(ESmsProviderType type)
        {
            if (type == ESmsProviderType.AliYun)
            {
                return "https://www.aliyun.com/product/sms";
            }
            if (type == ESmsProviderType.Yunpian)
            {
                return "http://www.yunpian.com/";
            }
            return string.Empty;
        }

        public static ESmsProviderType GetEnumType(string typeStr)
        {
            var retVal = ESmsProviderType.None;
            if (Equals(typeStr, ESmsProviderType.AliYun))
            {
                retVal = ESmsProviderType.AliYun;
            }
            else if (Equals(typeStr, ESmsProviderType.Yunpian))
            {
                retVal = ESmsProviderType.Yunpian;
            }
            return retVal;
        }

        private static bool Equals(ESmsProviderType type, string typeStr)
        {
            return !string.IsNullOrEmpty(typeStr) && string.Equals(GetValue(type).ToLower(), typeStr.ToLower());
        }

        private static bool Equals(string typeStr, ESmsProviderType type)
        {
            return Equals(type, typeStr);
        }

        private static ListItem GetListItem(ESmsProviderType type, bool selected)
        {
            var item = new ListItem(GetText(type), GetValue(type));
            if (selected)
            {
                item.Selected = true;
            }
            return item;
        }

        public static void AddListItems(ListControl listControl)
        {
            if (listControl != null)
            {
                listControl.Items.Add(GetListItem(ESmsProviderType.None, false));
                //listControl.Items.Add(GetListItem(ESmsProviderType.AliYun, false));
                listControl.Items.Add(GetListItem(ESmsProviderType.Yunpian, false));
            }
        }
    }
}
