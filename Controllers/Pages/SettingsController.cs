using System;
using System.Web.Http;
using SiteServer.Plugin;
using SS.SMS.Core;

namespace SS.SMS.Controllers.Pages
{
    [RoutePrefix("pages/settings")]
    public class SettingsController : ApiController
    {
        private const string Route = "";

        [HttpGet, Route(Route)]
        public IHttpActionResult GetConfig()
        {
            try
            {
                var request = Context.AuthenticatedRequest;
                if (!request.IsAdminLoggin || !request.AdminPermissions.HasSystemPermissions(Plugin.PluginId))
                {
                    return Unauthorized();
                }

                return Ok(new
                {
                    Value = Plugin.GetConfigInfo()
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost, Route(Route)]
        public IHttpActionResult Submit()
        {
            try
            {
                var request = Context.AuthenticatedRequest;
                if (!request.IsAdminLoggin || !request.AdminPermissions.HasSystemPermissions(Plugin.PluginId))
                {
                    return Unauthorized();
                }

                var configInfo = Plugin.GetConfigInfo();

                configInfo.IsEnabled = request.GetPostBool(nameof(configInfo.IsEnabled));
                configInfo.Provider = request.GetPostString(nameof(configInfo.Provider));
                configInfo.YunPianAppKey = request.GetPostString(nameof(configInfo.YunPianAppKey));
                configInfo.AliYunAccessKeyId = request.GetPostString(nameof(configInfo.AliYunAccessKeyId));
                configInfo.AliYunAccessKeySecret = request.GetPostString(nameof(configInfo.AliYunAccessKeySecret));
                configInfo.AliYunSignName = request.GetPostString(nameof(configInfo.AliYunSignName));

                Context.ConfigApi.SetConfig(Plugin.PluginId, 0, configInfo);

                return Ok(new { });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
