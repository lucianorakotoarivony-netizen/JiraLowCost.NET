using JiraLowCost.api.Constants;
using JiraLowCost.api.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiraLowCost.api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<TaskItem> TaskItems {get; set;}
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        string[] difficulties = [TaskItemDifficulty.JUNIOR, TaskItemDifficulty.MID, TaskItemDifficulty.SENIOR, TaskItemDifficulty.LEAD];
        string inDifficulties = string.Join(", ", difficulties.Select(d => $"'{d}'"));
        string[] roles = [UserRole.JUNIOR, UserRole.MID, UserRole.SENIOR, UserRole.LEAD, UserRole.PO];
        string inRole = string.Join(", ", roles.Select(r => $"'{r}'"));
        string[] priorities = [TaskItemPriority.LOW, TaskItemPriority.MEDIUM, TaskItemPriority.HIGH];
        string inPriorities = string.Join(", ", priorities.Select(p => $"'{p}'"));
        string[] status = [TaskItemStatus.TODO, TaskItemStatus.IN_PROGRESS, TaskItemStatus.PENDING, TaskItemStatus.DONE];
        string inStatus = string.Join(", ", status.Select(s => $"'{s}'"));

        base.OnModelCreating(builder);
        EntityTypeBuilder<User> userBuilder = builder.Entity<User>();
        userBuilder.Property(u => u.Role).HasDefaultValue(UserRole.JUNIOR);
        userBuilder.ToTable(u => u.HasCheckConstraint("CK_user_role",
                                            $"\"Role\" IN ({inRole})"));

        EntityTypeBuilder<TaskItem> taskItemBuilder = builder.Entity<TaskItem>();
        taskItemBuilder.Property(t => t.Title).IsRequired();
        taskItemBuilder.Property(t => t.Status).HasDefaultValue(TaskItemStatus.TODO);
        taskItemBuilder.Property(t => t.Priority).HasDefaultValue(TaskItemPriority.MEDIUM);
        taskItemBuilder.Property(t => t.Difficulty).HasDefaultValue(TaskItemDifficulty.SENIOR);
        taskItemBuilder.ToTable(t => t.HasCheckConstraint("CK_TaskItems_Difficulty",
                                            $"\"Difficulty\" IN ({inDifficulties})"));
        taskItemBuilder.ToTable(t => t.HasCheckConstraint("CK_TaskItems_Priority",
                                            $"\"Priority\" IN ({inPriorities})"));
        taskItemBuilder.ToTable(t => t.HasCheckConstraint("CK_TaskItems_Status",
                                            $"\"Status\" IN ({inStatus})"));

        

    }
}