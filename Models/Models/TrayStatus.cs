namespace Hrsw.XiAnPro.Models
{
    public enum TrayStatus
    {
        TS_Empty, // 空状态标识占位符
        TS_Idle,
        TS_Placed,
        TS_Wait,
        TS_Loading,
        TS_Measuring,
        TS_Measured,
        TS_Unloading,
        TS_Error
    }
}