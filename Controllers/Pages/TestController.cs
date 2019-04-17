using System;
using System.Web.Http;
using SiteServer.Plugin;
using SS.SMS.Core;

namespace SS.SMS.Controllers.Pages
{
    [RoutePrefix("pages/test")]
    public class TestController : ApiController
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

                configInfo.TestType = request.GetPostString(nameof(configInfo.TestType));
                configInfo.TestMobile = request.GetPostString(nameof(configInfo.TestMobile));
                configInfo.TestTplId = request.GetPostString(nameof(configInfo.TestTplId));

                if (configInfo.TestType == "code")
                {
                    if (!Plugin.Instance.SendCode(configInfo.TestMobile, Utils.GetRandomInt(1000, 9999), configInfo.TestTplId, out var errorMessage))
                    {
                        return BadRequest(errorMessage);
                    } 
                }

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
