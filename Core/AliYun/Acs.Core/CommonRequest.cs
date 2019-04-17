using System;
using SS.SMS.Core.AliYun.Acs.Core.Http;
using SS.SMS.Core.AliYun.Acs.Core.Utils;

namespace SS.SMS.Core.AliYun.Acs.Core
{
    public class CommonRequest : RpcAcsRequest<CommonResponse>
    {
        public CommonRequest(String product, String version, String action)
            : base(product, version, action)
        {
            this.AcceptFormat = FormatType.JSON;
        }

        public CommonRequest(String product, String version, String action, String locationProduct)
            : base(product, version, action, locationProduct)
        {
            this.AcceptFormat = FormatType.JSON;
        }

        public void Add(string key, object value)
        {
            DictionaryUtil.Add(QueryParameters, key, value);
        }

        public override CommonResponse GetResponse(Transform.UnmarshallerContext unmarshallerContext)
        {
            throw new NotImplementedException();
        }
    }
}
