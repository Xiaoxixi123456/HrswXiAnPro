using Hrsw.XiAnPro.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrsw.XiAnPro.Models;
using System.Threading;

namespace Hrsw.XiAnPro.LogicActivities
{
    public class MeasureTrayActivity : IAActivity<Tray, bool>
    {
        private IAActivity<Tray, Part> _selector;
        private IAActivity<Part, bool> _mesPartActivity;
        private ActivityController _ac;
        
        public MeasureTrayActivity(ICMMControl cmmControl, ActivityController ac)
        {
            _ac = ac;
            _selector = new PartSelectActivity();
            _mesPartActivity = new MeasurePartActivity(cmmControl);
        }

        public async Task<bool> ExecuteAsync(Tray tray, CancellationTokenSource cts)
        {
            Part part = null;
            tray.Status = TrayStatus.TS_Measuring;
            while (true)
            {
                if (_ac.IsOffline) break;

                if (_ac.Mark.Value)
                {
                    part = await _selector.ExecuteAsync(tray, cts);
                    if (part == null) break;
                }

                if (_ac.IsOffline)
                {
                    part.Status = PartStatus.PS_Idle;
                    break;
                }

                SetupPartParams(tray, part);

                bool success = await _mesPartActivity.ExecuteAsync(part, cts).ConfigureAwait(false);

                if (success) continue;

                part.Status = PartStatus.PS_Error;

                // 测量失败跳转 retry - true, next - false, nexttray - null
                if (_ac.Mark == null) break;
                if (!_ac.Success) return false;
            }
            return true;
        }

        private void SetupPartParams(Tray tray, Part part)
        {
            part.UseCmmNo = tray.UseCmmNo;
            CalcPartOffset(tray, part);
        }

        public void CalcPartOffset(Tray tray, Part part)
        {
            // 第一个工件基准坐标
            double yOrgBas = tray.BaseRowOffset;
            double xOrgBas = tray.BaseColumnOffset;

            int nb = part.SlotNb;
            int rnb = (nb - 1) / tray.ColumnCount;
            int cnb = (nb - 1) % tray.ColumnCount;

            double xBas = xOrgBas + cnb * tray.ColumnOffset;
            double yBas = yOrgBas - rnb * tray.RowOffset;

            part.XOffset = xBas;
            part.YOffset = yBas;
        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Retry()
        {
            throw new NotImplementedException();
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }

    }
}
