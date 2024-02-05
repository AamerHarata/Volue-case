using Microsoft.EntityFrameworkCore;

namespace Volue_case.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    
}