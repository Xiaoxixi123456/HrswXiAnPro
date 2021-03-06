﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using System.Threading;

namespace Hrsw.XiAnPro.Utilities
{
    //[Serializable]
    //public class PlcAccessRetry : MethodInterceptionAspect
    //{
    //    private int _times;
    //    private int _milsec;
    //    public PlcAccessRetry(int times, int milsec)
    //    {
    //        _times = times;
    //        _milsec = milsec;
    //    }

    //    public override void OnInvoke(MethodInterceptionArgs args)
    //    {
    //        int success = -1;
    //        while (true)
    //        {
    //            args.Proceed();
    //            success = (int)args.ReturnValue;
    //            if (success == 0 || _times-- == 0)
    //                break;
    //            Thread.Sleep(_milsec);
    //        }
    //    }
    //}

    [Serializable]
    public class PlcAccessRetry : MethodInterceptionAspect
    {
        private int _times;
        private int _milsec;
        public PlcAccessRetry(int times, int milsec)
        {
            _times = times;
            _milsec = milsec;
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            while (true)
            {
                //args.Proceed();
                //success = (int)args.ReturnValue;
                //if (success == 0 || _times-- == 0)
                //    break;
                try
                {
                    args.Proceed();
                    break;
                }
                catch (InvalidOperationException ex)
                {
                    if (_times-- == 0)
                    {
                        // 由使用者接收异常
                        throw ex;
                    }
                }
                Thread.Sleep(_milsec);
            }
        }
    }
}
