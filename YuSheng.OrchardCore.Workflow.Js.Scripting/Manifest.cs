using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "YuSheng OrchardCore Workflow Js Scripting",
    Author = "spike",
    Website = "",
    Version = "0.0.1"
)]

[assembly: Feature(
    Id = "YuSheng OrchardCore Workflow Js Scripting",
    Name = "YuSheng OrchardCore Workflow Js Scripting",
    Description = "Provides js scripting ",
    Dependencies = new[] { "OrchardCore.Workflows" },
    Category = "Workflows"
)]
