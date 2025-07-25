﻿using PontoEstagio.Domain.Entities;

namespace PontoEstagio.Domain.Repositories.UserProjects;

public interface IUserProjectsReadOnlyRepository
{
    Task<Project?> GetCurrentProjectForUserAsync(Guid userId);
    Task<bool> ExistsProjectAssignedToUserAsync(Guid project_id, Guid userIdToAssign);
    Task<bool> IsSupervisorAlreadyAssignedToProjectAsync(Guid projectId);
    Task<int> CountActiveProjectsForSupervisorAsync(Guid supervisorId);
}
