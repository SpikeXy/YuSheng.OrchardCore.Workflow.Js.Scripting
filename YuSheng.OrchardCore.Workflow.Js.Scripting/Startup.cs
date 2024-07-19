using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Workflows.Helpers;
using YuSheng.OrchardCore.Workflow.Js.Scripting.Activities;
using YuSheng.OrchardCore.Workflow.Js.Scripting.Drivers;

namespace YuSheng.OrchardCore.Workflow.Js.Scripting
{
    [Feature("OrchardCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {

            services.AddActivity<JsScriptTask, ScriptTaskDisplayDriver>(); ;


        }
    }
}
