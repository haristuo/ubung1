using Microsoft.EntityFrameworkCore;

namespace QuestionsAppSimple.Properties.DB;

public class QuestionsContext : DbContext
{
    public QuestionsContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Question> Questions { get; set; }
}