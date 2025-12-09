using ModelContextProtocol.Server; 
using System.ComponentModel; 

// 工作經歷 Model
public class JobHistory
{
    public string EmpNo { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;  
    public string EndDate { get; set; } = string.Empty;  
    public string Description { get; set; } = string.Empty;
}


[McpServerToolType]
public static class JobHistoryTool
{  
    // 模擬的工作經歷資料庫 
    private static readonly List<JobHistory> _histories = new()
    {
        // 0001 - 4 筆 
        new JobHistory { EmpNo = "0001", Company = "阿爾法科技", Position = "軟體工程師", StartDate = "2010-06", EndDate = "2013-05", Description = "負責核心後端服務與系統優化，實作高可用性方案" },
        new JobHistory { EmpNo = "0001", Company = "倍塔解決方案", Position = "資深軟體工程師", StartDate = "2013-06", EndDate = "2016-09", Description = "帶領小型團隊，設計分散式系統與 API 架構" },
        new JobHistory { EmpNo = "0001", Company = "伽馬應用", Position = "工程部主管", StartDate = "2016-10", EndDate = "2019-12", Description = "建立工程團隊與開發流程，推動微服務與 CI/CD 機制" },
        new JobHistory { EmpNo = "0001", Company = "德爾塔系統", Position = "技術經理", StartDate = "2020-01", EndDate = "2025-12", Description = "負責技術策略、技術團隊擴編與跨部門專案協作" },

        // 0002 - 1 筆
        new JobHistory { EmpNo = "0002", Company = "卡帕人資", Position = "人力資源專員", StartDate = "2018-01", EndDate = "2023-10", Description = "人力資源與招募" },
        
        // 0003 - 2 筆
        new JobHistory { EmpNo = "0003", Company = "財務夥伴", Position = "會計", StartDate = "2016-02", EndDate = "2019-09", Description = "處理帳務與報表" },
        new JobHistory { EmpNo = "0003", Company = "金融公司", Position = "資深會計", StartDate = "2019-10", EndDate = "2024-06", Description = "擔任會計主管" },
       
        // 0004 - 0 筆 (社會新鮮人) -> none here
       
        // 0005 - 5 筆
        new JobHistory { EmpNo = "0005", Company = "初創一號", Position = "實習生", StartDate = "2016-07", EndDate = "2016-09", Description = "專案實習" },
        new JobHistory { EmpNo = "0005", Company = "初創一號", Position = "初級開發工程師", StartDate = "2016-10", EndDate = "2018-06", Description = "前端與跨平台開發" },
        new JobHistory { EmpNo = "0005", Company = "世代科技", Position = "開發工程師", StartDate = "2018-07", EndDate = "2020-12", Description = "後端服務與資料庫優化" },
        new JobHistory { EmpNo = "0005", Company = "雲端科技", Position = "資深開發工程師", StartDate = "2021-01", EndDate = "2022-12", Description = "雲端平台與 CI/CD" },
        new JobHistory { EmpNo = "0005", Company = "AI 解決方案", Position = "資深技術組長", StartDate = "2023-01", EndDate = "2025-11", Description = "領導團隊與技術策略 (現職)" },
    };
 
    [McpServerTool, Description("取得指定員工的工作經歷")]
    public static IEnumerable<JobHistory> GetJobHistoryByEmpNo([Description("員工編號")] string empNo)
    {
        return _histories.Where(h => string.Equals(h.EmpNo, empNo));
    }
}
