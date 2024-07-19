using OrchardCore.Workflows.Display;
using OrchardCore.Workflows.Models;
using YuSheng.OrchardCore.Workflow.Js.Scripting.Activities;
using YuSheng.OrchardCore.Workflow.Js.Scripting.ViewModels;

namespace YuSheng.OrchardCore.Workflow.Js.Scripting.Drivers
{
    public class ScriptTaskDisplayDriver : ActivityDisplayDriver<JsScriptTask, ScriptTaskViewModel>
    {
        protected override void EditActivity(JsScriptTask source, ScriptTaskViewModel model)
        {
            model.JsFilePath = source.JsFilePath.ToString();
            model.Script = source.Script.Expression;
        }

        protected override void UpdateActivity(ScriptTaskViewModel model, JsScriptTask activity)
        {
            activity.JsFilePath = new WorkflowExpression<string>(model.JsFilePath);
            activity.Script = new WorkflowExpression<object>(model.Script);
        }
    }
}
