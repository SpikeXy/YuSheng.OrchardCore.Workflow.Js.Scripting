using Jint;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using SimpleExec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace YuSheng.OrchardCore.Workflow.Js.Scripting.Activities
{
    public class JsScriptTask : TaskActivity
    {
        private readonly IWorkflowScriptEvaluator _scriptEvaluator;
        private readonly IStringLocalizer S;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        public JsScriptTask(IWorkflowScriptEvaluator scriptEvaluator,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IHtmlHelper htmlHelper,
            IStringLocalizer<JsScriptTask> localizer)
        {
            _scriptEvaluator = scriptEvaluator;
            _expressionEvaluator = expressionEvaluator;
            S = localizer;
            _htmlHelper = htmlHelper;

        }

        public override string Name => nameof(JsScriptTask);

        public override LocalizedString DisplayText => S["Js Script Task"];

        public override LocalizedString Category => S["Script"];

        public WorkflowExpression<string> JsBinPath
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }


        public WorkflowExpression<string> JsFilePath
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        /// <summary>
        /// The script can call any available functions, including setOutcome().
        /// </summary>
        public WorkflowExpression<object> Script
        {
            get => GetProperty(() => new WorkflowExpression<object>());
            set => SetProperty(value);
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Success"], S["Failed"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {

            var jsBinPath = await _expressionEvaluator.EvaluateAsync(JsBinPath, workflowContext, null);
            var jsFilePath = await _expressionEvaluator.EvaluateAsync(JsFilePath, workflowContext, null);
            var script = Script.Expression;
            var exeContent = "";
            string code = "";
            bool isSuccess = true;
            try
            {
                if (string.IsNullOrEmpty(jsBinPath))
                {
                    //use jint as js engine
                    if (!string.IsNullOrEmpty(jsFilePath))
                    {
                        string jsCode = File.ReadAllText(jsFilePath);
                        var engine = new Engine();
                        engine.Execute(jsCode);
                        
                        //TODO: Workflow allows for input and output, enhancing scalability
                        //var result = engine.Invoke("yourFunctionName", "argument1", "argument2");
                        //Console.WriteLine(result);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(script))
                        {
                            var engine = new Engine();
                            engine.Execute(script);
                        }
                        else
                        {
                            workflowContext.Output["JsScript"] = "At least one of Js File and script has a value ";
                            return Outcomes("Failed");
                        }
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(jsFilePath))
                    {
                        exeContent = jsFilePath;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(script))
                        {
                            exeContent = script;
                        }
                        else
                        {
                            workflowContext.Output["JsScript"] = "Bash Script Content is null";
                            return Outcomes("Failed");
                        }
                    }
            
                    var (standardOutput1, standardError1) = await Command.ReadAsync(jsBinPath, exeContent);
                    if (string.IsNullOrEmpty(standardError1))
                    {
                        code = standardOutput1;
                    }
                    else
                    {
                        isSuccess = false;
                        code = standardError1;
                    }
                }
            }
            catch (Exception ex)
            {
                code = ex.Message;
                isSuccess = false;
            }

            workflowContext.Output["JsScript"] = _htmlHelper.Raw(_htmlHelper.Encode(code));
            if (isSuccess)
            {
                return Outcomes("Success");
            }
            else
            {
                return Outcomes("Failed");
            }
        }
    }
}
