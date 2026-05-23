
using JiraLowCost.api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JiraLowCost.api.Controllers.TaskItemController;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public partial class TaskItemController(ITaskItemService taskItemService): ControllerBase
{
    readonly string [] Role = [UserRole.PO, UserRole.LEAD];
    readonly string UnauthorizedOperationException = "Vous n'êtes pas autorisé à effectuer cette opération.";
    readonly string UnauthorizedAccessException = "Vous n'avez pas le droit d'accéder à ses ressources.";
}