using System.ComponentModel.DataAnnotations;

namespace YuSheng.OrchardCore.Workflow.Js.Scripting.ViewModels
{
    public class YuShengScriptTaskViewModel
    {
        [Required]
        public string JsBinPath { get; set; }
        [Required]
        public string JsFilePath { get; set; }

        [Required]
        public string Script { get; set; }
    }
}
