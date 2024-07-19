using System.ComponentModel.DataAnnotations;

namespace YuSheng.OrchardCore.Workflow.Js.Scripting.ViewModels
{
    public class ScriptTaskViewModel
    {
        [Required]
        public string JsFilePath { get; set; }

        [Required]
        public string Script { get; set; }
    }
}
