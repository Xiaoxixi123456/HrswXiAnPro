namespace Hrsw.XiAnPro.Models
{
    public enum PartStatus
    {
        PS_Empty, // 空状态表示占位符
        PS_Idle,
        PS_Wait,
        PS_Measuring,
        PS_Measured,
        PS_Error
    }
}