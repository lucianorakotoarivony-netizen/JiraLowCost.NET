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
        base.OnModelCreating(builder);
        EntityTypeBuilder<User> userBuilder = builder.Entity<User>();
        userBuilder.Property(u => u.Role).HasDefaultValue(UserRole.JUNIOR);

        EntityTypeBuilder<TaskItem> taskItemBuilder = builder.Entity<TaskItem>();
        taskItemBuilder.Property(t => t.Title).IsRequired();
        taskItemBuilder.Property(t => t.Status).HasDefaultValue(TaskItemStatus.TODO);
        taskItemBuilder.Property(t => t.Priority).HasDefaultValue(TaskItemPriority.MEDIUM);
        taskItemBuilder.Property(t => t.Difficulty).HasDefaultValue(UserRole.SENIOR);
    }
}