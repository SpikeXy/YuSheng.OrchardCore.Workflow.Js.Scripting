using OrchardCore.Workflows.Display;
using OrchardCore.Workflows.Models;
using YuSheng.OrchardCore.Workflow.Js.Scripting.Activities;
using YuSheng.OrchardCore.Workflow.Js.Scripting.ViewModels;

namespace YuSheng.OrchardCore.Workflow.Js.Scripting.Drivers
{
    public class ScriptTaskDisplayDriver : ActivityDisplayDriver<JsScriptTask, YuShengScriptTaskViewModel>
    {
        protected override void EditActivity(JsScriptTask source, YuShengScriptTaskViewModel model)
        {
            model.JsBinPath = source.JsBinPath.ToString();
            model.JsFilePath = source.JsFilePath.ToString();
            model.Script = source.Script.Expression;
        }

        protected override void UpdateActivity(YuShengScriptTaskViewModel model, JsScriptTask activity)
        {
            activity.JsBinPath = new WorkflowExpression<string>(model.JsBinPath);
            activity.JsFilePath = new WorkflowExpression<string>(model.JsFilePath);
            activity.Script = new WorkflowExpression<object>(model.Script);
        }
    }
}
