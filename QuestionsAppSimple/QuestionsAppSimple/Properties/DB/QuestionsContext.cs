using Microsoft.EntityFrameworkCore;

namespace QuestionsAppSimple.Properties.DB;

public class QuestionsContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
}
