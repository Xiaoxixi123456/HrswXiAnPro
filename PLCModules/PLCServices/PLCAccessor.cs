using Hrsw.XiAnPro.Utilities;
using Snap7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLCServices
{
    public class PLCAccessor
    {
        private S7Client _s7Client;
        private object syncLock = new object();
        public string PlcIP { get; set; }
        
        public PLCAccessor()
        {
            _s7Client = new S7Client();
        }

        #region 公共方法
        [PlcAccessRetry(5, 200)]
        public int Connect()
        {
            int success = _s7Client.ConnectTo(PlcIP, 0, 0);
            return success;
        }

        [PlcAccessRetry(3, 200)]
        public int Disconnect()
        {
            int success = _s7Client.Disconnect();
            return success;
        } 

        public bool IsConnected()
        {
           return _s7Client.Connected();
        }
        #endregion

        #region 写标志位
        [PlcAccessRetry(5, 200)]
        public int WriteMasks(int dbNumber, int start, byte mark)
        {
            byte[] buf = new byte[1];
            int result;
            lock (syncLock)
            {
                result = _s7Client.DBRead(dbNumber, start, 1, buf);
                byte marks = (byte)(buf[0] | mark);
                if (result != 0)
                    return result;
                result = _s7Client.DBWrite(dbNumber, start, 1, new byte[] { marks });
            }
            return result;
        }
        [PlcAccessRetry(5, 200)]
        public int WriteMasks(int dbNumber, int start, bool flag, params int[] pos)
        {
            byte[] buf = new byte[1];
            int result = -1;
            if (pos.Length <= 0 || pos.Length > 8)
            {
                return -1;
            }
            lock (syncLock)
            {
                result = _s7Client.DBRead(dbNumber, start, 1, buf);
                if (result != 0)
                    return result;
                for (int i = 0; i < pos.Length; i++)
                {
                    S7.SetBitAt(ref buf, 0, pos[i], flag);
                }
                result = _s7Client.DBWrite(dbNumber, start, 1, buf);
            }
            return result;
        }
        #endregion

        #region 读标志位
        [PlcAccessRetry(5, 200)]
        public int ReadMask(int dbNumber, int start, int bit, out bool result)
        {
            result = false;
            int success;
            byte[] buf = new byte[1];
            lock (syncLock)
            {
                success = _s7Client.DBRead(dbNumber, start, 1, buf);
                if (success != 0)
                    return success;
            }
            result = S7.GetBitAt(buf, 0, bit);
            return success;
        } 
        #endregion

        #region 写字符串
        [PlcAccessRetry(3, 300)]
        public int WriteString(int dbNumber, int start, int size, string message)
        {
            int success = -1;
            byte[] buf = new byte[size];
            S7.SetStringAt(buf, 0, size - 2, message);
            lock (syncLock)
            {
                success = _s7Client.DBWrite(dbNumber, start, size, buf);
                Thread.Sleep(300);
            }
            return success;
        }
        [PlcAccessRetry(3, 300)]
        public int WriteASCIIString(int dbNumber, int start, string message)
        {
            int success = -1;
            byte[] buf = new byte[message.Length];
            S7.SetCharsAt(buf, 0, message);
            lock (syncLock)
            {
                success = _s7Client.DBWrite(dbNumber, start, message.Length, buf);
                Thread.Sleep(300);
            }
            return success;
        }
        #endregion

        #region 读字符串
        [PlcAccessRetry(3, 300)]
        public int ReadString(int dbNumber, int start, int size, out string message)
        {
            message = string.Empty;
            int success = -1;
            byte[] buf = new byte[size];
            lock (syncLock)
            {
                success = _s7Client.DBRead(dbNumber, start, size, buf);
                Thread.Sleep(300);
            }
            if (success != 0)
                return success;
            message = S7.GetStringAt(buf, 0);
            return success;
        }
        [PlcAccessRetry(3, 300)]
        public int ReadASCIIString(int dbNumber, int start, int size, out string message)
        {
            message = string.Empty;
            int success = -1;
            byte[] buf = new byte[size];
            lock (syncLock)
            {
                success = _s7Client.DBRead(dbNumber, start, size, buf);
                Thread.Sleep(300);
            }
            if (success != 0)
                return success;
            message = S7.GetCharsAt(buf, 0, size);
            return success;
        } 
        #endregion
    }
}
