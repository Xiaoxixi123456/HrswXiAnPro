using Hrsw.XiAnPro.Utilities;
using Prism.Mvvm;
using Snap7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLCServices
{
    public class PLCAccessor : BindableBase
    {
        private S7Client _s7Client;
        private object syncLock = new object();
        private static PLCAccessor _inst;
        public static PLCAccessor Instance => _inst ?? (_inst = new PLCAccessor());
        [Bindable]
        public string PlcIP { get; set; }
        [Bindable]
        public bool Connected { get; set; }


        private PLCAccessor()
        {
            _s7Client = new S7Client();
        }

        private void Validate(int code)
        {
            if (code != 0)
            {
                string errorStr = _s7Client.ErrorText(code);
                throw new InvalidOperationException(errorStr);
            }
        }

        #region 公共方法


        [PlcAccessRetry(5, 200)]
        public void Connect()
        {
            int code = _s7Client.ConnectTo(PlcIP, 0, 0);
            Validate(code);
            Connected = true;
        }

        [PlcAccessRetry(3, 200)]
        public void Disconnect()
        {
            int code = _s7Client.Disconnect();
            Validate(code);
            Connected = false;
        } 

        public bool IsConnected()
        {
           return _s7Client.Connected();
        }
        #endregion

        #region 写标志位
        [PlcAccessRetry(5, 200)]
        public void WriteMasks(int dbNumber, int start, byte mark)
        {
            byte[] buf = new byte[1];
            lock (syncLock)
            {
                int code = _s7Client.DBRead(dbNumber, start, 1, buf);
                Validate(code);
                byte marks = (byte)(buf[0] | mark);
                code = _s7Client.DBWrite(dbNumber, start, 1, new byte[] { marks });
                Validate(code);
            }
        }

        [PlcAccessRetry(5, 200)]
        public void WriteMasks(int dbNumber, int start, bool flag, params int[] pos)
        {
            byte[] buf = new byte[1];
            if (pos.Length <= 0 || pos.Length > 8)
            {
                throw new InvalidOperationException("输入的位数不对");
            }
            lock (syncLock)
            {
                int code = _s7Client.DBRead(dbNumber, start, 1, buf);
                Validate(code);
                for (int i = 0; i < pos.Length; i++)
                {
                    S7.SetBitAt(ref buf, 0, pos[i], flag);
                }
                code = _s7Client.DBWrite(dbNumber, start, 1, buf);
                Validate(code);
            }
        }
        #endregion

        #region 读存储器标志位
        [PlcAccessRetry(5, 200)]
        public void ReadMask(int dbNumber, int start, int bit, out bool result)
        {
            result = false;
            byte[] buf = new byte[1];
            lock (syncLock)
            {
                int code = _s7Client.DBRead(dbNumber, start, 1, buf);
                Validate(code);
            }
            result = S7.GetBitAt(buf, 0, bit);
        }
        #endregion

        #region 读I口标志位
        [PlcAccessRetry(5, 200)]
        public void ReadIMask(int start, int bit, out bool result)
        {
            result = false;
            byte[] buf = new byte[1];
            lock (syncLock)
            {
                int code = _s7Client.EBRead(start, 1, buf);
                Validate(code);
            }
            result = S7.GetBitAt(buf, 0, bit);
        }
        #endregion

        #region 写字符串
        [PlcAccessRetry(3, 300)]
        public void WriteString(int dbNumber, int start, int size, string message)
        {
            byte[] buf = new byte[size];
            S7.SetStringAt(buf, 0, size - 2, message);
            lock (syncLock)
            {
                int code = _s7Client.DBWrite(dbNumber, start, size, buf);
                Validate(code);
                Thread.Sleep(300);
            }
        }

        [PlcAccessRetry(3, 300)]
        public void WriteASCIIString(int dbNumber, int start, string message)
        {
            byte[] buf = new byte[message.Length];
            S7.SetCharsAt(buf, 0, message);
            lock (syncLock)
            {
                int code = _s7Client.DBWrite(dbNumber, start, message.Length, buf);
                Validate(code);
                Thread.Sleep(300);
            }
        }
        #endregion

        #region 读字符串
        [PlcAccessRetry(3, 300)]
        public void ReadString(int dbNumber, int start, int size, out string message)
        {
            message = string.Empty;
            byte[] buf = new byte[size];
            lock (syncLock)
            {
                int code = _s7Client.DBRead(dbNumber, start, size, buf);
                Validate(code);
                Thread.Sleep(300);
            }
            message = S7.GetStringAt(buf, 0);
        }

        [PlcAccessRetry(3, 300)]
        public void ReadASCIIString(int dbNumber, int start, int size, out string message)
        {
            message = string.Empty;
            byte[] buf = new byte[size];
            lock (syncLock)
            {
                int code = _s7Client.DBRead(dbNumber, start, size, buf);
                Validate(code);
                Thread.Sleep(300);
            }
            message = S7.GetCharsAt(buf, 0, size);
        }
        #endregion

        #region 写整数
        [PlcAccessRetry(5, 200)]
        public void WriteInt(int dbNumber, int start, int value)
        {
            byte[] buf = new byte[2];
            lock (syncLock)
            {
                S7.SetIntAt(buf, 0, (short)value);
                int code = _s7Client.DBWrite(dbNumber, start, 2, buf);
                Validate(code);
            }
        }

        [PlcAccessRetry(5, 200)]
        public void WriteByte(int dbNumber, int start, int value)
        {
            //byte[] buf = new byte[1];
            lock (syncLock)
            {
                byte[] buf = BitConverter.GetBytes(value);
                //S7.SetByteAt(buf, 0, buf[0]);
                int code = _s7Client.DBWrite(dbNumber, start, 1, buf);
                Validate(code);
            }
        }
        #endregion
    }
}
