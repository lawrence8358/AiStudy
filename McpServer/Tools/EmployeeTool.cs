using ModelContextProtocol.Server;
using System.ComponentModel;

// 員工資料 Model   
public class EmployeeModel
{
    public string EmpNo { get; set; } = string.Empty;
    public string NameZh { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
}

[McpServerToolType]
public static class EmployeeTool
{
    // 模擬的員工資料庫 
    private static readonly List<EmployeeModel> _employees = new()
    {
        new EmployeeModel { EmpNo = "0001", NameZh = "沈小凱", NameEn = "Lawrence Shen", Department = "資訊處", Position = "資深經理" },
        new EmployeeModel { EmpNo = "0002", NameZh = "陳怡君", NameEn = "Irene Chen", Department = "人事室", Position = "專員" },
        new EmployeeModel { EmpNo = "0003", NameZh = "張志明", NameEn = "James Chang", Department = "財務處", Position = "會計" },
        new EmployeeModel { EmpNo = "0004", NameZh = "王美華", NameEn = "Mei-Wa Wang", Department = "業務部", Position = "業務代表" },
        new EmployeeModel { EmpNo = "0005", NameZh = "李俊廷", NameEn = "Justin Lee", Department = "資訊處", Position = "工程師" }
    };

    [McpServerTool, Description("取得所有員工清單")]
    public static IEnumerable<EmployeeModel> GetAll() => _employees;

    [McpServerTool, Description("依員工編號取得員工資訊")]
    public static EmployeeModel? GetById([Description("員工編號")] string empno)
        => _employees.FirstOrDefault(e => string.Equals(e.EmpNo, empno, StringComparison.OrdinalIgnoreCase));

    [McpServerTool, Description("依姓名（中文或英文）取得員工資訊字串")]
    public static string GetInfo([Description("員工中文或英文姓名")] string name)
    {
        return _employees
           .Where(e => e.NameEn.Contains(name, StringComparison.OrdinalIgnoreCase) || e.NameZh.Contains(name, StringComparison.OrdinalIgnoreCase))
           .Select(e => $"{e.EmpNo}: {e.NameZh} ({e.NameEn}) — {e.Department} / {e.Position}")
           .FirstOrDefault() ?? "Employee not found.";
    }

    [McpServerTool, Description("依部門搜尋員工列表")]
    public static IEnumerable<EmployeeModel> SearchByDepartment([Description("部門名稱")] string department)
        => _employees.Where(e => e.Department.Contains(department, StringComparison.OrdinalIgnoreCase));
}