// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Usage", "CA2254:템플릿은 정적 표현식이어야 합니다.", Scope = "member",
    Target = "~M:ProjectAme.App.Log(Microsoft.Extensions.Logging.LogLevel,System.String,System.Object[])"
)]
[assembly: SuppressMessage(
    "Info Code Smell", "S1133:Deprecated code should be removed", Scope = "member",
    Target = "~M:ProjectAme.ViewModels.ExtractWindowViewModel.#ctor"
)]
