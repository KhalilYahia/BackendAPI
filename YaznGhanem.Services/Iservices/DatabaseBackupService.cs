using YaznGhanem.Common;
using YaznGhanem.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.Iservices
{
    public interface IDatabaseBackupService
    {
        Task<string> ExportDatabaseToJsonAsync();

    }
}
